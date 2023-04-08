using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WetterkarteAPI.Models;

namespace WetterkarteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// Wenn möchten Sie von Databank Anwenden
        /// </summary>
        //IUserService _IUserService;
        //public AuthController(IUserService IUserService)
        //{
        //    _IUserService = IUserService;
        //}

        [HttpPost]
        public IActionResult Post([FromBody] Login login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Etelaat be dorosty vared nashode");
            }

            /// <summary>
            ///  Wenn möchten Sie von Databank Anwenden
            /// </summary>
            ///
            //var user = _IUserService.LoginUser(login);
            //if (user != null)
            //{
            //}

            if (login.Nutzername.ToLower() != "admin" || login.Passwort.ToLower() != "123")
            {
                return Unauthorized();
            }
            
            var SecreatKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("6bb7a98d4c1fe93212efc2cad5ca0525s")); 
            var SigningCredentials = new SigningCredentials(SecreatKey, SecurityAlgorithms.HmacSha256);

            var TokenOption = new JwtSecurityToken(
                issuer: "http://localhost:5070",  
                claims: new List<Claim>
                    {
                        new Claim (ClaimTypes.Name, login.Nutzername), 
                    },
                 expires: DateTime.Now.AddMinutes(30), 
                 signingCredentials: SigningCredentials
                 );
            var TokenString = new JwtSecurityTokenHandler().WriteToken(TokenOption);

            return Ok(new { Token = TokenString });
        }
    }
}

