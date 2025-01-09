using IronPdf_Web_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HtmlToPdfApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("token")]
        public IActionResult GenerateToken([FromBody] LoginRequest request)
        {
            if (request.Username == "user" && request.Password == "password")
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Sample-Signing-Key-IronSoftware-Random-Number-c123sda3r24124fvb"));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, request.Username),
                    new Claim(ClaimTypes.Role, "User")
                };

                var token = new JwtSecurityToken(
                    issuer: "Iron-Software",
                    audience: "Iron-User",
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: credentials);

                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            return Unauthorized();
        }
    }
}
