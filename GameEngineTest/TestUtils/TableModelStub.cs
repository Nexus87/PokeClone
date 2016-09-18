using GameEngine.Graphics;
using System;

namespace GameEngineTest.TestUtils
{
    public class TableModelStub<T> : ITableModel<T>
    {
        public event EventHandler<DataChangedEventArgs<T>> DataChanged { add { } remove { } }
        public event EventHandler<TableResizeEventArgs> SizeChanged { add { } remove { } }
        public T ReturnValue = default(T);

        public int Rows { get; set; }

        public int Columns { get; set; }

        public T DataAt(int row, int column)
        {
            if (row < 0 || column < 0 || row >= Rows || column >= Columns)
                throw new IndexOutOfRangeException("row/column");

            return ReturnValue;    
        }

        public bool SetDataAt(T data, int row, int column)
        {
            return false;
        }
    }
}
