using System;
using System.Collections.Generic;

namespace Circle.Core.Entities.Concrete
{
    public class Department : AuditEntity
    {
        public Guid LanguageId { get; set; }
        public string Title { get; set; }
        public bool Status { get; set; }
    }
}
