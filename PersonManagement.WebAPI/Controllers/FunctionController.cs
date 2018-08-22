using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonManagement.WebAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    using PesonManagement.Application.Interface;
    using PesonManagement.Application.ViewModel;

    public class FunctionController : ApiController
    {
        private readonly IFunctionService _funtionService;

        public FunctionController(IFunctionService funtionService)
        {
            this._funtionService = funtionService;
        }

        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        [HttpGet("{filter}", Name = "GetAllFilter")]
        public async Task<IActionResult> GetAllFilter(string filter)
        {
            try
            {
                var model = await _funtionService.GetAll(filter);
                return Ok(new OkObjectResult(model));
            }
            catch (Exception ex)
            {
                return BadRequest(new OkObjectResult(new { key = "Error: ", value = ex.ToString() }));
            }
        }

        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpGet(Name = "GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var model = await _funtionService.GetAll(string.Empty);
            var rootFunctions = model.Where(c => c.ParentId == null);
            var items = new List<FunctionViewModel>();
            foreach (var function in rootFunctions)
            {
                //add the parent category to the item list
                items.Add(function);
                //now get all its children (separate Category in case you need recursion)
                GetByParentId(model.ToList(), function, items);
            }
            return new ObjectResult(items);
        }

        /// <summary>
        /// The get all with parent id.
        /// </summary>
        /// <param name="parentId">
        /// The parent id.
        /// </param>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        [HttpGet("{parentId}", Name = "GetAllWithParentId")]
        public IActionResult GetAllWithParentId(Guid parentId)
        {
            var model = this._funtionService.GetAllWithParentId(parentId);
            return new OkObjectResult(model);
        }

        /// <summary>
        /// The save entity.
        /// </summary>
        /// <param name="functionViewModel">
        /// The function view model.
        /// </param>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        [HttpPost(Name = "SaveEntity")]
        public IActionResult SaveEntity(FunctionViewModel functionViewModel)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(x => x.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                if (functionViewModel.Id == Guid.Empty)
                {
                    this._funtionService.Add(functionViewModel);
                }
                else
                {
                    this._funtionService.Update(functionViewModel);
                }
                this._funtionService.Save();
                return new OkObjectResult(functionViewModel);
            }

        }

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        [HttpGet("{id}", Name = "GetById")]
        public IActionResult GetById(Guid id)
        {
            var model = this._funtionService.GetById(id);
            return new OkObjectResult(model);
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        [HttpPost("id", Name = "Delete")]
        public IActionResult Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                this._funtionService.Delete(id);
                this._funtionService.Save();
                return new OkObjectResult(id);
            }

        }

        /// <summary>
        /// The re order source.
        /// </summary>
        /// <param name="sourceId">
        /// The source id.
        /// </param>
        /// <param name="targetId">
        /// The target id.
        /// </param>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        [HttpPost("{source},{target}", Name = "ReOrderSource")]
        public IActionResult ReOrderSource(Guid sourceId, Guid targetId)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                this._funtionService.ReOrder(sourceId, targetId);
                this._funtionService.Save();
                return new OkObjectResult(this._funtionService.GetById(sourceId));
            }
        }

        /// <summary>
        /// The update parent id.
        /// </summary>
        /// <param name="sourceId">
        /// The source id.
        /// </param>
        /// <param name="targetId">
        /// The target id.
        /// </param>
        /// <param name="items">
        /// The items.
        /// </param>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        [HttpPost("{source},{target},{items}", Name = "UpdateParentId")]
        public IActionResult UpdateParentId(Guid sourceId, Guid targetId, Dictionary<Guid, int> items)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                this._funtionService.UpdateParentId(sourceId, targetId, items);
                this._funtionService.Save();
                return new OkObjectResult(this._funtionService.GetById(sourceId));
            }
        }
        #region Private Functions
        private void GetByParentId(IEnumerable<FunctionViewModel> allFunctions,
                                   FunctionViewModel parent, IList<FunctionViewModel> items)
        {
            var functionsEntities = allFunctions as FunctionViewModel[] ?? allFunctions.ToArray();
            var subFunctions = functionsEntities.Where(c => c.ParentId == parent.Id);
            foreach (var cat in subFunctions)
            {
                //add this category
                items.Add(cat);
                //recursive call in case your have a hierarchy more than 1 level deep
                GetByParentId(functionsEntities, cat, items);
            }
        }
        #endregion
    }
}
