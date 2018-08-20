using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PesonManagement.Areas.Components
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Mvc;

    using PesonManagement.Application.Interface;
    using PesonManagement.Application.ViewModel;
    using PesonManagement.Extensions;
    using PesonManagement.Utils;

    public class SideBarViewComponent : ViewComponent
    {
        private IFunctionService _functionService;

        public SideBarViewComponent(IFunctionService functionService)
        {
            this._functionService = functionService;
        }

        public IViewComponentResult Invoke()
        {
            var roles = ((ClaimsPrincipal)User).GetSpecificClaim("Roles");
            List<FunctionViewModel> functions;
            if (roles.Split(";").Contains(CommonConstants.AppRole.Admin))
            {
                functions = this._functionService.GetAll(string.Empty);
            }
            else
            {
                functions = this._functionService.GetAll(string.Empty);
            }

            return this.View(functions);
        }
    }
}
