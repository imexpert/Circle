using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circle.Frontends.Web.Helpers
{
    [HtmlTargetElement("CircleSelect")]
    public class CustomDropdownTagHelper : TagHelper
    {
        [HtmlAttributeName("Name")]
        public string Name { get; set; }

        [HtmlAttributeName("PlaceHolder")]
        public string PlaceHolder { get; set; }

        [HtmlAttributeName("Url")]
        public string Url { get; set; }

        [HtmlAttributeName("ParentModal")]
        public string ParentModal { get; set; }

        [HtmlAttributeName("QueryParam")]
        public string QueryParam { get; set; }

        [HtmlAttributeName("TextField")]
        public string TextField { get; set; }

        [HtmlAttributeName("ValueField")]
        public string ValueField { get; set; }

        [HtmlAttributeName("ChildControlId")]
        public string ChildControlId { get; set; }

        [HtmlAttributeName("ParamName")]
        public string ParamName { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "";
            output.TagMode = TagMode.StartTagAndEndTag;

            var sb = new StringBuilder();

            sb.Append("<select ");

            if (!string.IsNullOrEmpty(this.Name))
            {
                sb.Append($"id='{this.Name}' ");
                sb.Append($"name='{this.Name}' ");
            }

            sb.Append("class='customSelect form-select form-select-sm rounded-start-0' ");

            if (!string.IsNullOrEmpty(this.ParentModal))
            {
                sb.Append($"parent-modal='{this.ParentModal}' ");
            }

            if (!string.IsNullOrEmpty(this.Url))
            {
                sb.Append($"url='{this.Url}' ");
            }

            if (!string.IsNullOrEmpty(this.PlaceHolder))
            {
                sb.Append($"place-holder='{this.PlaceHolder}' ");
            }

            if (!string.IsNullOrEmpty(this.QueryParam))
            {
                sb.Append($"query-param='{this.QueryParam}' ");
            }

            if (!string.IsNullOrEmpty(this.TextField))
            {
                sb.Append($"text-field='{this.TextField}' ");
            }

            if (!string.IsNullOrEmpty(this.ValueField))
            {
                sb.Append($"value-field='{this.ValueField}' ");
            }

            if (!string.IsNullOrEmpty(this.ChildControlId))
            {
                sb.Append($"child-kontrol-id='{this.ChildControlId}' ");
            }

            if (!string.IsNullOrEmpty(this.ParamName))
            {
                sb.Append($"param-name='{this.ParamName}' ");
            }

            sb.Append(" ></select>");

            output.Content.SetHtmlContent(sb.ToString());
            base.Process(context, output);
        }
    }
}
