using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PesonManagement.Data.Entity;

namespace PesonManagement.Areas.Admin.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;

    [Area("Admin")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await this._signInManager.SignOutAsync();
            return this.Redirect("Admin/Home/Index");
        }
    }
}