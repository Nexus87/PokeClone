﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Utils;

namespace GameEngine.Graphics.Views
{
    public class DefaultListModel<T> : IItemModel<T>
    {
        protected readonly List<T> items = new List<T>();
        public event EventHandler<DataChangedArgs<T>> DataChanged = delegate { };
        public event EventHandler<SizeChangedArgs> SizeChanged = delegate { };

        public virtual int Rows
        {
            get { return items.Count(); }
        }

        public virtual  int Columns
        {
            get { return 1; }
        }

        public virtual T DataAt(int row, int column)
        {
            if (row >= Rows)
                return default(T);

            return items[row];
        }

        public virtual string DataStringAt(int row, int column)
        {
            if (row >= Rows)
                return "";
            var item = items[row];
            return item == null ? "" : items[row].ToString();
        }

        public bool SetData(T data, int row)
        {
            return SetData(data, row, 0);
        }
        public virtual bool SetData(T data, int row, int column)
        {
            if (row >= Rows)
            {
                items.Resize(row + 1);
                SizeChanged(this, new SizeChangedArgs { newColumns = Columns, newRows = Rows });
            }

            var oldValue = items[row];
            items[row] = data;
            if (!Object.Equals(oldValue, data))
                DataChanged(this, new DataChangedArgs<T> { column = column, row = row, newData = data });
            return true;
        }
    }
}
