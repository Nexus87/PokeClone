﻿using System;

namespace GameEngine.Graphics.TableView
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
}