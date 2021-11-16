using Circle.Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Circle.Web.Helper.Types
{
    public enum FilterType
    {
        Text,
        StartWith,
        EndWith,
        Like,
        Numeric,
        Date,
        Guid,
        Bool
    }

    public class SearchFilter
    {
        public SearchFilter()
        {
            IsNot = false;
        }

        public SearchFilter(string id, string value, FilterType filterType)
        {
            ID = id;
            Value = value;
            this.FilterType = filterType;
        }

        public SearchFilter(string id, string value, string value2, FilterType filterType)
        {
            ID = id;
            Value = value;
            Value2 = value2;
            this.FilterType = filterType;
        }

        public string ID { get; set; }

        public string Value { get; set; }

        public string Value2 { get; set; }

        public FilterType FilterType { get; set; }

        public bool IsNot { get; set; }
    }

    public static class SearchFilterHelper
    {
        public static List<SearchFilter> EmptySearchFilter()
        {
            return new List<SearchFilter>();
        }

        public static List<SearchFilter> GetSearchFilter(string filters)
        {
            List<SearchFilter> filter = new List<SearchFilter>();
            filter = JsonConvert.DeserializeObject<List<SearchFilter>>(filters);
            if (filter == null)
                filter = new List<SearchFilter>();

            return filter;
        }

        public static List<T> GetSearchFilterSelectedItems<T>()
        {
            List<T> result = null;

            var context = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            var formData = context.HttpContext.Request.Form;
            if (formData.Count > 0)
            {
                result = new List<T>();
                foreach (var item in formData.Keys)
                {
                    System.Type t = typeof(T);
                    t = Nullable.GetUnderlyingType(t) ?? t;

                    result.Add((item == null || DBNull.Value.Equals(item)) ? default(T) : (T)Convert.ChangeType(item, t));
                }
            }
            return result;
        }
    }
}
