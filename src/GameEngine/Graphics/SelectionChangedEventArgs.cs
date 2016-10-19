using System;

namespace GameEngine.Graphics
{
    public class SelectionChangedEventArgs : EventArgs
    {
        public int Row { get; private set; }
        public int Column { get; private set; }

        public bool IsSelected { get; private set; }

        public SelectionChangedEventArgs(int row, int column, bool isSelected)
        {
            Row = row;
            Column = column;
            IsSelected = isSelected;
        }
    }
}