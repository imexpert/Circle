using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circle.Core.Entities.Concrete
{
    public class Department : AuditEntity
    {
        public Guid LanguageId { get; set; }
        public string Title { get; set; }
        public bool Status { get; set; }
        public Language Language { get; set; }
    }
}
