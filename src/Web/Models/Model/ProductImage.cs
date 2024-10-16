using Web.Models.BaseModel;

namespace Web.Models.Model
{
    public class ProductImage : BaseAuditableModel
    {
        public string ImageUrl { get; set; } = null!;

        public int ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;
    }

}
