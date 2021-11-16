using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circle.Web.Helper.Bootstrap.Grid
{
    public static class GridHelper
    {
        public static Grid<T> CustomTable<T>(this IHtmlHelper helper, IEnumerable<T> dataSource)
            where T : class
        {
            return new Grid<T>(helper, dataSource);
        }
    }
}
