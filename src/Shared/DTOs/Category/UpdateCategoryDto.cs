namespace Shared.DTOs.Category
{
    public record UpdateCategoryDto
    {
        public string Name { get; init; } = null!;

        public string? Description { get; init; }

        public string? ImageUrl { get; init; }

    }
}
