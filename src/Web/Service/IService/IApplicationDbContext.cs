using Web.Models.Model;

namespace Web.Service.IService
{
    public interface IApplicationDbContext
    {
        DbSet<Category> Categories { get; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
