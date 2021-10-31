using Circle.Core.Utilities.Types;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circle.Core.Bootstrap.Grid
{
    public static class PagerHelper
    {
        public static HtmlString Pager(this IHtmlHelper helper, string id, PageInfo pageInfo, string cssClass = "")
        {
            StringBuilder sb = new StringBuilder();
            int pgStart = 1;
            int pgEnd = 1;

            if (pageInfo.PageCount <= 1)
                cssClass = "hidden";

            if (string.IsNullOrEmpty(pageInfo.SortColumn))
                pageInfo.SortColumn = "-";

            sb.AppendFormat("<ul id='{0}' class='pagination {1}' data-sort='{2}' data-isasc='{3}' data-pagesize='{4}'>", id, cssClass, pageInfo.SortColumn, pageInfo.IsAsc, pageInfo.PageSize);


            if (pageInfo.PageNumber > pageInfo.PageCount || pageInfo.PageNumber < 1)
                pageInfo.PageNumber = 1;

            pgStart = pageInfo.PageNumber - 2;
            pgEnd = pageInfo.PageNumber + 2;

            if (pgStart <= 1)
            {
                pgStart = 1;
                pgEnd = 5;
            }

            if (pgEnd > pageInfo.PageCount)
                pgEnd = pageInfo.PageCount;

            if (pageInfo.PageNumber == 1)
                sb.Append("<li class=\"disabled\"><a href=\"#\">&laquo;</a></li>");
            else
                sb.Append("<li data-pg=\"1\"><a href=\"#\">&laquo;</a></li>");

            for (int i = pgStart; i <= pgEnd; i++)
            {
                if (i == pageInfo.PageNumber)
                    sb.AppendFormat("<li class=\"active\"><a href=\"#\">{0}</a></li>", i);
                else
                    sb.AppendFormat("<li data-pg=\"{0}\"><a href=\"#\">{0}</a></li>", i);
            }

            if (pageInfo.PageNumber == pageInfo.PageCount)
                sb.Append("<li class=\"disabled\"><a href=\"#\">&raquo;</a></li>");
            else
                sb.AppendFormat("<li data-pg=\"{0}\" title=\"{0}\"><a href=\"#\">&raquo;</a></li>", pageInfo.PageCount);

            sb.AppendFormat("<li><a href=\"#\"># {0}</a></li>", pageInfo.RecordCount);
            sb.Append("</ul>");

            return new HtmlString(sb.ToString());
        }
    }
}
