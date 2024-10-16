using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DTOs.Product
{
    public record ProductDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
        public string? Description { get; init; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; init; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceDiscount { get; init; }
        public int Stock { get; init; }
        public List<string> ImageUrls { get; init; } = new List<string>();

        public int CategoryId { get; init; }
    }
}
