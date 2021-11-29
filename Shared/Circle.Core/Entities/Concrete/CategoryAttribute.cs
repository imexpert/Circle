using System;

namespace Circle.Core.Entities.Concrete
{
    public class CategoryAttribute : AuditEntity
    {
        public Guid? LinkedAttributeId { get; set; }
        public Guid CategoryId { get; set; }
        public string Code { get; set; }
        public int TypeCode { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
    }
}
