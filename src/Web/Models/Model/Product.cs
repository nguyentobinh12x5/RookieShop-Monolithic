using System.ComponentModel.DataAnnotations.Schema;
using Web.Models.BaseModel;

namespace Web.Models.Model
{
    public class Product : BaseAuditableModel
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceDiscount { get; set; }
        public int Stock { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;

        public IList<ProductImage> ProductImages { get; private set; } = new List<ProductImage>();
    }
}
