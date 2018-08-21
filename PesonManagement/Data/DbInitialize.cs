using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PesonManagement.Data
{
    using Microsoft.AspNetCore.Identity;

    using PesonManagement.Data.Entity;
    using PesonManagement.Utils;

    public class DbInitialize
    {
        private readonly AppDbContext _context;
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;

        public DbInitialize(AppDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            this._context = context;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task Seed()
        {
            if (!_context.Roles.Any())
            {
                await _roleManager.CreateAsync(
                    new AppRole() { Name = "Admin", NormalizedName = "Admin", Description = "Top Manager", });
                await _roleManager.CreateAsync(
                    new AppRole() { Name = "Staff", NormalizedName = "Staff", Description = "Staff" });
                await _roleManager.CreateAsync(
                    new AppRole() { Name = "Customer", NormalizedName = "Customer", Description = "Customer" });
            }

            if (!_context.Users.Any())
            {
                await _userManager.CreateAsync(
                    new AppUser()
                    {
                        UserName = "admin",
                        FullName = "Admin",
                        Email = "admin@gmail.com",
                        Status = Status.Active,
                        CreatedDate = DateTime.Now
                    },
                    "123654$");
                var userAdmin = await _userManager.FindByNameAsync("admin");
                await _userManager.AddToRoleAsync(userAdmin, "Admin");
                await _userManager.CreateAsync(
                    new AppUser()
                    {
                        UserName = "staff",
                        FullName = "Staff",
                        Email = "staff@gmail.com",
                        Status = Status.Active,
                        CreatedDate = DateTime.Now
                    },
                    "123654$");
                var userStaff = await _userManager.FindByNameAsync("staff");
                await _userManager.AddToRoleAsync(userStaff, "Staff");
                await _userManager.CreateAsync(
                    new AppUser()
                    {
                        UserName = "customer",
                        FullName = "Customer",
                        Email = "customer@gmail.com",
                        Status = Status.Active,
                        CreatedDate = DateTime.Now
                    },
                    "123654$");
                var userCustomer = await _userManager.FindByNameAsync("customer");
                await _userManager.AddToRoleAsync(userCustomer, "Customer");
            }

            if (!_context.Persons.Any())
            {
                List<Person> listPersons = new List<Person>();
                for (int i = 1; i <= 20; i++)
                {
                    var person = new Person();
                    person.Name = "Doctor" + i;
                    person.Job = "Doctor";
                    person.Code = "DR" + i;
                    person.Status = Status.Active;
                    person.Id = new Guid();
                    person.DateOfBirth = DateTime.UtcNow;
                    person.CreatedDate = DateTime.Now;
                    listPersons.Add(person);
                }

                for (int i = 1; i <= 20; i++)
                {
                    var person = new Person();
                    person.Name = "Engineer" + i;
                    person.Job = "Engineer";
                    person.Code = "ENG" + i;
                    person.Status = Status.Active;
                    person.Id = new Guid();
                    person.DateOfBirth = DateTime.UtcNow;
                    person.CreatedDate = DateTime.Now;
                    listPersons.Add(person);
                }

                for (int i = 1; i <= 20; i++)
                {
                    var person = new Person();
                    person.Name = "Singer" + i;
                    person.Job = "Singer";
                    person.Code = "SNG" + i;
                    person.Status = Status.Active;
                    person.Id = new Guid();
                    person.DateOfBirth = DateTime.UtcNow;
                    person.CreatedDate = DateTime.Now;
                    listPersons.Add(person);
                }

                _context.Persons.AddRange(listPersons);
            }

            if (!_context.Functions.Any())
            {
                var guidSystem = Guid.NewGuid();
                var guidPerson = Guid.NewGuid();
                List<Function> listFunctions = new List<Function>()
                                                   {
                                                       new Function()
                                                           {
                                                               Id = guidSystem,
                                                               Name = "System",
                                                               ParentId = null,
                                                               SortOrder = 1,
                                                               Url = "/",
                                                               IconCss="fa-desktop",
                                                               CreatedDate = DateTime.Now
                                                           },
                                                       new Function()
                                                           {
                                                               Id = Guid.NewGuid(),
                                                               Name = "Role",
                                                               ParentId = guidSystem,
                                                               SortOrder = 1,
                                                               Url = "/admin/role/index",
                                                               IconCss="fa-home",
                                                               CreatedDate = DateTime.Now
                                                           },
                                                       new Function()
                                                           {
                                                               Id = Guid.NewGuid(),
                                                               Name = "Function",
                                                               ParentId = guidSystem,
                                                               SortOrder = 2,
                                                               Url = "/admin/function/index",
                                                               IconCss="fa-home",
                                                               CreatedDate = DateTime.Now
                                                           },
                                                       new Function()
                                                           {
                                                               Id = Guid.NewGuid(),
                                                               Name = "User",
                                                               ParentId = guidSystem,
                                                               SortOrder = 3,
                                                               Url = "/admin/user/index",
                                                               IconCss="fa-home",
                                                               CreatedDate = DateTime.Now
                                                           },
                                                       new Function()
                                                           {
                                                               Id = Guid.NewGuid(),
                                                               Name = "Activity",
                                                               ParentId = guidSystem,
                                                               SortOrder = 4,
                                                               Url = "/admin/activity/index",
                                                               IconCss="fa-home",
                                                               CreatedDate = DateTime.Now
                                                           },
                                                       new Function()
                                                           {
                                                               Id = Guid.NewGuid(),
                                                               Name = "Error",
                                                               ParentId = guidSystem,
                                                               SortOrder = 5,
                                                               Url = "/admin/error/index",
                                                               IconCss="fa-home",
                                                               CreatedDate = DateTime.Now

                                                           },
                                                       new Function()
                                                           {
                                                               Id = Guid.NewGuid(),
                                                               Name = "Configuration",
                                                               ParentId = guidSystem,
                                                               SortOrder = 6,
                                                               Url = "/admin/setting/index",
                                                               IconCss="fa-home",
                                                               CreatedDate = DateTime.Now

                                                           },
                                                       new Function()
                                                           {
                                                               Id = guidPerson,
                                                               Name = "Person",
                                                               ParentId = null,
                                                               SortOrder = 2,
                                                               Url = "/",
                                                               IconCss="fa-table",
                                                               CreatedDate = DateTime.Now

                                                           }
                                                   };
                _context.Functions.AddRange(listFunctions);
            }

            await _context.SaveChangesAsync();
        }
    }
}
