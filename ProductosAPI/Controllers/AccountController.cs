using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductosAPI.Auth;

namespace ProductosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {private readonly IJwtAuthenticationService _authService;

        public AccountController(IJwtAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(string login, string password)
        {
            if (login == "admin" && password == "admin123")
            {
                var token = _authService.Authenticate(login);
                return Ok(new { Token = token });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
