using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics.Views
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

    public class SizeChangedEventArgs : EventArgs
    {
        public int NewRows { get; private set; }
        public int NewColumns { get; private set; }

        public SizeChangedEventArgs(int newRows, int newColumns)
        {
            NewRows = newRows;
            NewColumns = newColumns;
        }
    }

    public interface IItemModel<T>
    {
        event EventHandler<DataChangedEventArgs<T>> DataChanged;
        event EventHandler<SizeChangedEventArgs> SizeChanged;
        int Rows { get; }
        int Columns { get; }

        T DataAt(int row, int column);
        string DataStringAt(int row, int column);
        bool SetData(T data, int row, int column);
    }
}
