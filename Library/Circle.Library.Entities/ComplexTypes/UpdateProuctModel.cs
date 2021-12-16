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
        public string ProductId { get; set; }
        public string ProductCategoryId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public byte[] Image { get; set; }
    }
}
