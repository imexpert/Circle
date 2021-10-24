using System;
using System.Linq.Expressions;

namespace Circle.Core.Bootstrap.Grid.Column
{
    public class GridColumn<T> : IGridColumn<T> where T : class
    {

        private string _title;
        private string _titleTooltip;
        private string _titleTemplate;
        private string _format;
        private string _template;
        private int _width;
        private bool _hidden = false;
        private bool _readonly = false;
        private bool _sortable = true;
        private bool _groupable = true;
        private bool _filterable = true;

        public Expression<Func<T, object>> expression;

        #region ctor

        public GridColumn(Expression<Func<T, object>> exp)
        {
            expression = exp;
        }

        #endregion

        #region Readonly Properties

        public string Title { get { return _title; } }

        public string TitleTooltip { get { return _titleTooltip; } }

        public string TitleTemplate { get { return _titleTemplate; } }

        public string Format { get { return _format; } }

        public string Template { get { return _template; } }

        public int Width { get { return _width; } }

        public bool Hidden { get { return _hidden; } }

        public bool Readonly { get { return _readonly; } }

        public bool Sortable { get { return _sortable; } }

        public bool Groupable { get { return _groupable; } }

        public bool Filterable { get { return _filterable; } }

        #endregion

        #region Fluent Methods

        IGridColumn<T> IGridColumn<T>.Title(string title)
        {
            _title = title;
            return this;
        }

        IGridColumn<T> IGridColumn<T>.TitleTooltip(string titleTooltip)
        {
            _titleTooltip = titleTooltip;
            return this;
        }

        IGridColumn<T> IGridColumn<T>.TitleTemplate(string titleTemplate)
        {
            _titleTemplate = titleTemplate;
            return this;
        }

        IGridColumn<T> IGridColumn<T>.Format(string format)
        {
            _format = format;
            return this;
        }

        IGridColumn<T> IGridColumn<T>.Template(string template)
        {
            _template = template;
            return this;
        }

        IGridColumn<T> IGridColumn<T>.Width(int width)
        {
            _width = width;
            return this;
        }

        IGridColumn<T> IGridColumn<T>.Hidden(bool isHidden)
        {
            _hidden = isHidden;
            return this;
        }

        IGridColumn<T> IGridColumn<T>.Readonly(bool isReadonly)
        {
            _readonly = isReadonly;
            return this;
        }

        IGridColumn<T> IGridColumn<T>.Sortable(bool isSortable)
        {
            _sortable = isSortable;
            return this;
        }

        IGridColumn<T> IGridColumn<T>.Groupable(bool isGroupable)
        {
            _groupable = isGroupable;
            return this;
        }

        IGridColumn<T> IGridColumn<T>.Filterable(bool isFilterable)
        {
            _filterable = isFilterable;
            return this;
        }

        #endregion
    }
}
