using System;

namespace Circle.Core.Entities.Concrete
{
    public class Category : AuditEntity
    {
        public Guid LanguageId { get; set; }
        public Guid LinkedCategoryId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconName { get; set; }
    }
}
