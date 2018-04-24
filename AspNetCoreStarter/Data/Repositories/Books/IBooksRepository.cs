using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreStarter.Data.Models;

namespace AspNetCoreStarter.Data.Repositories.Books
{
    public interface IBooksRepository
    {
        Task<Book> FindAsync(int id);
        Task<IList<Book>> FetchAll();
        Task<Book> SaveAsync(Book book);
    }
}