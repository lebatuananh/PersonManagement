using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PesonManagement.Application.Interface;
using PesonManagement.Application.ViewModel;

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

        [HttpPost]
        public IActionResult Update(PersonViewModel personViewModel)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(x => x.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                if (personViewModel.Id==Guid.Empty)
                {
                    _personService.Add(personViewModel);
                }
                else
                {
                    _personService.Update(personViewModel);
                }
                _personService.Save();
                return new OkObjectResult(personViewModel);
            }
        }
    }
}