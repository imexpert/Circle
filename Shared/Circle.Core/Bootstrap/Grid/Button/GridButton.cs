using Circle.Core.Extensions;

namespace Circle.Core.Bootstrap.Grid.Button
{
    public class GridButton : IGridButton
    {
        private string _id;
        private string _title;
        private string _tooltip;
        private string _data;
        private string _actionURL;
        private string _cssClass;
        private string _icon;
        private GridButtonType _buttonType;


        public GridButton(GridButtonType buttonType)
        {
            _buttonType = buttonType;
        }

        public string ID { get { return _id; } }

        public string Title { get { return _title; } }

        public string Tooltip { get { return _tooltip; } }

        public string ActionUrl { get { return _actionURL; } }

        public string CssClass { get { return _cssClass; } }

        public string Icon { get { return _icon; } }

        public GridButtonType ButtonType { get { return _buttonType; } }

        IGridButton IGridButton.ID(string id)
        {
            _id = id;
            return this;
        }

        IGridButton IGridButton.Title(string title)
        {
            _title = title;
            return this;
        }

        IGridButton IGridButton.Tooltip(string tooltip)
        {
            _tooltip = tooltip;
            return this;
        }

        IGridButton IGridButton.ActionUrl(string actionUrl)
        {
            _actionURL = actionUrl;
            return this;
        }

        IGridButton IGridButton.CssClass(string cssClass)
        {
            _cssClass = cssClass;
            return this;
        }

        IGridButton IGridButton.Icon(string icon)
        {
            _icon = icon;
            return this;
        }

        IGridButton IGridButton.ButtonType(GridButtonType buttonType)
        {
            _buttonType = buttonType;
            return this;
        }

        public override string ToString()
        {
            string idContent = "";
            string tooltipContent = "";
            string dataContent = "";
            string iconContent = "";
            string actionContent = "";
            string typeContent = "";
            string btnColorCss = "btn-default";

            #region add tooltip
            if (_tooltip.IsBlank())
            {
                switch (_buttonType)
                {
                    case GridButtonType.insert:
                        _tooltip = "Kayıt Ekle";
                        break;
                    case GridButtonType.update:
                        _tooltip = "Kayıt Güncelle";
                        break;
                    case GridButtonType.delete:
                        _tooltip = "Kayıt Sil";
                        break;
                    case GridButtonType.print:
                        _tooltip = "Yazdır";
                        break;
                }
            }
            #endregion

            #region add icon
            if (_icon.IsBlank())
            {
                switch (_buttonType)
                {
                    case GridButtonType.insert:
                        _icon = "fa-plus";
                        break;
                    case GridButtonType.update:
                        _icon = "fa-edit";
                        break;
                    case GridButtonType.delete:
                        _icon = "fa-trash";
                        break;
                    case GridButtonType.print:
                        _icon = "fa-print";
                        break;
                }
            }
            #endregion

            #region add html attributes
            if (!_id.IsBlank())
                idContent = string.Format(" id='{0}'", _id);

            if (!_actionURL.IsBlank())
                actionContent = string.Format(" data-action='{0}'", _actionURL);

            if (!_tooltip.IsBlank())
                tooltipContent = string.Format(" title='{0}'", _tooltip);

            if (!_data.IsBlank())
                dataContent = string.Format(" data-grid='{0}'", _data);
            #endregion

            #region add button color
            switch (_buttonType)
            {
                case GridButtonType.insert:
                    btnColorCss = "btn-default";
                    break;
                case GridButtonType.update:
                    btnColorCss = "btn-primary";
                    break;
                case GridButtonType.delete:
                    btnColorCss = "btn-danger";
                    break;
                case GridButtonType.export:
                    btnColorCss = "btn-info";
                    break;
                case GridButtonType.print:
                    btnColorCss = "btn-info";
                    break;
            }
            #endregion

            if (_title.IsBlank())
            {
                if (_icon.IsBlank())
                    _icon = "fa-gear";

                iconContent = string.Format("<i class='fa {0}'></i>", _icon);
            }
            else
            {
                if (!_icon.IsBlank())
                    iconContent = string.Format("<i class='fa {0} xs-rmargin'></i>", _icon);
            }

            if (_buttonType != GridButtonType.custom)
                typeContent = _buttonType.ToString("G") + "item";

            return string.Format("<a{0} href='#' {1} class='btn btn-icon-sm {2} {3} {4} xx-lmargin' {5} {6}>{7}{8}</a>"
                , idContent, actionContent, _cssClass, typeContent, btnColorCss, tooltipContent, dataContent, iconContent, _title);
        }

    }
}
