using System;

namespace Circle.Core.Entities.Concrete
{
    public class AuditEntity : IEntity
    {
        public Guid Id { get; set; }
        public string RecordUsername { get; set; }
        public DateTime RecordDate { get; set; }
        public string UpdateUsername { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Ip { get; set; }
    }
}
