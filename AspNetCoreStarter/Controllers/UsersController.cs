using System.Threading.Tasks;
using AspNetCoreStarter.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreStarter.Controllers
{
    [Route("api/[controller]")]
    public class UsersController: Controller
    {
        private readonly IUsersRepository _usersRepository;

        public UsersController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        [HttpGet("count")]
        public async Task<IActionResult> Count()
        {
            var count = await _usersRepository.CountAsync();
            return Ok(new {count});
        }
    }
}
