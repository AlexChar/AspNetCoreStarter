using Newtonsoft.Json;

namespace AspNetCoreStarter.ViewModels.Books
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [JsonProperty("d")]
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
    }
}
