using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PesonManagement.Application.Implementation
{
    using global::AutoMapper;
    using global::AutoMapper.QueryableExtensions;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using PesonManagement.Application.Interface;
    using PesonManagement.Application.ViewModel;
    using PesonManagement.Data.Entity;
    using PesonManagement.Data.Interface;
    using PesonManagement.Utils;

    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;

        private readonly IRepository<Function, Guid> _functionRepository;

        private readonly IRepository<Announcement, Guid> _announcementRepository;

        private readonly IRepository<AnnouncementUser, Guid> _announcementUserRepository;

        private readonly IRepository<Permission, Guid> _permissionRepository;

        private readonly IUnitOfWork _unitOfWork;

        public RoleService(RoleManager<AppRole> roleManager, IUnitOfWork unitOfWork, IRepository<Function, Guid> functionRepository, IRepository<Announcement, Guid> announcementRepository, IRepository<AnnouncementUser, Guid> announcementUserRepository)
        {
            this._roleManager = roleManager;
            this._unitOfWork = unitOfWork;
            this._functionRepository = functionRepository;
            this._announcementRepository = announcementRepository;
            this._announcementUserRepository = announcementUserRepository;
        }

        public async Task<bool> AddAsync(AnnouncementViewModel announcement, List<AnnouncementUserViewModel> announcementUsers, AppRoleViewModel roleVm)
        {
            var role = new AppRole()
            {
                Name = roleVm.Name,
                Description = roleVm.Description
            };
            var result = await this._roleManager.CreateAsync(role);
            var annoucement = Mapper.Map<AnnouncementViewModel, Announcement>(announcement);
            this._announcementRepository.Add(annoucement);
            foreach (var announcementUser in announcementUsers)
            {
                var user = Mapper.Map<AnnouncementUserViewModel, AnnouncementUser>(announcementUser);
                this._announcementUserRepository.Add(user);
            }

            return result.Succeeded;
        }

        public async Task DeleteAsync(Guid id)
        {
            var role = await this._roleManager.FindByIdAsync(id.ToString());
            await this._roleManager.DeleteAsync(role);
        }

        public  List<AppRoleViewModel> GetAll()
        {
            return  this._roleManager.Roles.ProjectTo<AppRoleViewModel>().ToList();
        }

        public PaginationResult<AppRoleViewModel> GetAllPagingAsync(string keyword, int page, int pageSize)
        {
            var query = this._roleManager.Roles;
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            }

            int totalRow = query.Count();
            query = query.Skip((page - 1) * pageSize).Take(pageSize);
            var data = query.ProjectTo<AppRoleViewModel>().ToList();
            var paginationSet = new PaginationResult<AppRoleViewModel>()
            {
                Results = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize,
            };
            return paginationSet;
        }

        public async Task<AppRoleViewModel> GetById(Guid id)
        {
            var role = await this._roleManager.FindByIdAsync(id.ToString());
            return Mapper.Map<AppRole, AppRoleViewModel>(role);
        }

        public async Task UpdateAsync(AppRoleViewModel roleVm)
        {
            var role = await this._roleManager.FindByIdAsync(roleVm.Id.ToString());
            role.Name = roleVm.Name;
            role.Description = roleVm.Description;
            await this._roleManager.UpdateAsync(role);
        }

        public List<PermissionViewModel> GetListFunctionWithRole(Guid roleId)
        {
            var functions = this._functionRepository.FindAll();
            var permissons = this._permissionRepository.FindAll();
            var query = from f in functions
                        join p in permissons on f.Id equals p.FunctionId into fp
                        from p in fp.DefaultIfEmpty()
                        where p != null && p.RoleId.Equals(roleId)
                        select new PermissionViewModel()
                        {
                            RoleId = roleId,
                            FunctionId = f.Id,
                            CanCreate = p != null ? p.CanCreate : false,
                            CanRead = p != null ? p.CanRead : false,
                            CanDelete = p != null ? p.CanDelete : false,
                            CanUpdate = p != null ? p.CanUpdate : false
                        };
            return query.ToList();
        }

        public void SavePermission(List<PermissionViewModel> permissionsVms, Guid roleId)
        {
            var permissions = Mapper.Map<List<PermissionViewModel>, List<Permission>>(permissionsVms);
            var oldPermission = this._permissionRepository.FindAll().Where(x => x.RoleId.Equals(roleId)).ToList();
            if (oldPermission.Count() > 0)
            {
                this._permissionRepository.RemoveMultiple(oldPermission);
            }

            foreach (var permission in permissions)
            {
                this._permissionRepository.Add(permission);
            }
            this._unitOfWork.Commit();
        }

        public Task<bool> CheckPermission(string functionId, string action, string[] roles)
        {
            var functions = this._functionRepository.FindAll();
            var permissions = this._permissionRepository.FindAll();
            var role = this._roleManager.Roles;
            var query = from f in functions
                        join p in permissions on f.Id equals p.FunctionId
                        join r in role on p.RoleId equals r.Id
                        where roles.Contains(r.Name) && f.Id.Equals(functionId)
                                                     && ((p.CanCreate && action == "Create")
                                                         || (p.CanRead && action == "Read")
                                                         || (p.CanDelete && action == "Delete")
                                                         || (p.CanUpdate) && action == "Update")
                        select p;
            return query.AnyAsync();
        }
    }
}