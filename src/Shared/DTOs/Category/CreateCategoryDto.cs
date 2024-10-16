using Microsoft.AspNetCore.Http;

namespace Shared.DTOs.Category
{
    public record CreateCategoryDto
    {
        public string Name { get; init; } = null!;

        public string? Description { get; init; }

        public IFormFile? Image { get; init; }
    }
}
