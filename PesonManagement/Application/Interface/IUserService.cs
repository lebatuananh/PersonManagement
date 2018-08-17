using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PesonManagement.Application.Interface
{
    using PesonManagement.Application.ViewModel;
    using PesonManagement.Utils;

    public interface IUserService
    {
        Task<bool> AddAsync(AppUserViewModel userVm);

        Task DeleteAsync(Guid id);

        List<AppUserViewModel> GetAll();

        PaginationResult<AppUserViewModel> GetAllPagingAsync(string keyword, int page, int pageSize);

        Task<AppUserViewModel> GetById(Guid id);

        Task UpdateAsync(AppUserViewModel userVm);
    }
}
