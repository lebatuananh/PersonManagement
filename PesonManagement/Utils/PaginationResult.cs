using System.Collections.Generic;

namespace PesonManagement.Utils
{
    public class PaginationResult<T> : PagedResultBase where T : class
    {
        public IList<T> Results { get; set; }

        public PaginationResult()
        {
            Results = new List<T>();
        }
    }
}