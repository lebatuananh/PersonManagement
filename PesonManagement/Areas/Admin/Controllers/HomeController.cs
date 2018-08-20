using Microsoft.AspNetCore.Mvc;

namespace PesonManagement.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}