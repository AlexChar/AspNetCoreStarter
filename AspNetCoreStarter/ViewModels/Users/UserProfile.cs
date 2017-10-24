using AspNetCoreStarter.Models;
using AutoMapper;

namespace AspNetCoreStarter.ViewModels.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>();
        }
    }
}
