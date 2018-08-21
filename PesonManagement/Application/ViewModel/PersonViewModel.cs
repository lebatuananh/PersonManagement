using System;

namespace PesonManagement.Application.ViewModel
{
    using PesonManagement.Utils;
    using System.ComponentModel.DataAnnotations;

    public class PersonViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Job { get; set; }

        public Status Status { get; set; }

        public string Avatar { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDelete { get; set; }
    }
}