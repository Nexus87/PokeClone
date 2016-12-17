using System;
using GameEngine.Utils;

namespace GameEngine.GUI.Graphics.TableView
{
    public class DefaultTableModel<T> : ITableModel<T>
    {
        private readonly Table<T> items = new Table<T>();

        public event EventHandler<DataChangedEventArgs<T>> DataChanged = delegate { };
        public event EventHandler<TableResizeEventArgs> SizeChanged = delegate { };

        public virtual int Columns { get { return items.Columns; } }

        public virtual int Rows { get { return items.Rows; } }

        public T DataAt(int row, int column)
        {
            if (row >= Rows || row < 0)
                throw new IndexOutOfRangeException("column: must be between 0 and " + Rows + " but was " + row);
            if (column >= Columns || column < 0)
                throw new IndexOutOfRangeException("column: must be between 0 and " + Columns + " but was " + column);

            return items[row, column];
        }

        public virtual bool SetDataAt(T data, int row, int column)
        {
            bool sizeChanged = row >= Rows || column >= Columns;

            var oldValue = items[row, column];
            items[row, column] = data;

            if(sizeChanged)
                SizeChanged(this, new TableResizeEventArgs(Rows, Columns));

            if (!Equals(oldValue, data))
                DataChanged(this, new DataChangedEventArgs<T>(row, column, data));
            return true;
        }
    }
}