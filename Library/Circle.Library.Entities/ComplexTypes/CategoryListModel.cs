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
        public List<Category> ProductCodeList { get; set; }
        public Guid SelectedCategory { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public Category Category { get; set; }
        public List<Category> Categories { get; set; }
        public bool IsLastCategory { get; set; }
    }
}
