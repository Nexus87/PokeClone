using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics.Views
{
    public class DataChangedArgs<T> : EventArgs
    {
        public int row;
        public int column;
        public T newData;
    }

    public class SizeChangedArgs : EventArgs
    {
        public int newRows;
        public int newColumns;
    }

    public interface IItemModel<T>
    {
        event EventHandler<DataChangedArgs<T>> DataChanged;
        event EventHandler<SizeChangedArgs> SizeChanged;
        int Rows { get; }
        int Columns { get; }

        T DataAt(int row, int column);
        string DataStringAt(int row, int column);
        bool SetData(T data, int row, int column);
    }
}
