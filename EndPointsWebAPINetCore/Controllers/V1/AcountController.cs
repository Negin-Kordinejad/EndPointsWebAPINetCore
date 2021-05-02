using EndPointsWebAPINetCore.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPointsWebAPINetCore.Controllers.V1
{
   [Route("Acount")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AcountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
 
        public AcountController(UserManager<IdentityUser> usermanager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = usermanager;
            _signInManager = signInManager;
        }

        // [HttpPost]

      //  POST api/Account/Register
       [AllowAnonymous]
       [Route("Register")]
       [HttpPost]
        public async Task<IActionResult> Register(RegisterUserDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new IdentityUser() { UserName = model.Email, Email = model.Email };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }
       
        [NonAction]
        private IActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return StatusCode(500);
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}