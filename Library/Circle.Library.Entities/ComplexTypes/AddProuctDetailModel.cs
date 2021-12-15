using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circle.Library.Entities.ComplexTypes
{
    public class AddProuctDetailModel
    {
        public Guid UpdateProductId { get; set; }
        public Guid SelectMaterial { get; set; }
        public Guid SelectMaterialDetail { get; set; }
        public Guid SelectLength { get; set; }
        public Guid SelectDiameter { get; set; }
    }
}
