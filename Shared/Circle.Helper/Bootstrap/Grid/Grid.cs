using Circle.Helper.Bootstrap.Grid.Column;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Circle.Helper.Bootstrap.Grid
{
    public class Grid<T>
        where T : class
    {
        private string id;
        private List<GridColumn<T>> _columns = new List<GridColumn<T>>();
        private Expression<Func<T, object>> pkColumn;
        HtmlHelper _helper;
        IEnumerable<T> _dataSource;

        public Grid(HtmlHelper helper, IEnumerable<T> dataSource)
        {
            _helper = helper;
            _dataSource = dataSource;
        }

        public Grid<T> PKColumn(Expression<Func<T, object>> pkColumn)
        {
            this.pkColumn = pkColumn;
            return this;
        }

        public Grid<T> Columns(Action<GridColumnBuilder<T>> columnBuilder)
        {
            var builder = new GridColumnBuilder<T>();
            columnBuilder(builder);
            foreach (var column in builder)
            {
                _columns.Add(column);
            }
            return this;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public string ToHtmlString()
        {
            StringBuilder sb = new StringBuilder();

            CultureInfo turkey = new CultureInfo("tr-TR");
            Thread.CurrentThread.CurrentCulture = turkey;

            if (id.IsBlank())
            {
                id = Guid.NewGuid().ToString();
            }
        }
    }
}
