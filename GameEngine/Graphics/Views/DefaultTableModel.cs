using System;
using GameEngine.Utils;

namespace GameEngine.Graphics.Views
{
    public class DefaultTableModel<T> : IItemModel<T>
    {
        private T[,] items;

        public DefaultTableModel() : this(0, 0)
        {
        }

        public DefaultTableModel(uint rows, uint columns)
        {
            items = new T[rows, columns];
        }

        public event EventHandler<DataChangedArgs<T>> DataChanged = delegate { };
        public event EventHandler<SizeChangedArgs> SizeChanged = delegate { };

        public int Columns { get { return items.GetLength(1); } }

        public int Rows { get { return items.GetLength(0); } }

        public T DataAt(int row, int column)
        {
            if (row >= Rows || column >= Columns)
                return default(T);

            return items[row, column];
        }

        public string DataStringAt(int row, int column)
        {
            if (row >= Rows || column >= Columns)
                return "";

            var item = items[row, column];
            return item == null ? "" : item.ToString();
        }


        public bool SetData(T data, int row, int column)
        {
            if (row >= Rows || column >= Columns)
            {
                Resize(row >= Rows ? row + 1 : Rows, column >= Columns ? column + 1 : Columns);
                SizeChanged(this, new SizeChangedArgs { newRows = Rows, newColumns = Columns });
            }

            var oldValue = items[row, column];
            items[row, column] = data;

            if (!Object.Equals(oldValue, data))
                DataChanged(this, new DataChangedArgs<T> { column = column, row = row, newData = data });
            return true;
        }

        private void Resize(int newRows, int newColumns)
        {
            var newItems = new T[newRows, newColumns];
            items.Copy(newItems);
            items = newItems;
        }
    }
}