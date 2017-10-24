using System.Linq;
using System.Threading.Tasks;
using AspNetCoreStarter.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreStarter.Data.Repositories
{
    public interface IUsersRepository
    {
        Task<int> CountAsync();
        Task<ApplicationUser> GetByEmail(string email);
    }

    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _db;

        public UsersRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Task<int> CountAsync()
        {
            return _db.Users.CountAsync();
        }

        public Task<ApplicationUser> GetByEmail(string email)
        {
            return _db.Users.Where(c => c.Email == email).FirstOrDefaultAsync();
        }
    }
}
