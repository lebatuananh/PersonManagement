namespace PesonManagement.Data.Entity.Interface
{
    public interface IHasSoftDelete
    {
        bool IsDelete { get; set; }
    }
}