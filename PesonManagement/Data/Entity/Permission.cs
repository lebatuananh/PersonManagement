using System;

namespace PesonManagement.Data.Entity
{
    using PesonManagement.Utils;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Permissions")]
    public class Permission : DomainEntity<Guid>
    {
        [Required]
        public Guid RoleId { get; set; }

        [Required]
        public Guid FunctionId { get; set; }

        public bool CanCreate { set; get; }
        public bool CanRead { set; get; }

        public bool CanUpdate { set; get; }
        public bool CanDelete { set; get; }
        public bool ImportExcel { get; set; }
        public bool ExportExcel { get; set; }

        [ForeignKey("RoleId")]
        public virtual AppRole AppRole { get; set; }

        [ForeignKey("FunctionId")]
        public virtual Function Function { get; set; }
    }
}