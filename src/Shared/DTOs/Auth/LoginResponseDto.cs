using Shared.DTOs.User;

namespace Shared.DTOs.Auth
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
