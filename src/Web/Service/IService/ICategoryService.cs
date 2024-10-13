using Web.Commom;
using Shared.DTOs.Category;

namespace Web.Service.IService
{
    public interface ICategoryService
    {
        Task<PaginatedList<CategoryDto>> GetAllCategories(GetWithPaginationQuery query, CancellationToken cancellationToken);
        Task<CategoryDto> GetCategoryById(int CategoryId, CancellationToken cancellationToken);

        Task<CategoryDto> CreateCategory(CategoryDto CategoryDto, CancellationToken cancellationToken);

        Task<CategoryDto> UpdateCategory(int CategoryId, CategoryDto CategoryDto, CancellationToken cancellationToken);

        Task<bool> DeleteCategory(int CategoryId, CancellationToken cancellationToken);
    }
}
