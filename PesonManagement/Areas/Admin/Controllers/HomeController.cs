using Microsoft.AspNetCore.Mvc;
using PesonManagement.Extensions;

namespace PesonManagement.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            //Request.HttpContext.Response.Headers.Add("X-My-Test-Header", "XX-Secret+xxx+xx+hambamjdo");
            var email=User.GetSpecificClaim("Email");
            return View();
        }
    }
}