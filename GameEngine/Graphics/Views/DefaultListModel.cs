using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics.Views
{
    public class DefaultListModel<T> : IItemModel<T>
    {
        public List<T> Items
        {
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Items must not be null");

                if (items == value)
                    return;

                if (items.Count != value.Count)
                {
                    items = value;
                    SizeChanged(this, null);
                    return;
                }

                items = value;
            }
        }

        private List<T> items = new List<T>();
        public event EventHandler SizeChanged = delegate { };

        public int Rows
        {
            get { return items == null ? 0 : items.Count(); }
        }

        public int Columns
        {
            get { return 1; }
        }

        public T DataAt(int row, int column)
        {
            if (column > 0)
                return default(T);

            return items[row];
        }

        public string DataStringAt(int row, int column)
        {
            return items[row].ToString();
        }


        public void SetData(T data, int row, int column)
        {
            if (column > 0)
                throw new InvalidOperationException("Index out of bound");

            items[row] = data;
        }
    }
}
