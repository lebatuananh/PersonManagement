using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PesonManagement.Application.Interface
{
    using PesonManagement.Application.ViewModel;
    using PesonManagement.Utils;

    public interface IRoleService
    {
        Task<bool> AddAsync(AnnouncementViewModel announcement, List<AnnouncementUserViewModel> announcementUsers, AppRoleViewModel roleVm);

        Task DeleteAsync(Guid id);

        List<AppRoleViewModel> GetAll();

        PaginationResult<AppRoleViewModel> GetAllPagingAsync(string keyword, int page, int pageSize);

        Task<AppRoleViewModel> GetById(Guid id);

        Task UpdateAsync(AppRoleViewModel userVm);

        List<PermissionViewModel> GetListFunctionWithRole(Guid roleId);

        void SavePermission(List<PermissionViewModel> permissions, Guid roleId);

        Task<bool> CheckPermission(string functionId, string action, string[] roles);
    }
}