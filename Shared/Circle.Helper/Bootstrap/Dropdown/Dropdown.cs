using Circle.Core.Utilities.IoC;
using Circle.Helper.Extensions;
using MediatR;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Circle.Helper.Bootstrap.Dropdown
{
    public class Dropdown<T> : IHtmlContent where T : class
    {
        private string id;
        private bool isMultipleSelect;
        private string placeHolderText;
        private Expression<Func<T, object>> textField;
        private Expression<Func<T, object>> valueField;
        private Expression<Func<T, object>> pkColumn;
        private IEnumerable<T> DataSource { get; set; }
        public Dropdown(IHtmlHelper helper, IEnumerable<T> dataSource)
        {
            DataSource = dataSource;
        }

        public Dropdown<T> ID(string id = "")
        {
            this.id = id;
            return this;
        }

        public Dropdown<T> IsMultiple(bool select)
        {
            this.isMultipleSelect = select;
            return this;
        }

        public Dropdown<T> SetPlaceHolder(string placeHolder)
        {
            this.placeHolderText = placeHolder;
            return this;
        }

        public Dropdown<T> PKColumn(Expression<Func<T, object>> pkColumn)
        {
            this.pkColumn = pkColumn;
            return this;
        }

        public Dropdown<T> SetTextField(Expression<Func<T, object>> textField)
        {
            this.textField = textField;
            return this;
        }

        public Dropdown<T> SetValueField(Expression<Func<T, object>> valueField)
        {
            this.valueField = valueField;
            return this;
        }
        /*
         * <select class="form-select select2-hidden-accessible" data-control="select2" data-placeholder="Select an option" data-select2-id="select2-data-1-5fao" tabindex="-1" aria-hidden="true">
        <option data-select2-id="select2-data-3-a3r7"></option>
        <option value="1" data-select2-id="select2-data-67-j9sr">Option 1</option>
        <option value="2" data-select2-id="select2-data-68-isax">Option 2</option>
        <option value="3" data-select2-id="select2-data-69-fg4d">Option 3</option>
        <option value="4" data-select2-id="select2-data-70-t9qz">Option 4</option>
        <option value="5" data-select2-id="select2-data-71-6ace">Option 5</option>
    </select>
         */

        public override string ToString()
        {
            return ToHtmlString();
        }

        public string ToHtmlString()
        {
            StringBuilder sb = new StringBuilder();
            if (id.IsBlank())
            {
                id = Guid.NewGuid().ToString();
            }

            string multiple = null;
            if (isMultipleSelect)
            {
                multiple = "Multiple";
            }

            //Start Select
            sb.AppendFormat("<select class='form-select select2-hidden-accessible' data-control='select2' data-placeholder='{0}' data-select2-id='select2-data-1-5fao' tabindex='-1' aria-hidden='true' id='{1}' {2}>", placeHolderText, id, multiple);

            sb.Append("<option data-select2-id='select2-data-3-a3r7'></option>");

            Type type = typeof(T);

            foreach (var item in this.DataSource)
            {
                //Start tr
                sb.AppendFormat("<option value='{0}' data-select2-id='select2-data-71-6ace'>", item.GetPropertyValue(valueField.Name));

                var att = (DisplayFormatAttribute)type.GetProperty(textField.Name).GetCustomAttributes(typeof(DisplayFormatAttribute), true).SingleOrDefault();
                if (att == null)
                    att = new DisplayFormatAttribute();

                sb.AppendFormat("{0}", item.GetPropertyValueEx(textField.Name, att.NullDisplayText, att.DataFormatString));
                sb.Append("</option>");
            }

            //End Select
            sb.Append("</select>");

            return sb.ToString();
        }
        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write(ToHtmlString());
        }
    }
}
