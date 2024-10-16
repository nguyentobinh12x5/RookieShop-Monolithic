using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DTOs.Product
{
    public record UpdateProductDto
    {
        public string Name { get; init; } = null!;
        public string? Description { get; init; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; init; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceDiscount { get; init; }
        public int Stock { get; init; }

        public int CategoryId { get; init; }

    }
}
