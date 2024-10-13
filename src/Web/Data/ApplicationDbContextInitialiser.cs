using Microsoft.AspNetCore.Identity;
using Web.Commom.Constants;
using Web.Identity;
using Web.Models.Model;

namespace Web.Data
{
    public static class InitialiserExtensions
    {
        public static async Task InitialiseDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

            await initialiser.InitialiseAsync();

            await initialiser.SeedAsync();
        }
    }
    public class ApplicationDbContextInitialiser
    {
        private readonly ILogger<ApplicationDbContextInitialiser> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                await _context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            //Default roles
            var administratorRole = new IdentityRole
            {
                Name = Roles.Administrator,
                NormalizedName = Roles.Administrator.ToUpper(),
            };
            var customerRole = new IdentityRole
            {
                Name = Roles.Customer,
                NormalizedName = Roles.Customer.ToUpper(),

            };

            if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await _roleManager.CreateAsync(administratorRole);
            }

            if (_roleManager.Roles.All(r => r.Name != customerRole.Name))
            {
                await _roleManager.CreateAsync(customerRole);
            }

            // Default users
            var administrator = new ApplicationUser
            {
                UserName = "administrator@localhost",
                Email = "administrator@localhost",
                FirstName = "Admin",
                LastName = "Admin",
                Address = "Admin Address"

            };
            var customer = new ApplicationUser
            {
                UserName = "customer@localhost",
                Email = "customer@localhost",
                FirstName = "Customer",
                LastName = "Customer",
                Address = "Customer Address"
            };

            if (_userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await _userManager.CreateAsync(administrator, "Administrator1!");
                if (!string.IsNullOrWhiteSpace(administratorRole.Name))
                {
                    await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
                }
            }

            if (_userManager.Users.All(u => u.UserName != customer.UserName))
            {
                await _userManager.CreateAsync(customer, "Customer1!");
                if (!string.IsNullOrWhiteSpace(customerRole.Name))
                {
                    await _userManager.AddToRolesAsync(customer, new[] { customerRole.Name });
                }
            }

            // Default data
            // Seed, if necessary
            if (!_context.Categories.Any())
            {
                _context.Categories.AddRange(
                new Category
                {
                    Name = "Category 1",
                    Description = "Description 1",
                    ImageUrl = "https://via.placeholder.com/150",
                },
                  new Category
                  {
                      Name = "Category 2",
                      Description = "Description 2",
                      ImageUrl = "https://via.placeholder.com/150",
                  }
                );

                await _context.SaveChangesAsync();
            }
        }
    }
}
