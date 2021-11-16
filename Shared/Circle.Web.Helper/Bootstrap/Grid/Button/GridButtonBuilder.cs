using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circle.Web.Helper.Bootstrap.Grid.Button
{
    public class GridButtonBuilder : IList<GridButton>
    {
        private readonly List<GridButton> _buttons = new List<GridButton>();

        public GridButton this[int index]
        {
            get
            {
                return _buttons[index];
            }

            set
            {
                _buttons[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return _buttons.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public virtual void Add(GridButton item)
        {
            _buttons.Add(item);
        }

        public IGridButton Add(GridButtonType buttonType)
        {
            GridButton btn = new GridButton(buttonType);
            _buttons.Add(btn);
            return btn;
        }

        public void Clear()
        {
            _buttons.Clear();
        }

        public bool Contains(GridButton item)
        {
            return _buttons.Contains(item);
        }

        public void CopyTo(GridButton[] array, int arrayIndex)
        {
            _buttons.CopyTo(array, arrayIndex);
        }

        public IEnumerator<GridButton> GetEnumerator()
        {
            return _buttons.GetEnumerator();
        }

        public int IndexOf(GridButton item)
        {
            return _buttons.IndexOf(item);
        }

        public void Insert(int index, GridButton item)
        {
            _buttons.Insert(index, item);
        }

        public bool Remove(GridButton item)
        {
            return _buttons.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _buttons.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _buttons.GetEnumerator();
        }

    }
}
