using LIDOM.Interface;
using LIDOM.Models;
using LIDOM.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebLIDOM.Models.DTO;

namespace LIDOM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepository<User> _userRepository;
        private readonly IConfiguration _configuration;
        public AuthenticationController(IConfiguration configuration)
        {
            var passwordHasher = new utils.PasswordHasher();
            _userRepository = new UserRepository(passwordHasher);
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login(string userName, string password)
        {
            if (!ModelState.IsValid) return Ok(null);

            var user = _userRepository.login(userName, password);
            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("usuario", user.UserName),
                new Claim("password", user.Password),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(4),
                signingCredentials: signIn
                );

            var result = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Register(AuthDto authDto)
        {
            if (!ModelState.IsValid) return Ok(null);

            var user = _userRepository.register(authDto.UserName, authDto.Password);

            return Ok(user);
        }

    }
}
