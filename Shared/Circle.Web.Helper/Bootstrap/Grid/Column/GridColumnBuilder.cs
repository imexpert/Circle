using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Circle.Web.Helper.Bootstrap.Grid.Column
{
    public class GridColumnBuilder<T> : IList<GridColumn<T>> where T : class
    {
        private readonly ModelMetadataProvider _metadataProvider;
        private readonly List<GridColumn<T>> _columns = new List<GridColumn<T>>();

        public GridColumnBuilder() 
        {

        }

        private GridColumnBuilder(ModelMetadataProvider metadataProvider)
        {
            _metadataProvider = metadataProvider;
        }

        public IGridColumn<T> Add(Expression<Func<T, object>> propertySpecifier)
        {
            var column = new GridColumn<T>(propertySpecifier);
            Add(column);

            return column;
        }

        protected IList<GridColumn<T>> Columns
        {
            get { return _columns; }
        }

        public IEnumerator<GridColumn<T>> GetEnumerator()
        {
            return _columns
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static MemberExpression GetMemberExpression(LambdaExpression expression)
        {
            return RemoveUnary(expression.Body) as MemberExpression;
        }

        private static System.Type GetTypeFromMemberExpression(MemberExpression memberExpression)
        {
            if (memberExpression == null) return null;

            var dataType = GetTypeFromMemberInfo(memberExpression.Member, (PropertyInfo p) => p.PropertyType);
            if (dataType == null) dataType = GetTypeFromMemberInfo(memberExpression.Member, (MethodInfo m) => m.ReturnType);
            if (dataType == null) dataType = GetTypeFromMemberInfo(memberExpression.Member, (FieldInfo f) => f.FieldType);

            return dataType;
        }

        private static System.Type GetTypeFromMemberInfo<TMember>(MemberInfo member, Func<TMember, System.Type> func) where TMember : MemberInfo
        {
            if (member is TMember)
            {
                return func((TMember)member);
            }
            return null;
        }

        private static Expression RemoveUnary(Expression body)
        {
            var unary = body as UnaryExpression;
            if (unary != null)
            {
                return unary.Operand;
            }
            return body;
        }

        protected virtual void Add(GridColumn<T> column)
        {
            _columns.Add(column);
        }

        void ICollection<GridColumn<T>>.Add(GridColumn<T> column)
        {
            Add(column);
        }

        void ICollection<GridColumn<T>>.Clear()
        {
            _columns.Clear();
        }

        bool ICollection<GridColumn<T>>.Contains(GridColumn<T> column)
        {
            return _columns.Contains(column);
        }

        void ICollection<GridColumn<T>>.CopyTo(GridColumn<T>[] array, int arrayIndex)
        {
            _columns.CopyTo(array, arrayIndex);
        }

        bool ICollection<GridColumn<T>>.Remove(GridColumn<T> column)
        {
            return _columns.Remove(column);
        }

        int ICollection<GridColumn<T>>.Count
        {
            get { return _columns.Count; }
        }

        bool ICollection<GridColumn<T>>.IsReadOnly
        {
            get { return false; }
        }

        int IList<GridColumn<T>>.IndexOf(GridColumn<T> item)
        {
            return _columns.IndexOf(item);
        }

        void IList<GridColumn<T>>.Insert(int index, GridColumn<T> item)
        {
            _columns.Insert(index, item);
        }

        void IList<GridColumn<T>>.RemoveAt(int index)
        {
            _columns.RemoveAt(index);
        }

        GridColumn<T> IList<GridColumn<T>>.this[int index]
        {
            get { return _columns[index]; }
            set { _columns[index] = value; }
        }
    }
}
