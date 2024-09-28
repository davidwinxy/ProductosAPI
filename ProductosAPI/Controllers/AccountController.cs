using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductosAPI.Auth;
using ProductosAPI.Models;
using ProductosAPI.Services.Interfaces;

namespace InventarioESFEAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IJwtAuthenticationService _authService;
        private readonly ILoginService _loginService;

        public AccountController(IJwtAuthenticationService authService, ILoginService loginService)
        {
            _authService = authService;
            _loginService = loginService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var login = await _loginService.GetByEmailAsync(loginRequest.Email);

            if (login == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, login.password))
            {
                return Unauthorized(new { message = "Credenciales inválidas" });
            }

            var token = _authService.Authenticate(loginRequest.Email);
            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            var existingLogin = await _loginService.GetByEmailAsync(registerRequest.Email);
            if (existingLogin != null)
            {
                return BadRequest(new { message = "El correo ya está en uso." });
            }

            var newLogin = new Login
            {
                username = registerRequest.Email,
                password = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password)
            };

            await _loginService.CreateAsync(newLogin);

            return CreatedAtAction(nameof(Login), new { email = newLogin.username });
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}