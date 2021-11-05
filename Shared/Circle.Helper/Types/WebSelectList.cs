using Circle.Helper.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circle.Helper.Utilities.Types
{
    public class WebSelectList : IEnumerable<WebSelectListItem>
    {
        private List<WebSelectListItem> _webSelectList;
        public WebSelectList(IEnumerable list, string ValueField, string TextField, string ParentValueField = "")
        {
            _webSelectList = new List<WebSelectListItem>();
            WebSelectListItem tmp = null;
            foreach (var item in list)
            {
                tmp = new WebSelectListItem();
                if (!ParentValueField.IsBlank())
                {
                    tmp.ParentValue = item.GetPropertyValue(ParentValueField).ToString();
                }
                tmp.Value = item.GetPropertyValue(ValueField).ToString();
                tmp.Text = item.GetPropertyValue(TextField).ToString();

                _webSelectList.Add(tmp);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public virtual IEnumerator<WebSelectListItem> GetEnumerator()
        {
            return _webSelectList.GetEnumerator();
        }
    }
    public class WebSelectListItem
    {
        public string ParentValue { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
        public bool Selected { get; set; }
    }
}
