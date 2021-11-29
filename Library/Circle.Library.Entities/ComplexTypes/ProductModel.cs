using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circle.Library.Entities.ComplexTypes
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public string ImageString
        {
            get
            {
                if (Image != null && Image.Length > 0)
                {
                    return "data:image/jpeg;base64," + Convert.ToBase64String(Image);
                }

                return "";
            }
            set { }
        }
        public string Description { get; set; }
    }
}
