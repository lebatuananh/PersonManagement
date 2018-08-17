using Microsoft.AspNetCore.Mvc;

namespace PesonManagement.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;

    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}