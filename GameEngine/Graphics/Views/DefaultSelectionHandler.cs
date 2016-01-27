using Microsoft.Xna.Framework.Input;
using System;

namespace GameEngine.Graphics.Views
{
    // TODO: Split TrySetColumn/TrySetRow to separate setting selectedColumn/Row and select the cell
    // in the view/updating the viewport. The way it is done now is a hack to avoid handing -1 to
    // SetCellSelection when the table size change from 0,0 to something bigger and the other way
    // around
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

        private void Unselect()
        {
            needsUpdate = false;
            selectedRow = selectedColumn = -1;
        }

        private bool IsUnselected()
        {
            return selectedRow == -1 && selectedColumn == -1;
        }

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

        private bool needsUpdate = true;
        private int newRow;
        private int newColumn;

        public int SelectedColumn { get { return needsUpdate ? newColumn : selectedColumn; } internal set { TrySetColumn(value); } }
        public int SelectedRow { get { return needsUpdate ? newRow : selectedRow; } internal set { TrySetRow(value); } }

        public virtual bool HandleInput(Keys key)
        {
            if (key == UpKey)
            {
                TrySetRow(SelectedRow - 1);
                return true;
            }
            else if (key == DownKey)
            {
                TrySetRow(SelectedRow + 1);
                return true;
            }
            else if (key == LeftKey)
            {
                TrySetColumn(SelectedColumn - 1);
                return true;
            }
            else if (key == RightKey)
            {
                TrySetColumn(SelectedColumn + 1);
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

        public void Init(IItemView view)
        {
            if (this.view != null)
                this.view.OnTableResize -= view_OnTableResize;

            this.view = view;
            view.OnTableResize += view_OnTableResize;

            // No cell there to select
            if (view.Rows == 0 || view.Columns == 0)
                Unselect();
        }
        
        private void TrySetColumn(int column)
        {
            if (column == SelectedColumn)
                return;

            if (column >= view.Columns || column < 0)
                return;

            newColumn = column;
            needsUpdate = true;
        }

        private void TrySetRow(int row)
        {
            if (row == SelectedRow)
                return;

            if (row >= view.Rows || row < 0)
                return;

            newRow = row;
            needsUpdate = true;
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
            if (e.rows == 0 || e.columns == 0)
            {
                Unselect();
                return;
            }

            // On shrink, move selection
            if (e.rows <= SelectedRow)
            {
                TrySetRow(e.rows - 1);
            }

            if (e.columns <= SelectedColumn)
            {
                TrySetColumn(e.columns - 1);
            }

            if (IsUnselected())
            {
                TrySetRow(0);
                TrySetColumn(0);
            }


        }

        public void Update()
        {
            if (!needsUpdate)
                return;

            if (selectedRow != -1 && selectedColumn != -1)
                view.SetCellSelection(selectedRow, selectedColumn, false);

            if (!view.SetCellSelection(newRow, newColumn, true))
                return;

            selectedRow = newRow;
            selectedColumn = newColumn;
            needsUpdate = false;

            UpdateViewpoint();
            SelectionChanged(this, null);
        }
    }
}