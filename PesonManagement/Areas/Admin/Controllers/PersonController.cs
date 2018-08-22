using Microsoft.AspNetCore.Mvc;
using PesonManagement.Application.Interface;

namespace PesonManagement.Areas.Admin.Controllers
{
    public class PersonController : BaseController
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var model = _personService.GetAll();
            return new OkObjectResult(model);
        }
    }
}