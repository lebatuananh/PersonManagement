using System;
using System.Collections.Generic;
using System.Linq;

namespace PesonManagement.Areas.Components
{
    using Microsoft.AspNetCore.Mvc;
    using PesonManagement.Application.Interface;
    using PesonManagement.Application.ViewModel;
    using PesonManagement.Extensions;
    using PesonManagement.Utils;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class SideBarViewComponent : ViewComponent
    {
        private IFunctionService _functionService;

        public SideBarViewComponent(IFunctionService functionService)
        {
            this._functionService = functionService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var roles = ((ClaimsPrincipal)User).GetSpecificClaim("Roles");
            List<FunctionViewModel> functions;
            if (roles.Split(";").Contains(CommonConstants.AppRole.Admin))
            {
                functions = await this._functionService.GetAll(string.Empty);
            }
            else
            {
                functions = await this._functionService.GetAll(string.Empty);
            }
            // ReSharper disable once Mvc.ViewComponentViewNotResolved
            return this.View(functions);
        }
    }
}