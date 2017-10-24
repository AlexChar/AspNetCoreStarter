using System.ComponentModel.DataAnnotations;

namespace AspNetCoreStarter.ViewModels.Users
{
    public class UserEditViewModel
    {
        [Required, Range(1, 100)]
        public int Id { get; set; }

        [Required, DataType(DataType.EmailAddress), MinLength(6)]
        public string Email { get; set; }

        [Required, MinLength(6)]
        public string UserName { get; set; }
    }
}