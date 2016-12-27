using System;
using GameEngine.GUI.Utils;

namespace GameEngine.GUI.Graphics.TableView
{
    public class DefaultTableModel<T> : ITableModel<T>
    {
        private readonly Table<T> _items = new Table<T>();

        public event EventHandler<DataChangedEventArgs<T>> DataChanged = delegate { };
        public event EventHandler<TableResizeEventArgs> SizeChanged = delegate { };

        public virtual int Columns => _items.Columns;

        public virtual int Rows => _items.Rows;

        public T DataAt(int row, int column)
        {
            if (row >= Rows || row < 0)
                throw new IndexOutOfRangeException("column: must be between 0 and " + Rows + " but was " + row);
            if (column >= Columns || column < 0)
                throw new IndexOutOfRangeException("column: must be between 0 and " + Columns + " but was " + column);

            return _items[row, column];
        }

        public virtual bool SetDataAt(T data, int row, int column)
        {
            bool sizeChanged = row >= Rows || column >= Columns;

            var oldValue = _items[row, column];
            _items[row, column] = data;

            if(sizeChanged)
                SizeChanged(this, new TableResizeEventArgs(Rows, Columns));

            if (!Equals(oldValue, data))
                DataChanged(this, new DataChangedEventArgs<T>(row, column, data));
            return true;
        }
    }
}