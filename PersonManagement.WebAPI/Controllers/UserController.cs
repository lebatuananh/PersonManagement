namespace PersonManagement.WebAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    using PesonManagement.Application.Interface;
    using PesonManagement.Application.ViewModel;

    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var model = this._userService.GetAll();
            return new OkObjectResult(model);
        }

        /// <summary>
        /// The save entity.
        /// </summary>
        /// <param name="appUserViewModel">
        /// The app user view model.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> SaveEntity(AppUserViewModel appUserViewModel)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(x => x.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                if (appUserViewModel.Id == Guid.Empty)
                {
                    await this._userService.AddAsync(appUserViewModel);
                }
                else
                {
                    await this._userService.UpdateAsync(appUserViewModel);
                }
                return new OkObjectResult(appUserViewModel);
            }
        }

        /// <summary>
        /// The get by id async.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var model = await this._userService.GetById(id);
            return new OkObjectResult(model);
        }

        /// <summary>
        /// The delete async.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                await this._userService.DeleteAsync(id);
                return new OkObjectResult(id);
            }
            
        }

        [HttpGet("{keyword},{page},{pageSize}")]
        public IActionResult GetAllPaging(string keyword, int page, int pageSize)
        {
            var model = this._userService.GetAllPagingAsync(keyword, page, pageSize);
            return new OkObjectResult(model);
        }
    }
}