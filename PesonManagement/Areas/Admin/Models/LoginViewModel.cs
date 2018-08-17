using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PesonManagement.Areas.Admin.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using OfficeOpenXml.Utils;

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Require input User Name")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Require input Password, must have at least 1 number, the minimum length is 6 characters")]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Remember")]
        public bool Remember { get; set; }
    }
}
