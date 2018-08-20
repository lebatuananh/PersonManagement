using System;
using System.Collections.Generic;

namespace PesonManagement.Data.Entity
{
    using PesonManagement.Data.Entity.Interface;
    using PesonManagement.Utils;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Announcements")]
    public class Announcement : DomainEntity<Guid>, IDateTracking
    {
        [Required]
        [StringLength(250)]
        public string Title { set; get; }

        [StringLength(250)]
        public string Content { set; get; }

        public Guid UserId { set; get; }

        [ForeignKey("UserId")]
        public virtual AppUser AppUser { get; set; }

        public virtual ICollection<AnnouncementUser> AnnouncementUsers { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
        public Status Status { get; set; }
    }
}