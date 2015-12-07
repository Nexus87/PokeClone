using System;

namespace GameEngine.Graphics.Views
{
    public class DefaultTableModel<T> : IItemModel<T>
    {
        private T[,] items;

        public DefaultTableModel()
        {
            items = new T[0, 0];
        }

        public DefaultTableModel(uint rows, uint columns)
        {
            items = new T[rows, columns];
        }
        public event EventHandler<DataChangedArgs<T>> DataChanged = delegate { };

        public int Columns { get { return items == null ? 0 : items.GetLength(1); } }

        public T[,] Items
        {
            set
            {
                if (items == value)
                    return;

                if (value == null)
                {
                    items = null;
                    DataChanged(this, null);
                    return;
                }

                if (items.GetLength(0) != value.GetLength(0) ||
                    items.GetLength(1) != value.GetLength(1))
                {
                    items = value;
                    DataChanged(this, null);
                    return;
                }

                items = value;
            }
        }

        public int Rows { get { return items == null ? 0 : items.GetLength(0); } }

        public T DataAt(int row, int column)
        {
            if (items == null)
                return default(T);

            return items[row, column];
        }

        public string DataStringAt(int row, int column)
        {
            if (items == null)
                return "";

            return items[row, column].ToString();
        }


        public bool SetData(T data, int row, int column)
        {
            items[row, column] = data;
            return true;
        }
    }
}