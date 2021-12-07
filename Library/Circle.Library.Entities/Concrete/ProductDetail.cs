using Circle.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circle.Library.Entities.Concrete
{
    public class ProductDetail : AuditEntity
    {
        public Guid ProductId { get; set; }
        public Guid Material { get; set; }
        public Guid MaterialDetail { get; set; }
        public Guid Diameter { get; set; }
        public Guid Length { get; set; }
    }
}
