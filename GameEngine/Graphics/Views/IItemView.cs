using System;
namespace GameEngine.Graphics.Views
{
    public class TableResizeEventArgs : EventArgs
    {
        public int rows;
        public int columns;

        public TableResizeEventArgs(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
        }
    }
    public interface IItemView
    {
        event EventHandler<TableResizeEventArgs> OnTableResize;

        int Columns { get; }
        int Rows { get; }

        int ViewportColumns { get; }
        int ViewportRows { get; }
        int ViewportStartRow { get; set; }
        int ViewportStartColumn { get; set; }

        bool IsCellSelected(int row, int column);
        bool SetCellSelection(int row, int column, bool isSelected);
    }
}