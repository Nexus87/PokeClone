namespace GameEngine.Graphics.Views
{
    internal interface IItemView
    {
        int Columns { get; }
        int Rows { get; }

        int ViewportColumns { get; }
        int ViewportRows { get; }
        int VisibleColumns { get; }
        int VisibleRows { get; }
        int ViewportStartRow { get; set; }
        int ViewportStartColumn { get; set; }

        bool IsCellSelected(int row, int column);
        bool SetCellSelection(int row, int column, bool isSelected);
    }
}