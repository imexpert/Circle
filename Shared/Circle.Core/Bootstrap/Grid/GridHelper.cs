using Microsoft.AspNetCore.Html;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circle.Core.Bootstrap.Grid
{
    public static class GridHelper
    {
        public static IHtmlContent CircleTable<T>(this IHtmlHelper helper, IEnumerable<T> dataSource) where T : class
        {
            return new Grid<T>(helper, dataSource).ToHtmlString();
        }
    }
}
