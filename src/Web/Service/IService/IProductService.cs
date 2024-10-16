using Shared.DTOs.Product;
using Web.Commom.Request;
using Web.Commom.Response;

namespace Web.Service.IService
{
    public interface IProductService
    {
        Task<PaginatedList<ProductDto>> GetAllProducts(GetWithPaginationQuery query, CancellationToken cancellationToken);

        Task<ProductDto> GetProductById(int ProductId, CancellationToken cancellationToken);

        Task<ProductDto> CreateProduct(CreateProductDto request, CancellationToken cancellationToken);

        Task<ProductDto> UpdateProduct(int ProductId, UpdateProductDto ProductDto, CancellationToken cancellationToken);

        Task<bool> DeleteProduct(int ProductId, CancellationToken cancellationToken);

    }
}
