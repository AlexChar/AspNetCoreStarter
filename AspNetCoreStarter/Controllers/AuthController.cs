using System.Threading.Tasks;
using AspNetCoreStarter.Application.Policies;
using AspNetCoreStarter.Data.Repositories.Users;
using AspNetCoreStarter.Models;
using AspNetCoreStarter.Models.AccountViewModels;
using AspNetCoreStarter.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreStarter.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : IdentityController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthService _authService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly ICurrentUser _currentUser;

        public AuthController(UserManager<ApplicationUser> userManager,
            IAuthService authService,
            SignInManager<ApplicationUser> _signInManager,
            IPasswordHasher<ApplicationUser> passwordHasher,
            ICurrentUser currentUser)
        {
            _userManager = userManager;
            _authService = authService;
            this._signInManager = _signInManager;
            _passwordHasher = passwordHasher;
            _currentUser = currentUser;
        }

        [AllowAnonymous, HttpPost("token")]
        public async Task<IActionResult> GetToken([FromBody] LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return Unauthorized();

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe,
                    lockoutOnFailure: false);

            if (!result.Succeeded) return GetErrorResult(result);

            var jwt = await _authService.GenerateJwtAsync(user);
            return Ok(jwt);
        }

        [HttpGet("values")]
        public async Task<IActionResult> Values()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return Ok(new
            {
                user,
                currentUserId = _currentUser.UserId,
                currentUser = await _currentUser.GetUserAsync(),
                d = new[] { "paris", "polyzos", }
            });
        }

        [HttpGet("values-policy"), Authorize(Policy = AuthServerPolicies.RolePermissionsPolicy)]
        public async Task<IActionResult> ValuesForRolePermissions()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return Ok(new
            {
                user,
                currentUserId = _currentUser.UserId,
                currentUser = await _currentUser.GetUserAsync(),
                d = new[] { "AllowRolePermissionsChange Policy" }
            });
        }
    }
}
