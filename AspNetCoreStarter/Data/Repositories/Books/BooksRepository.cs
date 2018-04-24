using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreStarter.Data.Models;
using AspNetCoreStarter.Data.Stores;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreStarter.Data.Repositories.Books
{
    public class BooksRepository : IBooksRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ICacheStore _booksCache;

        public BooksRepository(ApplicationDbContext dbContext,
            ICacheStore booksCache)
        {
            _dbContext = dbContext;
            _booksCache = booksCache;
        }

        public async Task<Book> FindAsync(int id)
        {
            _booksCache.Cache.TryGetValue(id, out var book);
            if (book != null) return book;

            book = await _dbContext.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);

            _booksCache.Cache.Add(id, book);

            return book;
        }

        public async Task<IList<Book>> FetchAll()
        {
            return await _dbContext.Books
                .Include(b => b.Author)
                .ToListAsync();
        }

        public async Task<Book> SaveAsync(Book book)
        {
            await _dbContext.Books.AddAsync(book);

            await _dbContext.SaveChangesAsync();

            return book;
        }
    }
}