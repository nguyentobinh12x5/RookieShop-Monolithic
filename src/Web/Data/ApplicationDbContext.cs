using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Web.Identity;
using Web.Models.Model;
using Web.Service.IService;

namespace Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; } = null!;
    }
}
