using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Circle.Helper.Bootstrap.Grid
{
    public static class GridHelper
    {
        public static Grid<T> CircleTable<T>(this IHtmlHelper helper, IEnumerable<T> dataSource) 
            where T : class
        {
            return new Grid<T>(helper, dataSource);
        }
    }
}
