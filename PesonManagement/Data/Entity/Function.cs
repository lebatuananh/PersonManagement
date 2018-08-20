using System;

namespace PesonManagement.Data.Entity
{
    using PesonManagement.Data.Entity.Interface;
    using PesonManagement.Utils;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Functions")]
    public class Function : DomainEntity<Guid>, IDateTracking, IHasSoftDelete
    {
        public int SortOrder { get; set; }
        public Guid? ParentId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Url { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool IsDelete { get; set; }

        public Status Status { get; set; }
    }
}