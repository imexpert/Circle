using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circle.Library.Entities.ComplexTypes
{
    public class ProductDetailModel
    {
        public Guid ProductDetailId { get; set; }
        public string Material { get; set; }
        public string MaterialDetail { get; set; }
        public string Diameter { get; set; }
        public string Length { get; set; }
    }
}
