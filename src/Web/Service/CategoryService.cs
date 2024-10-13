using Web.Commom;
using Web.Models.Model;
using Web.Service.IService;
using Shared.DTOs.Category;

namespace Web.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IApplicationDbContext _context;

        private readonly IMapper _mapper;
        public CategoryService(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public async Task<CategoryDto> CreateCategory(CategoryDto categoryDto, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(categoryDto);

            _context.Categories.Add(category);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> UpdateCategory(int categoryId, CategoryDto categoryDto, CancellationToken cancellationToken)
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
