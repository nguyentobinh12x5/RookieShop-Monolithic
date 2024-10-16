using Shared.DTOs.Category;
using Web.Commom.Request;
using Web.Commom.Response;

namespace Web.Service.IService
{
    public interface ICategoryService
    {
        Task<PaginatedList<CategoryDto>> GetAllCategories(GetWithPaginationQuery query, CancellationToken cancellationToken);
        Task<CategoryDto> GetCategoryById(int CategoryId, CancellationToken cancellationToken);

        Task<CategoryDto> CreateCategory(CreateCategoryDto request, CancellationToken cancellationToken);

        Task<CategoryDto> UpdateCategory(int CategoryId, UpdateCategoryDto CategoryDto, CancellationToken cancellationToken);

        Task<bool> DeleteCategory(int CategoryId, CancellationToken cancellationToken);
    }
}
