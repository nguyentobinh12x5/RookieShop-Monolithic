using Web.Models.Model;

namespace Web.Service.IService
{
    public interface IApplicationDbContext
    {
        DbSet<Category> Categories { get; }

        DbSet<Product> Products { get; }

        DbSet<ProductImage> ProductImages { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
