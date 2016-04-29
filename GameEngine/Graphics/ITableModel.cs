using System;

namespace GameEngine.Graphics
{
    public class DataChangedEventArgs<T> : EventArgs
    {
        public int Row { get; private set; }
        public int Column { get; private set; }
        public T NewData { get; private set; }

        public DataChangedEventArgs(int row, int column, T newData)
        {
            Row = row;
            Column = column;
            NewData = newData;
        }
    }

    public interface ITableModel<T>
    {
        event EventHandler<DataChangedEventArgs<T>> DataChanged;
        event EventHandler<TableResizeEventArgs> SizeChanged;
        int Rows { get; }
        int Columns { get; }

        T DataAt(int row, int column);
        bool SetData(T data, int row, int column);

        T this[int row, int column] { get; set; }
    }
}
