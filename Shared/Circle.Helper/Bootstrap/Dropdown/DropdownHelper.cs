using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circle.Helper.Bootstrap.Dropdown
{
    public static class DropdownHelper
    {
        public static Dropdown<T> CircleDropdown<T>(this IHtmlHelper helper, IEnumerable<T> dataSource)
            where T : class
        {
            return new Dropdown<T>(helper, dataSource);
        }
    }
}
