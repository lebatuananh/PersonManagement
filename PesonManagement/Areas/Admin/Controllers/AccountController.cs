using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PesonManagement.Data.Entity;

namespace PesonManagement.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;

    public class AccountController : BaseController
    {
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(SignInManager<AppUser> signInManager)
        {
            this._signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await this._signInManager.SignOutAsync();
            return this.Redirect("/Admin/Login/Index");
        }
    }
}