﻿using GameEngine.Graphics;
using System;
using System.Collections.Generic;

namespace BattleLib.GraphicComponents
{
    internal abstract class SingleDimensionTableModel<T> : ITableModel<T>
    {
        public event EventHandler<DataChangedEventArgs<T>> DataChanged = delegate { };
        public event EventHandler<TableResizeEventArgs> SizeChanged = delegate { };

        protected IList<T> items;

        public virtual int Rows { get { return items.Count; } }

        public int Columns
        {
            get { return 1; }
        }

        public virtual T DataAt(int row, int column)
        {
            if (row < 0 || row >= Rows || column < 0 || column >= Columns)
                throw new ArgumentOutOfRangeException("Index out of bound");

            return items[row];
        }

        public virtual bool SetDataAt(T data, int row, int column)
        {
            if (row < 0 || row >= Rows || column < 0 || column >= Columns)
                throw new ArgumentOutOfRangeException("Index out of bound");

            if (Object.Equals(items[row], (data)))
                return true;

            items[row] = data;
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
