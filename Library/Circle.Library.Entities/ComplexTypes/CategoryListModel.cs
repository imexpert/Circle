using Circle.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circle.Library.Entities.ComplexTypes
{
    public class CategoryListModel
    {
        public string ProductCode { get; set; }
        public Category Category { get; set; }
        public List<Category> Categories { get; set; }
    }
}
