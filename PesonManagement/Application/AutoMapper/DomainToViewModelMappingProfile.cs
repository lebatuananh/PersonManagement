namespace PesonManagement.Application.AutoMapper
{
    using global::AutoMapper;

    using PesonManagement.Application.ViewModel;
    using PesonManagement.Data.Entity;

    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Person, PersonViewModel>().MaxDepth(2);
            CreateMap<Function, FunctionViewModel>();
            CreateMap<Permission, PermissionViewModel>().MaxDepth(2);
            CreateMap<AppRole, AppRoleViewModel>().MaxDepth(2);
            CreateMap<AppUser, AppUserViewModel>()
                .ForMember(dest => dest.DateCreated, m => m.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.ModifiedDate, m => m.MapFrom(src => src.ModifiedDate))
                .MaxDepth(2);
        }
    }
}