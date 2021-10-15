using Circle.Core.Entities;
using Circle.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circle.Library.Entities.Concrete
{
    public class Category : AuditEntity
    {
        public Guid LinkedCategoryId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconName { get; set; }
    }
}
