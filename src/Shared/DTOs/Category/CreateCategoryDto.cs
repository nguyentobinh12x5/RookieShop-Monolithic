using Microsoft.AspNetCore.Http;

namespace Shared.DTOs.Category
{
    public record CreateCategoryDto
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public IFormFile? Image { get; set; }
    }
}
