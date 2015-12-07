using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics.Views
{
    public class DefaultListModel<T> : IItemModel<T>
    {
        public virtual List<T> Items
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
                    DataChanged(this, null);
                    return;
                }

                items = value;
            }
        }

        protected List<T> items = new List<T>();
        public event EventHandler<DataChangedArgs<T>> DataChanged = delegate { };

        public virtual int Rows
        {
            get { return items == null ? 0 : items.Count(); }
        }

        public virtual  int Columns
        {
            get { return 1; }
        }

        public virtual T DataAt(int row, int column)
        {
            if (column > 0)
                return default(T);

            return items[row];
        }

        public virtual string DataStringAt(int row, int column)
        {
            return items[row].ToString();
        }


        public virtual bool SetData(T data, int row, int column)
        {
            if (column > 0)
                throw new InvalidOperationException("Index out of bound");

            items[row] = data;

            return true;
        }
    }
}
