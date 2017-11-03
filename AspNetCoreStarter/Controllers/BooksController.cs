using System.Threading.Tasks;
using AspNetCoreStarter.Data.Models;
using AspNetCoreStarter.Data.Repositories.Books;
using AspNetCoreStarter.ViewModels.Books;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreStarter.Controllers
{
    [Route("api/[controller]"), AllowAnonymous]
    public class BooksController : Controller
    {
        private readonly IBooksRepository _booksRepository;

        public BooksController(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        [HttpGet("{id:min(1)}")]
        public async Task<IActionResult> Get(int id)
        {
            var book = await _booksRepository.FindAsync(id);
            if (book == null) return NotFound();

            return Ok(Mapper.Map<BookViewModel>(book));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]BookCreateViewModel model)
        {
            var book = new Book
            {
                Title = model.Title,
                Description = model.Description,
                Author = new Author
                {
                    Name = model.AuthorName
                }
            };

            // TODO: Use repository pattern to save a book

            //_dbContext.Books.Add(book);
            //await _dbContext.SaveChangesAsync();

            return Ok(book);
        }
    }
}
