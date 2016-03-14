using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Utils;

namespace GameEngine.Graphics.Views
{
    public class DefaultListModel<T> : IItemModel<T>
    {
        private readonly List<T> items = new List<T>();
        public event EventHandler<DataChangedEventArgs<T>> DataChanged = delegate { };
        public event EventHandler<SizeChangedEventArgs> SizeChanged = delegate { };

        public DefaultListModel() { }

        public DefaultListModel(int size)
        {
            items.Resize(size);
        }

        public virtual int Rows
        {
            get { return items.Count(); }
        }

        public virtual  int Columns
        {
            get { return 1; }
        }

        public virtual T DataAt(int row, int column)
        {
            if (row >= Rows)
                return default(T);

            return items[row];
        }

        public virtual string DataStringAt(int row, int column)
        {
            if (row >= Rows)
                return "";
            var item = items[row];
            return item == null ? "" : items[row].ToString();
        }

        public bool SetData(T data, int row)
        {
            return SetData(data, row, 0);
        }
        public virtual bool SetData(T data, int row, int column)
        {
            if (row >= Rows)
            {
                items.Resize(row + 1);
                SizeChanged(this, new SizeChangedEventArgs(Rows, Columns));
            }

            var oldValue = items[row];
            items[row] = data;
            if (!Object.Equals(oldValue, data))
                DataChanged(this, new DataChangedEventArgs<T>(row, column, data));
            return true;
        }


        public T this[int row, int column]
        {
            get
            {
                return DataAt(row, column);
            }
            set
            {
                SetData(value, row, column);
            }
        }
    }
}
