using Microsoft.AspNetCore.Mvc;
using WebApiSIA.Core.Application.Dtos.User;
using WebApiSIA.Core.Application.Interfaces.Services;

namespace WebApiSIA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var result = await _userService.LoginAsync(request.UserName, request.Password);

            if (result == null)
                return Unauthorized("Usuario o contraseña incorrectos.");

            return Ok(result);
        }

    }
}
