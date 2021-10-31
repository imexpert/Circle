using Circle.Core.Bootstrap.Grid.Button;
using Circle.Core.Bootstrap.Grid.Column;
using Circle.Core.Extensions;
using Circle.Core.Utilities.Types;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;

namespace Circle.Core.Bootstrap.Grid
{
    public class Grid<T> : IHtmlContent where T : class
    {
        #region properties

        private string id;
        private string name;
        private string cssIcon;
        private bool sort;
        private bool isHeader;
        private bool isToolbar;
        private bool isToolbarSearchInput;
        private string toolbarSearchInputPlaceholder;
        private bool isToolbarAddButton;
        private string toolbarAddButtonText;
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
            isToolbar = true;
            isHeader = true;
            isFooter = true;
            isToolbarSearchInput = true;
            isToolbarAddButton = true;
            toolbarAddButtonText = "Yeni Ekle";
            toolbarSearchInputPlaceholder = string.Empty;
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

        public Grid<T> HasToolbar(bool hasToolbar)
        {
            this.isToolbar = hasToolbar;
            return this;
        }

        public Grid<T> HasToolbarSearchInput(bool hasToolbarSearchInput)
        {
            this.isToolbarSearchInput = hasToolbarSearchInput;
            return this;
        }

        public Grid<T> SetToolbarSearchInputPlaceHolder(string toolbarSearchInputPlaceHolder)
        {
            this.toolbarSearchInputPlaceholder = toolbarSearchInputPlaceHolder;
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

            // Start Card
            sb.Append("<div class='card'>");

            if (isToolbar)
            {
                // Start Card-Header
                sb.Append("<div class='card-header border-0 pt-6'>");

                // Start Card-Title
                sb.Append("<div class='card-title'>");

                if (isToolbarSearchInput)
                {
                    // Start Card-Header-Search
                    sb.Append("<div class='d-flex align-items-center position-relative my-1'>");

                    sb.AppendFormat("<input type='text' data-kt-user-table-filter='search' class='form-control form-control-solid w-250px ps-14' placeholder='{0}' />", toolbarSearchInputPlaceholder);

                    // End Card-Header-Search
                    sb.Append("</div>");
                }

                // End Card-Title
                sb.Append("</div>");

                // Start Card toolbar
                sb.Append("<div class='card-toolbar'>");

                // Start Card toolbar - d-flex
                sb.Append("<div class='d-flex justify-content-end' data-kt-user-table-toolbar='base'>");

                //Start - Toolbar Button
                sb.AppendFormat("<button type='button' class='btn btn-primary'>{0}", toolbarAddButtonText);

                // End - Toolbar Button
                sb.Append("</button>");

                // End Card toolbar - d-flex
                sb.Append("</div>");

                // End Card toolbar
                sb.Append("</div>");

                // End Card-Header
                sb.Append("</div>");
            }

            //Start Card body
            sb.Append("<div class='card-body pt-0'>");

            //Start table-responsive
            sb.Append("<div class='table-responsive'>");

            //Start table
            sb.AppendFormat("<table class='table align-middle table-row-dashed fs-6 gy-5 dataTable no-footer' id='{0}'>", id);

            //Start thead
            sb.Append("<thead>");

            //Start thead-tr
            sb.Append("<tr>");

            //Seçilen kolonların eklenmesi
            foreach (var columnHeader in _columns.Where(w => w.Hidden == false))
            {
                var propInfo = Util.GetPropertyInfo<T>(columnHeader.expression);
                var displayName = Util.GetDisplayName<T>(propInfo);

                if (!string.IsNullOrEmpty(columnHeader.Title))
                    sb.AppendFormat("<th>{0}</th>", columnHeader.Title);
                else
                    sb.AppendFormat("<th>{0}</th>", displayName);
            }

            

            //End thead-tr
            sb.Append("</tr>");

            //End thead
            sb.Append("</thead>");

            //Start tbody
            sb.Append("<tbody>");

            sb.Append(GetColumnValues());

            //End tbody
            sb.Append("</tbody>");

            // End table
            sb.Append("</table>");

            // End table-responsive
            sb.Append("</div>");

            // End Card body
            sb.Append("</div>");

            // End Card
            sb.Append("</div>");

            return sb.ToString();
        }

        public string GetColumnValues()
        {
            StringBuilder sb = new StringBuilder();

            Type type = typeof(T);

            foreach (var item in this.DataSource)
            {
                var pkPropInfo = Util.GetPropertyInfo<T>(pkColumn);

                //Start tr
                sb.AppendFormat("<tr data-id='{0}'>", item.GetPropertyValue(pkPropInfo.Name));

                //Gizli olmayan kolon değerlerini alıyoruz.
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

                if (_rowButtons.Count > 0)
                {
                    sb.Append("<td class='text-end'>");



                    sb.Append("</td>");
                    sb.Append("<td>");
                    foreach (var btnItem in _rowButtons)
                    {
                        sb.Append(btnItem.ToString());
                    }
                    sb.Append("</td>");
                }

                //End tr
                sb.Append("</tr>");
            }

            return sb.ToString();
        }

        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            string aaa = ToHtmlString();
            writer.Write(aaa);
        }



        #endregion
    }
}
