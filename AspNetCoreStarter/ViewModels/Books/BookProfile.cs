using AspNetCoreStarter.Data.Models;
using AutoMapper;

namespace AspNetCoreStarter.ViewModels.Books
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookViewModel>();
        }
    }
}
