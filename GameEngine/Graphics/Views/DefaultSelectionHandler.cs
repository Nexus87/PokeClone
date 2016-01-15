using Microsoft.Xna.Framework.Input;
using System;

namespace GameEngine.Graphics.Views
{
    public class DefaultSelectionHandler : ISelectionHandler
    {
        public Keys BackKey = Keys.Escape;
        public Keys DownKey = Keys.Down;
        public Keys LeftKey = Keys.Left;
        public Keys RightKey = Keys.Right;
        public Keys SelectKey = Keys.Enter;
        public Keys UpKey = Keys.Up;
        private int selectedColumn;

        private int selectedRow;

        private IItemView view;

        public DefaultSelectionHandler()
        {
        }

        public DefaultSelectionHandler(Configuration config)
        {
            DownKey = config.KeyDown;
            LeftKey = config.KeyLeft;
            RightKey = config.KeyRight;
            UpKey = config.KeyUp;
            SelectKey = config.KeySelect;
            BackKey = config.KeyBack;
        }

        public event EventHandler<EventArgs> CloseRequested = delegate { };

        public event EventHandler<EventArgs> ItemSelected = delegate { };

        public event EventHandler<EventArgs> SelectionChanged = delegate { };

        public int SelectedColumn { get { return selectedColumn; } internal set { TrySetColumn(value); } }
        public int SelectedRow { get { return selectedRow; } internal set { TrySetRow(value); } }
        private int Columns { get; set; }
        private int Rows { get; set; }

        public virtual void HandleInput(Keys key)
        {
            if (key == UpKey)
                TrySetRow(SelectedRow - 1);
            else if (key == DownKey)
                TrySetRow(SelectedRow + 1);
            else if (key == LeftKey)
                TrySetColumn(SelectedColumn - 1);
            else if (key == RightKey)
                TrySetColumn(SelectedColumn + 1);
            else if (key == SelectKey)
                ItemSelected(this, null);
            else if (key == BackKey)
                CloseRequested(this, null);
        }

        public void Init(IItemView view)
        {
            if (this.view != null)
                this.view.OnTableResize -= view_OnTableResize;

            this.view = view;
            Rows = view.Rows;
            Columns = view.Columns;
            view.OnTableResize += view_OnTableResize;
            view.SetCellSelection(0, 0, true);
        }

        private void TrySetColumn(int column)
        {
            if (column == selectedColumn)
                return;

            if (column >= Columns || column < 0)
                return;

            view.SetCellSelection(selectedRow, selectedColumn, false);
            selectedColumn = column;
            view.SetCellSelection(selectedRow, selectedColumn, true);
            UpdateViewpoint();
            if (SelectionChanged != null)
                SelectionChanged(this, null);
        }

        private void TrySetRow(int row)
        {
            if (row == selectedRow)
                return;

            if (row >= Rows || row < 0)
                return;

            view.SetCellSelection(selectedRow, selectedColumn, false);
            selectedRow = row;
            view.SetCellSelection(selectedRow, selectedColumn, true);
            UpdateViewpoint();
            if (SelectionChanged != null)
                SelectionChanged(this, null);
        }

        private void UpdateViewpoint()
        {
            int startRow = view.ViewportStartRow;
            int startColumn = view.ViewportStartColumn;

            if (SelectedRow < startRow)
                view.ViewportStartRow = SelectedRow;
            else if (SelectedRow >= startRow + view.ViewportRows)
                view.ViewportStartRow = SelectedRow - (view.ViewportRows - 1);

            if (SelectedColumn < startColumn)
                view.ViewportStartColumn = SelectedColumn;
            else if (SelectedColumn >= startColumn + view.ViewportColumns)
                view.ViewportStartColumn = SelectedColumn - (view.ViewportColumns - 1);
        }

        private void view_OnTableResize(object sender, TableResizeEventArgs e)
        {
            if (e.rows < Rows)
            {
                Rows = e.rows;
                TrySetRow(Rows - 1);
            }

            if (e.columns < Columns)
            {
                Columns = e.columns;
                TrySetColumn(Columns - 1);
            }

            Rows = e.rows;
            Columns = e.columns;
        }
    }
}