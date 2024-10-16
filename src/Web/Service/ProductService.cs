using Shared.DTOs.Product;
using Web.Commom.Request;
using Web.Commom.Response;
using Web.Models.Model;
using Web.Service.IService;

namespace Web.Service
{
    public class ProductService : IProductService
    {
        private readonly IApplicationDbContext _context;

        private readonly IMapper _mapper;

        private readonly CloudinaryService _cloudinaryService;

        public ProductService(IApplicationDbContext context, IMapper mapper, CloudinaryService cloudinaryService)
        {

            _context = context;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;

        }

        public async Task<PaginatedList<ProductDto>> GetAllProducts(GetWithPaginationQuery query, CancellationToken cancellationToken)
        {

            return await _context.Products
                .OrderBy(x => x.Id)
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(query.PageNumber, query.PageSize);

        }

        public async Task<ProductDto> GetProductById(int productId, CancellationToken cancellationToken)
        {

            var product = await _context.Products.FindAsync(productId);

            Guard.Against.NotFound(productId, product);

            return _mapper.Map<ProductDto>(product);

        }

        public async Task<ProductDto> CreateProduct(CreateProductDto request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(request.CategoryId);

            Guard.Against.NotFound(request.CategoryId, category);

            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                PriceDiscount = request.PriceDiscount,
                Stock = request.Stock,
                CategoryId = request.CategoryId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync(cancellationToken);

            if (request.Images != null && request.Images.Count > 0)
            {
                var imageStreams = new List<Stream>();
                var fileNames = new List<string>();

                foreach (var image in request.Images)
                {
                    imageStreams.Add(image.OpenReadStream());
                    fileNames.Add(image.FileName);
                }

                var uploadResults = await _cloudinaryService.UploadListImagesAsync(imageStreams, fileNames);

                foreach (var result in uploadResults)
                {
                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var productImage = new ProductImage
                        {
                            ImageUrl = result.SecureUrl.ToString(),
                            ProductId = product.Id
                        };

                        _context.ProductImages.Add(productImage);
                    }
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> UpdateProduct(int productId, UpdateProductDto productDto, CancellationToken cancellationToken)
        {

            var product = await _context.Products.FindAsync(productId);

            Guard.Against.NotFound(productId, product);

            product = _mapper.Map(productDto, product);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ProductDto>(product);

        }

        public async Task<bool> DeleteProduct(int productId, CancellationToken cancellationToken)
        {

            var product = await _context.Products.FindAsync(productId);

            Guard.Against.NotFound(productId, product);

            _context.Products.Remove(product);


            await _context.SaveChangesAsync(cancellationToken);

            return true;

        }
    }
}
