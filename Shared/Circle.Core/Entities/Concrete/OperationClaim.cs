using System;

namespace Circle.Core.Entities.Concrete
{
    public class OperationClaim : AuditEntity
    {
        public Guid LanguageId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
    }
}