using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Web.Identity
{
    public class SignInManager : SignInManager<ApplicationUser>
    {
        public SignInManager(
        UserManager<ApplicationUser> userManager,
        IHttpContextAccessor contextAccessor,
        IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
        IOptions<IdentityOptions> optionsAccessor,
        ILogger<SignInManager<ApplicationUser>> logger,
        IAuthenticationSchemeProvider schemes,
        IUserConfirmation<ApplicationUser> confirmation) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
        }

        public override async Task SignInAsync(ApplicationUser user, bool isPersistent, string? authenticationMethod = null)
        {
            await base.SignInAsync(user, isPersistent, authenticationMethod);
        }

        public override async Task SignOutAsync()
        {
            await base.SignOutAsync();
        }
    }
}
