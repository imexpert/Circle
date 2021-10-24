using Circle.Core.Bootstrap.Grid.Column;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Circle.Core.Bootstrap.Grid
{
    public class Grid<T>
        where T : class
    {
        private string id;
        private List<GridColumn<T>> _columns = new List<GridColumn<T>>();
        private Expression<Func<T, object>> pkColumn;
        IHtmlHelper _helper;
        IEnumerable<T> _dataSource;

        public Grid(IHtmlHelper helper, IEnumerable<T> dataSource)
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

        public IHtmlContent ToHtmlString()
        {
            StringBuilder sb = new StringBuilder();

            var buildDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = buildDir + @"/Bootstrap/Grid/table.html";
            string table = File.ReadAllText(filePath);

            CultureInfo turkey = new CultureInfo("tr-TR");
            Thread.CurrentThread.CurrentCulture = turkey;

            return new HtmlString(table);
        }
    }
}
