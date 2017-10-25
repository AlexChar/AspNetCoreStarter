using System.Threading.Tasks;
using AspNetCoreStarter.Models;

namespace AspNetCoreStarter.Services.Auth
{
    public interface IAuthService
    {
        Task<object> GenerateJwtAsync(ApplicationUser user);
    }
}