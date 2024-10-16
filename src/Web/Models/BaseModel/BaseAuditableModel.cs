﻿namespace Web.Models.BaseModel
{
    public class BaseAuditableModel : BaseModel
    {
        public DateTimeOffset Created { get; set; } = DateTimeOffset.UtcNow;

        public string? CreatedBy { get; set; }

        public DateTimeOffset LastModified { get; set; }

        public string? LastModifiedBy { get; set; }
    }
}
