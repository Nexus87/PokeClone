using GameEngine.Utils;
using System;

namespace GameEngine.Graphics
{
    public class DefaultTableModel<T> : ITableModel<T>
    {
        private T[,] items;

        public DefaultTableModel() : this(0, 0)
        {
        }

        public DefaultTableModel(uint rows, uint columns)
        {
            items = new T[rows, columns];
        }

        public event EventHandler<DataChangedEventArgs<T>> DataChanged = delegate { };
        public event EventHandler<TableResizeEventArgs> SizeChanged = delegate { };

        public virtual int Columns { get { return items.GetLength(1); } }

        public virtual int Rows { get { return items.GetLength(0); } }

        public T DataAt(int row, int column)
        {
            if (row >= Rows || column >= Columns)
                return default(T);

            return items[row, column];
        }

        public virtual bool SetData(T data, int row, int column)
        {
            if (row >= Rows || column >= Columns)
            {
                Resize(row >= Rows ? row + 1 : Rows, column >= Columns ? column + 1 : Columns);
                SizeChanged(this, new TableResizeEventArgs(Rows, Columns));
            }

            var oldValue = items[row, column];
            items[row, column] = data;

            if (!Object.Equals(oldValue, data))
                DataChanged(this, new DataChangedEventArgs<T>(row, column, data));
            return true;
        }

        private void Resize(int newRows, int newColumns)
        {
            var newItems = new T[newRows, newColumns];
            items.Copy(newItems);
            items = newItems;
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

        protected void OnSizeChanged(int rows, int columns)
        {
            SizeChanged(this, new TableResizeEventArgs(rows, columns));
        }
    }
}