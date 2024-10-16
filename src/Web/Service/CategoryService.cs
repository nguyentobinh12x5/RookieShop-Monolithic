using Shared.DTOs.Category;
using Web.Commom.Request;
using Web.Commom.Response;
using Web.Models.Model;
using Web.Service.IService;

namespace Web.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IApplicationDbContext _context;

        private readonly IMapper _mapper;

        private readonly CloudinaryService _cloudinaryService;
        public CategoryService(IApplicationDbContext context, IMapper mapper, CloudinaryService cloudinaryService)
        {
            _context = context;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<PaginatedList<CategoryDto>> GetAllCategories(GetWithPaginationQuery query, CancellationToken cancellationToken)
        {
            return await _context.Categories
                .OrderBy(x => x.Id)
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(query.PageNumber, query.PageSize);
        }

        public async Task<CategoryDto> GetCategoryById(int categoryId, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(categoryId);

            Guard.Against.NotFound(categoryId, category);

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> CreateCategory(CreateCategoryDto request, CancellationToken cancellationToken)
        {
            string imageUrl = null;

            if (request.Image != null && request.Image.Length > 0)
            {
                var uploadResult = await _cloudinaryService.UploadImageAsync(request.Image.OpenReadStream(), request.Image.FileName);
                if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    imageUrl = uploadResult.SecureUrl.ToString();
                }
            }

            var category = new Category
            {
                Name = request.Name,
                Description = request.Description,
                ImageUrl = imageUrl,
            };

            _context.Categories.Add(category);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> UpdateCategory(int categoryId, UpdateCategoryDto categoryDto, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(categoryId);

            Guard.Against.NotFound(categoryId, category);

            category = _mapper.Map(categoryDto, category);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<bool> DeleteCategory(int categoryId, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(categoryId);

            Guard.Against.NotFound(categoryId, category);

            _context.Categories.Remove(category);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
