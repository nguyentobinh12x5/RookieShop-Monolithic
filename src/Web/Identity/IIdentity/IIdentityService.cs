using Shared.DTOs.Auth;
using Shared.DTOs.User;
using Web.Commom.Response;

namespace Web.Identity.IIdentity
{
    public interface IIdentityService
    {
        Task<Result> LogOut();

        Task<(Result, LoginResponseDto loginresponse)> LoginAsync(LoginRequestDto request);

        Task<string?> GetUserNameAsync(string userId);

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<bool> AuthorizeAsync(string userId, string policyName);

        Task<(Result Result, string UserId)> CreateUserAsync(CreateUserDto create);

        Task<Result> DeleteUserAsync(string userId);
    }
}
