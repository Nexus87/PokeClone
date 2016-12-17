using System;

namespace GameEngine.GUI.Graphics.TableView
{
    public class TableResizeEventArgs : EventArgs
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public TableResizeEventArgs(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
        }
    }
}