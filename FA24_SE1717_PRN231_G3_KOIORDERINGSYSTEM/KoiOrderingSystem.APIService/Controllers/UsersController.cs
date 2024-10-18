using KoiOrderingSystem.APIService.Request;
using KoiOrderingSystem.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KoiOrderingSystem.APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IConfiguration conf) => _userService ??= new UserService(conf);

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var msg = await _userService.Login(request.UsernameOrEmail, request.Password);

            return Ok(msg);
        }
    }
}
