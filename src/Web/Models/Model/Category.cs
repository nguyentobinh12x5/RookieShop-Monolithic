using Web.Models.BaseModel;

namespace Web.Models.Model
{
    public class Category : BaseAuditableModel
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }
    }
}
