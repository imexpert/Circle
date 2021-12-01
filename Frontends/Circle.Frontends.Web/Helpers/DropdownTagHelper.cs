using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circle.Frontends.Web.Helpers
{
    [HtmlTargetElement("Dropdown")]
    public class DropdownTagHelper : TagHelper
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string TextField { get; set; }
        public string ValueField { get; set; }
        public string ChildName { get; set; }
        public string ParamName { get; set; }
        public string QueryString { get; set; }
        public string ParentModal { get; set; }
        public string PlaceHolder { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagMode = TagMode.StartTagAndEndTag;

            var sb = new StringBuilder();
            sb.AppendFormat(
                "<select " +
                "style='width:100%'" +
                "class='customSelect form-select form-select-sm rounded-start-0' " +
                "name='{0}' " +
                "id='{0}' " +
                "data-placeholder='{8}' " +
                "data-dropdownParent='{7}' " +
                "data-url='{1}' " +
                "data-childName='{4}'" +
                "data-text='{2}' " +
                "data-value='{3}' " +
                "data-allowClear='true' " +
                "data-paramName='{5}' data-queryString='{6}' >", Name, Url, TextField, ValueField, ChildName, ParamName, QueryString, ParentModal, PlaceHolder);
            sb.Append("</select>");

            output.PreContent.SetHtmlContent(sb.ToString());
            base.Process(context, output);
        }
    }
}
