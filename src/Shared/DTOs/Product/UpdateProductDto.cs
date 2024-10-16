using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DTOs.Product
{
    public record UpdateProductDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceDiscount { get; set; }
        public int Stock { get; set; }

        public int CategoryId { get; set; }

    }
}
