using System;

namespace PesonManagement.Data.Entity.Interface
{
    public interface IDateTracking
    {
        DateTime CreatedDate { get; set; }
        DateTime ModifiedDate { get; set; }
    }
}