using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace AspNetCoreStarter.Controllers
{
    public class IdentityController : Controller
    {
        protected IActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null) { return BadRequest("Error parsing identity result"); }

            // No errors
            if (result.Succeeded) return null;

            if (result.Errors != null)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }

            if (ModelState.IsValid)
            {
                // No ModelState errors are available to send, so just return an empty BadRequest.
                return BadRequest();
            }

            return BadRequest(ModelState);
        }

        protected IActionResult GetErrorResult(SignInResult result)
        {
            if (result == null) { return BadRequest("Error parsing identity result"); }

            // No errors
            if (result.Succeeded) return null;

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("lockedout", "User account locked out.");
            }

            if (ModelState.IsValid)
            {
                // No ModelState errors are available to send, so just return an empty BadRequest.
                return BadRequest();
            }

            return BadRequest(ModelState);
        }
    }
}
