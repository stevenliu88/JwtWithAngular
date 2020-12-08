using JWTExampleWebAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWTExampleWebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        public AuthController(IConfiguration config)
        {
            _config = config;
        }
        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] LoginModel user) 
        {
            if (user == null) {
                return BadRequest("Invalid client request");
            }

            if (user.UserName == "John" & user.Password == "1234") 
            {
                var securityToken = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecurityKey"]));
                var credential = new SigningCredentials(securityToken, SecurityAlgorithms.HmacSha256);

                var claims = new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.NameId, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var token = new JwtSecurityToken
                    (
                        issuer: _config["Jwt:Issuer"],
                        audience : _config["Jwt:Issuer"],
                        claims,
                        expires: DateTime.Now.AddMinutes(60),
                        signingCredentials: credential
                    );

                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(new {token = jwtToken});
            }

            return Unauthorized();
        }
    }
}
