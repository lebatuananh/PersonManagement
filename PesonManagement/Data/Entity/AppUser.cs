using System;

namespace PesonManagement.Data.Entity
{
    using Microsoft.AspNetCore.Identity;
    using PesonManagement.Data.Entity.Interface;
    using PesonManagement.Utils;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AppUsers")]
    public class AppUser : IdentityUser<Guid>, IDateTracking, IHasSoftDelete
    {
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Avatar { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool IsDelete { get; set; }
    }
}