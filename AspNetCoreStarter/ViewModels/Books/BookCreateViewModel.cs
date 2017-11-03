using System.ComponentModel.DataAnnotations;

namespace AspNetCoreStarter.ViewModels.Books
{
    public class BookCreateViewModel
    {
        [Required, MinLength(2)]
        public string Title { get; set; }
        [MinLength(2)]
        public string Description { get; set; }
        [MinLength(2)]
        public string AuthorName { get; set; }
    }
}
