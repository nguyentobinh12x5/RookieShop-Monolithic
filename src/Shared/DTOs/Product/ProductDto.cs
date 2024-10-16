using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DTOs.Product
{
    public record ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceDiscount { get; set; }
        public int Stock { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();

        public int CategoryId { get; set; }
    }
}
