using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PesonManagement.Data.Implementation
{
    using PesonManagement.Data.Interface;
    public class EFUnitOfWork:IUnitOfWork
    {
        private readonly AppDbContext _context;

        public EFUnitOfWork(AppDbContext context)
        {
            this._context = context;
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        public void Commit()
        {
            this._context.SaveChanges();
        }
    }
}
