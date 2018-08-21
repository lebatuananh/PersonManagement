using Microsoft.AspNetCore.Mvc;

namespace PesonManagement.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;

    [Area("Admin")]
    [Authorize]
    public class BaseController : Controller
    {
    }
}