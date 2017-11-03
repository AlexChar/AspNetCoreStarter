using System.Threading.Tasks;
using AspNetCoreStarter.Data.Models;

namespace AspNetCoreStarter.Data.Repositories.Books
{
    public interface IBooksRepository
    {
        Task<Book> FindAsync(int id);
    }
}