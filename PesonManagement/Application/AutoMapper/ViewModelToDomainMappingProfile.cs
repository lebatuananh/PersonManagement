namespace PesonManagement.Application.AutoMapper
{
    using global::AutoMapper;

    using PesonManagement.Application.ViewModel;
    using PesonManagement.Data.Entity;

    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<FunctionViewModel, Function>().ConstructUsing(c => new Function());
            CreateMap<PersonViewModel, Person>().ConstructUsing(c => new Person());
            CreateMap<PermissionViewModel, Permission>().ConstructUsing(c => new Permission());
            CreateMap<AppUserViewModel, AppUser>().ConstructUsing(c => new AppUser());
            CreateMap<AppRoleViewModel, AppRole>().ConstructUsing(x => new AppRole());
            CreateMap<AnnouncementViewModel, Announcement>().ConstructUsing(x => new Announcement());
            CreateMap<AnnouncementUserViewModel, AnnouncementUser>().ConstructUsing(x => new AnnouncementUser());
        }
    }
}