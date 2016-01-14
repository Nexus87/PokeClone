using Microsoft.Xna.Framework.Input;
using System;

namespace GameEngine.Graphics.Views
{
    public class DefaultSelectionHandler : ISelectionHandler
    {
        public Keys DownKey = Keys.Down;
        public Keys LeftKey = Keys.Left;
        public Keys RightKey = Keys.Right;
        public Keys SelectKey = Keys.Enter;
        public Keys UpKey = Keys.Up;
        public Keys BackKey = Keys.Escape;

        public event EventHandler<EventArgs> ItemSelected = delegate { };
        public event EventHandler<EventArgs> CloseRequested = delegate { };
        public event EventHandler<EventArgs> SelectionChanged = delegate { };

        public int SelectedRow { get { return selectedRow; } internal set { TrySetRow(value); } }
        public int SelectedColumn { get { return selectedColumn; } internal set { TrySetColumn(value); } }

        private int selectedRow;
        private int selectedColumn;

        private int Rows { get; set; }
        private int Columns { get; set; }

        private IItemView view;

        public DefaultSelectionHandler() {}
        public DefaultSelectionHandler(Configuration config)
        {
            DownKey = config.KeyDown;
            LeftKey = config.KeyLeft;
            RightKey = config.KeyRight;
            UpKey = config.KeyUp;
            SelectKey = config.KeySelect;
            BackKey = config.KeyBack;
        }

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

        private void TrySetColumn(int column)
        {
            if (column == selectedColumn)
                return;

            if (column >= Columns || column < 0)
                return;

            selectedColumn = column;
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

            selectedRow = row;
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
        public void Init(IItemView view)
        {
            this.view = view;
            Rows = view.Rows;
            Columns = view.Columns;
        }
    }
}