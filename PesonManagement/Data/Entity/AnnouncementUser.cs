using System;

namespace PesonManagement.Data.Entity
{
    using PesonManagement.Utils;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using PesonManagement.Data.Entity.Interface;

    [Table("AnnouncementUsers")]
    public class AnnouncementUser : DomainEntity<Guid>,IDateTracking
    {
        [Required]
        public Guid AnnouncementId { get; set; }

        public Guid UserId { get; set; }

        public bool? HasRead { get; set; }

        [ForeignKey("AnnouncementId")]
        public virtual Announcement Announcement { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
        
    }
}