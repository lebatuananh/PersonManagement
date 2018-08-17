using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PesonManagement.Data.Interface
{
    public interface IUnitOfWork :IDisposable
    {
        void Commit();
    }
}
