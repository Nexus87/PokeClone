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

        private ITableView view;

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

        public int SelectedColumn { get { return selectedColumn; } internal set { SetSelection(SelectedRow, value); } }
        public int SelectedRow { get { return selectedRow; } internal set { SetSelection(value, SelectedColumn); } }

        public virtual bool HandleInput(Keys key)
        {
            if (key == UpKey)
            {
                SelectedRow = Math.Max(0, SelectedRow - 1);
                return true;
            }
            else if (key == DownKey)
            {
                SelectedRow++;
                return true;
            }
            else if (key == LeftKey)
            {
                SelectedColumn = Math.Max(0, selectedColumn - 1);
                return true;
            }
            else if (key == RightKey)
            {
                SelectedColumn++;
                return true;
            }
            else if (key == SelectKey)
            {
                ItemSelected(this, null);
                return true;
            }
            else if (key == BackKey)
            {
                CloseRequested(this, null);
                return true;
            }

            return false;
        }

        public void Init(ITableView view)
        {
            if (this.view != null)
                this.view.OnTableResize -= view_OnTableResize;

            this.view = view;
            view.OnTableResize += view_OnTableResize;
            
            // At the moment, nothing is selected
            selectedColumn = selectedRow = -1;
            if (view.Rows != 0 || view.Columns != 0)
                SetSelection(0, 0);
        }

        private void SetSelection(int row, int column)
        {
            if (column == selectedColumn && row == selectedRow)
                return;

            if (column >= view.Columns || row >= view.Rows)
                return;

            view.SetCellSelection(selectedRow, selectedColumn, false);
            view.SetCellSelection(row, column, true);

            selectedColumn = column;
            selectedRow = row;

            UpdateViewpoint();
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
            if (selectedColumn == -1 && selectedRow == -1 && e.rows > 0 && e.columns > 0)
            {
                SetSelection(0, 0);
                return;
            }

            if (selectedColumn < e.columns && selectedRow < e.rows)
                return;

            int newColumn = selectedColumn;
            int newRow = selectedRow;
            // On shrink, move selection
            if (selectedRow >= e.rows)
                newRow = e.rows - 1;

            if (selectedColumn >= e.columns)
                newColumn = e.columns - 1;

            SetSelection(newRow, newColumn);
        }
    }
}