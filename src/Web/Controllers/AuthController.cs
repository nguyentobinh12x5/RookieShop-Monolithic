using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Auth;
using Shared.DTOs.User;
using Web.Identity.IIdentity;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto request)
        {
            var (result, loginResponse) = await _identityService.LoginAsync(request);

            if (result.Succeeded)
            {
                return Ok(loginResponse);
            }

            return BadRequest(result);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            await _identityService.LogOut();

            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(CreateUserDto request)
        {
            var result = await _identityService.CreateUserAsync(request);

            if (result.Result.Succeeded)
            {
                return Ok(result.UserId);
            }

            return BadRequest(result.Result);
        }
    }
}
