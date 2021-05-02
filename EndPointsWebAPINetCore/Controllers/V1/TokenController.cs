using LocalDataProvider.DataServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EndPointsWebAPINetCore.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class TokenController : ControllerBase
    {
        private readonly AppDbContext _contexr;
        private readonly UserManager<IdentityUser> _userManager;

        public TokenController(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _contexr = context;
            _userManager = userManager;
        }



        [Route("/Token")]
        [HttpPost]
        public async Task<IActionResult> Create(string username, string password, string grant_type)
        {
            if (await IsValidUsernameAndPassword(username, password))
            {
                return new ObjectResult(await GenerateToken(username));
            }
            else
            {
                return BadRequest();
            }

        }
        [NonAction]
        private async Task<bool> IsValidUsernameAndPassword(string username, string password)
        {
            var user = await  _userManager.FindByEmailAsync(username);
            return await _userManager.CheckPasswordAsync(user,password);
        }
       [NonAction]
        private async Task<dynamic> GenerateToken(string username)
        {
            var user = await _userManager.FindByEmailAsync(username);
            var roles = from ur in _contexr.UserRoles
                        join r in _contexr.Roles on ur.RoleId equals r.Id
                        select new { ur.UserId, r.Id, r.Name };

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,username),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(JwtRegisteredClaimNames.Nbf,new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp,new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString())
            };
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }
            var token = new JwtSecurityToken(
                             new JwtHeader(
                                 new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Mysequritykeyissecretdonotell")),
                   SecurityAlgorithms.HmacSha256)), new JwtPayload(claims));

            var output = new
            {
                Access_token = new JwtSecurityTokenHandler().WriteToken(token),
                Username = username
            };
            return output;
           
        }
    }
}
