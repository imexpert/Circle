using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circle.Library.Entities.ComplexTypes
{
    public class UpdateProuctModel
    {
        public bool IsImageExist { get; set; }
        public string UpdateProductId { get; set; }
        public string UpdateProductCategoryId { get; set; }
        public string UpdateProductName { get; set; }
        public string UpdateProductDescription { get; set; }
        public byte[] Image { get; set; }
    }
}
