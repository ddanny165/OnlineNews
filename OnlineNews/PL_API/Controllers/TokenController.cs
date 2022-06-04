using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Text;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using BBL.Interfaces;
using BBL.DTO;

namespace PL_API.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration; 
        private readonly IAuthorService _authorService;

        public TokenController(IConfiguration configuration, IAuthorService authorService)
        {
            _authorService = authorService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Post(AuthorDTO authorData)
        {
            if (authorData.Username == null || authorData.Email == null || authorData.Password == null)
            {
                return BadRequest();
            }

            var author = await GetAuthor(authorData.Email, authorData.Password);

            if (author == null)
            {
                return BadRequest("Invalid credentials");

            }

            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", author.Id.ToString()),
                        new Claim("Username", author.Username),
                        new Claim("Email", author.Email)
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

        private async Task<AuthorDTO> GetAuthor(string email, string password)
        {
            var authors = await _authorService.GetAllAsync();

            return authors.FirstOrDefault(a => a.Email == email && a.Password == password);
        }
    }
}
