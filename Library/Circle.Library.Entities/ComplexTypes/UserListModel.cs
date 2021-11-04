using Circle.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circle.Library.Entities.ComplexTypes
{
    public class UserListModel
    {
        public List<User> UserList { get; set; }
        public List<Department> DepartmentList { get; set; }
    }
}
