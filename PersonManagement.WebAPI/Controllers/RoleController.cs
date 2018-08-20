using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PersonManagement.WebAPI.Controllers
{
    using PesonManagement.Application.Interface;

    public class RoleController : ApiController
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            this._roleService = roleService;
        }

        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpGet(Name = "GetAllRole")]
        public IActionResult GetAll()
        {
            var model = this._roleService.GetAll();
            return new OkObjectResult(model);
        }

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var model = await this._roleService.GetById(id);
            return new OkObjectResult(model);
        }

        /// <summary>
        /// The get all paging.
        /// </summary>
        /// <param name="keyword">
        /// The keyword.
        /// </param>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        [HttpGet("{keyword},{page},{pageSize}", Name = "GetAllPagingRole")]
        public IActionResult GetAllPaging(string keyword, int page, int pageSize)
        {
            var model = this._roleService.GetAllPagingAsync(keyword, page, pageSize);
            return new OkObjectResult(model);
        }
    }
}