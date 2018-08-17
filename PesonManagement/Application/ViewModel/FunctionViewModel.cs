using System;

namespace PesonManagement.Application.ViewModel
{
    using System.ComponentModel.DataAnnotations;

    public class FunctionViewModel
    {
        public Guid Id { get; set; }
        public int SortOrder { get; set; }
        public Guid? ParentId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Url { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDelete { get; set; }
    }
}