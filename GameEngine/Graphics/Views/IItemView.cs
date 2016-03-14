using System;
namespace GameEngine.Graphics.Views
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

    public interface ITableView
    {
        event EventHandler<TableResizeEventArgs> OnTableResize;

        int Columns { get; }
        int Rows { get; }

        int StartRow { get; set; }
        int StartColumn { get; set; }

        bool IsCellSelected(int row, int column);
        bool SetCellSelection(int row, int column, bool isSelected);
    }
}