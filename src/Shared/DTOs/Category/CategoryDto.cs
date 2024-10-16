namespace Shared.DTOs.Category
{
    public record CategoryDto
    {
        public int Id { get; init; }

        public string Name { get; init; } = null!;

        public string? Description { get; init; }

        public string? ImageUrl { get; init; }
    }
}
