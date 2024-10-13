using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Shared.DTOs.Auth;
using Shared.DTOs.User;
using Web.Commom;
using Web.Constants;
using Web.Extensions;
using Web.Identity.IIdentity;

namespace Web.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
            IAuthorizationService authorizationService,
            SignInManager<ApplicationUser> signInManager,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
            _signInManager = signInManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }


        public async Task<Result> LogOut()
        {
            await _signInManager.SignOutAsync();

            return Result.Success();
        }

        public async Task<(Result, LoginResponseDto loginresponse)> LoginAsync(LoginRequestDto request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                return (Result.Failure(new[] { "User does not exist." }), new LoginResponseDto() { User = null, Token = "" });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded)
            {
                return (Result.Failure(new[] { "Invalid password" }), new LoginResponseDto() { User = null, Token = "" });
            }

            UserDto userDto = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address
            };

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            LoginResponseDto loginResponse = new LoginResponseDto
            {
                User = userDto,
                Token = token
            };

            var success = IdentityResult.Success;

            return (success.ToApplicationResult(), loginResponse);
        }

        public async Task<string?> GetUserNameAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            return user?.UserName;
        }

        public async Task<(Result Result, string UserId)> CreateUserAsync(CreateUserDto create)
        {
            var user = new ApplicationUser
            {
                UserName = create.UserName,
                Email = create.Email,
                FirstName = create.FirstName,
                LastName = create.LastName,
                Address = create.Address
            };

            var result = await _userManager.CreateAsync(user, create.Password);

            if (result.Succeeded && string.IsNullOrEmpty(create.Role))
            {
                await _userManager.AddToRoleAsync(user, Roles.Customer);
            }
            else if (result.Succeeded && !string.IsNullOrEmpty(create.Role))
            {
                await _userManager.AddToRoleAsync(user, create.Role);
            }

            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);

            return user != null && await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return false;
            }

            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

            var result = await _authorizationService.AuthorizeAsync(principal, policyName);

            return result.Succeeded;
        }

        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            return user != null ? await DeleteUserAsync(user) : Result.Success();
        }

        public async Task<Result> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }
    }
}
