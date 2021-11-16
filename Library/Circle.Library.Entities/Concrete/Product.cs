using Circle.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circle.Library.Entities.Concrete
{
    public class Product : AuditEntity
    {
        public Guid CategoryId { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }
    }
}
