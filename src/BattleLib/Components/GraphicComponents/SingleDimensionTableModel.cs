using System;
using System.Collections.Generic;
using GameEngine.Graphics;

namespace BattleLib.Components.GraphicComponents
{
    public abstract class SingleDimensionTableModel<T> : ITableModel<T>
    {
        public event EventHandler<DataChangedEventArgs<T>> DataChanged = delegate { };
        public event EventHandler<TableResizeEventArgs> SizeChanged
        {
            add { }
            remove { }
        }

        protected IList<T> Items;

        protected SingleDimensionTableModel()
        {
            SizeChanged += delegate { };
        }

        public virtual int Rows { get { return Items.Count; } }

        public int Columns
        {
            get { return 1; }
        }

        public virtual T DataAt(int row, int column)
        {
            if (row < 0 || row >= Rows || column < 0 || column >= Columns)
                throw new ArgumentOutOfRangeException("Index out of bound");

            return Items[row];
        }

        public virtual bool SetDataAt(T data, int row, int column)
        {
            if (row < 0 || row >= Rows || column < 0 || column >= Columns)
                throw new ArgumentOutOfRangeException("Index out of bound");

            if (Object.Equals(Items[row], (data)))
                return true;

            Items[row] = data;
            DataChanged(this, new DataChangedEventArgs<T>(row, column, data));

            return true;
        }

        public T this[int row, int column]
        {
            get { return DataAt(row, column); }
            set { SetDataAt(value, row, column); }
        }
    }
}
