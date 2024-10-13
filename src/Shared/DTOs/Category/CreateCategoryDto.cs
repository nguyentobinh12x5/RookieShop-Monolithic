using Microsoft.AspNetCore.Http;

namespace Shared.DTOs.Category
{
    public class CreateCategoryDto
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public IFormFile Image { get; set; }
    }
}
