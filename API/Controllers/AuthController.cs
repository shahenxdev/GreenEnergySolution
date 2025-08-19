using API.Helper;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private static readonly List<Users> Users = new()
        {
            new Users { Username = "admin", Password = "password" },
            new Users { Username = "user", Password = "password" }
        };

        [HttpPost("login")]
        public IActionResult Login([FromBody] Users login)
        {
            var user = Users.SingleOrDefault(u => u.Username == login.Username && u.Password == login.Password);
            if (user == null)
                return Unauthorized("Invalid credentials");

            var token = JwtHelper.GenerateToken(user.Username);
            return Ok(new { Token = token });
        }
    }
}
