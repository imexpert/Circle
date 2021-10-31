using Circle.Helper.Bootstrap.Grid.Button;
using Circle.Helper.Bootstrap.Grid.Column;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Circle.Helper.Bootstrap.Grid
{
    public class Grid<T> : HtmlString where T : class
    {
        #region properties

        private string id;
        private string name;
        private string cssIcon;
        private bool sort;
        private bool isHeader;
        private bool isFooter;
        private Expression<Func<T, object>> pkColumn;
        private string deleteUrl;
        private string updateUrl;
        private string headerTemplate;
        private string rowTemplate;
        private IHtmlHelper _helper;
        private IEnumerable<T> DataSource { get; set; }
        private List<GridButton> _toolbarButtons = new List<GridButton>();
        private List<GridButton> _rowButtons = new List<GridButton>();
        private List<GridColumn<T>> _columns = new List<GridColumn<T>>();

        #endregion

        #region constructor

        public Grid(IHtmlHelper helper, IEnumerable<T> dataSource)
        {
            _helper = helper;
            isHeader = true;
            isFooter = true;
            DataSource = dataSource;
        }

        #endregion

        #region properties fluent

        public Grid<T> ID(string id = "")
        {
            this.id = id;
            return this;
        }

        public Grid<T> Name(string name)
        {
            this.name = name;
            return this;
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

        public Grid<T> ToolbarButtons(Action<GridButtonBuilder> btnBuilder)
        {
            var builder = new GridButtonBuilder();
            btnBuilder(builder);
            foreach (var button in builder)
            {
                _toolbarButtons.Add(button);
            }
            return this;
        }

        public Grid<T> RowButtons(Action<GridButtonBuilder> btnBuilder)
        {
            var builder = new GridButtonBuilder();
            btnBuilder(builder);
            foreach (var button in builder)
            {
                _rowButtons.Add(button);
                if (button.ButtonType == GridButtonType.update && !button.ActionUrl.IsBlank())
                {
                    updateUrl = button.ActionUrl;
                }
                else if (button.ButtonType == GridButtonType.delete && !button.ActionUrl.IsBlank())
                {
                    deleteUrl = button.ActionUrl;
                }
            }
            return this;
        }

        public Grid<T> Sort(bool sort = true)
        {
            this.sort = sort;
            return this;
        }

        public Grid<T> CssIcon(string cssIcon = "")
        {
            this.cssIcon = cssIcon;
            return this;
        }

        public Grid<T> DeleteURL(string deleteUrl)
        {
            this.deleteUrl = deleteUrl;
            return this;
        }

        public Grid<T> UpdateURL(string updateUrl)
        {
            this.updateUrl = updateUrl;
            return this;
        }

        public Grid<T> HasHeader(bool hasHeader)
        {
            this.isHeader = hasHeader;
            return this;
        }

        public Grid<T> HasFooter(bool hasFooter)
        {
            this.isFooter = hasFooter;
            return this;
        }

        public Grid<T> HeaderTemplate(string headerTemplate)
        {
            this.headerTemplate = headerTemplate;
            return this;
        }

        public Grid<T> RowTemplate(string rowTemplate)
        {
            this.rowTemplate = rowTemplate;
            return this;
        }

        #endregion

        #region return html to view

        public override string ToString()
        {
            return ToHtmlString();
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

            if (string.IsNullOrEmpty(name))
                name = "Liste";

            if (string.IsNullOrEmpty(cssIcon))
                cssIcon = "fa-gear";

            sb.Append("<div class='panel panel-default'>");
            if (isHeader)
            {
                sb.AppendFormat("<div class='panel-heading'><div class='panel-title'><i class='fa {0}'></i>&nbsp;<span>{1}</span>", cssIcon, name);

                if (_toolbarButtons.Count > 0)
                {
                    sb.Append("<div class='pull-right'>");
                    foreach (var item in _toolbarButtons)
                    {
                        sb.Append(item.ToString().Replace("btn-icon-sm", "btn-toolbar-sm"));
                    }
                    sb.Append("</div>");
                }

                sb.Append("</div></div>");
            }
            sb.Append("<div class='panel-body xs-padding'><div class='table-responsive'>");
            sb.AppendFormat("<table id='{0}' class='table table-hover no-margin bsDataTable' ", id);
            sb.AppendFormat("data-update='{0}'", updateUrl ?? "");
            sb.AppendFormat("data-delete='{0}'", deleteUrl ?? "");
            sb.Append(">");

            if (headerTemplate.IsBlank())
            {
                sb.Append("<colgroup>");
                if (_rowButtons.Count > 0)
                {
                    sb.Append("<col class='col-xs-1'>");
                }

                for (int i = 0; i < _columns.Count(c => c.Hidden == false); i++)
                {
                    sb.Append("<col>");
                }
                sb.Append("</colgroup>");

                sb.Append("<thead><tr>");
                if (_rowButtons.Count > 0)
                {
                    sb.Append("<th></th>");
                }

                foreach (var columnHeader in _columns.Where(w => w.Hidden == false))
                {
                    var propInfo = Util.GetPropertyInfo<T>(columnHeader.expression);
                    var displayName = Util.GetDisplayName<T>(propInfo);
                    sb.AppendFormat("<th>{0}</th>", displayName);
                }

                sb.Append("</tr></thead>");
            }
            else
            {
                sb.Append(headerTemplate);
            }
            sb.Append("<tbody>");

            System.Type type = typeof(T);

            foreach (var item in this.DataSource)
            {
                var pkPropInfo = Util.GetPropertyInfo<T>(pkColumn);
                if (rowTemplate.IsBlank())
                {

                    sb.AppendFormat("<tr data-id='{0}'", item.GetPropertyValue(pkPropInfo.Name));

                    foreach (var columnName in _columns.Where(w => w.Hidden == true))
                    {
                        string name = "";
                        if (columnName.expression.Body.ToString().Count(f => f == '.') == 1)
                        {
                            var cPropInfo = Util.GetPropertyInfo<T>(columnName.expression);
                            name = cPropInfo.Name;
                        }
                        else
                        {
                            var pos = columnName.expression.Body.ToString().IndexOf(".");
                            name = columnName.expression.Body.ToString().Remove(0, pos + 1);
                        }

                        var att = (DisplayFormatAttribute)type.GetProperty(name).GetCustomAttributes(typeof(DisplayFormatAttribute), true).SingleOrDefault();
                        if (att == null)
                            att = new DisplayFormatAttribute();

                        sb.AppendFormat(" data-{0}='{1}'", name, item.GetPropertyValueEx(name, att.NullDisplayText, att.DataFormatString));
                    }
                    sb.Append(">");

                    if (_rowButtons.Count > 0)
                    {
                        sb.Append("<td>");
                        foreach (var btnItem in _rowButtons)
                        {
                            sb.Append(btnItem.ToString());
                        }
                        sb.Append("</td>");
                    }

                    foreach (var columnName in _columns.Where(w => w.Hidden == false))
                    {
                        string name = "";
                        if (columnName.expression.Body.ToString().Count(f => f == '.') == 1)
                        {
                            var cPropInfo = Util.GetPropertyInfo<T>(columnName.expression);
                            name = cPropInfo.Name;
                        }
                        else
                        {
                            var pos = columnName.expression.Body.ToString().IndexOf(".");
                            name = columnName.expression.Body.ToString().Remove(0, pos + 1);
                        }



                        var att = (DisplayFormatAttribute)type.GetProperty(name).GetCustomAttributes(typeof(DisplayFormatAttribute), true).SingleOrDefault();
                        if (att == null)
                            att = new DisplayFormatAttribute();

                        sb.AppendFormat("<td>{0}</td>", item.GetPropertyValueEx(name, att.NullDisplayText, att.DataFormatString));
                    }
                }
                else
                {
                    sb.AppendFormat("<tr data-id='{0}'>", item.GetPropertyValue(pkPropInfo.Name));

                    if (_rowButtons.Count > 0)
                    {
                        sb.Append("<td>");
                        foreach (var btnItem in _rowButtons)
                        {
                            sb.Append(btnItem.ToString());
                        }
                        sb.Append("</td>");
                    }

                    string[] args = new string[_columns.Count];

                    int i = 0;
                    foreach (var columnName in _columns.Where(w => w.Hidden == false))
                    {
                        string name = "";
                        if (columnName.expression.Body.ToString().Count(f => f == '.') == 1)
                        {
                            var cPropInfo = Util.GetPropertyInfo<T>(columnName.expression);
                            name = cPropInfo.Name;
                        }
                        else
                        {
                            var pos = columnName.expression.Body.ToString().IndexOf(".");
                            name = columnName.expression.Body.ToString().Remove(0, pos + 1);
                        }
                        args[i] = item.GetPropertyValue(name).ToString();

                        i++;
                    }

                    sb.AppendFormat(rowTemplate, args);
                }
                sb.Append("</tr>");
            }
            sb.Append("</tbody></table></div></div>");
            if (isFooter)
            {
                sb.Append("<div class='panel-footer' style='min-height:54px'>");
                sb.Append(_helper.Pager("", new PageInfo() { RecordCount = (this.DataSource as PagedList<T>).RecordCount, PageNumber = (this.DataSource as PagedList<T>).PageNumber, PageSize = (this.DataSource as PagedList<T>).PageSize }, "pull-right no-margin").ToString());
                sb.Append("</div>");
            }
            sb.Append("</div>");

            return sb.ToString();
        }



        #endregion
    }
}
