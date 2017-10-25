using System.Threading.Tasks;
using AspNetCoreStarter.Models;
using AspNetCoreStarter.ViewModels.Users;

namespace AspNetCoreStarter.Data.Repositories.Users
{
    public interface ICurrentUser
    {
        Task<UserViewModel> GetUserAsync();
        Task<ApplicationUser> GetAppUserAsync();
        string UserId { get; }
    }
}
