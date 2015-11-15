using Microsoft.Xna.Framework.Input;
using System;

namespace GameEngine.Graphics.Views
{
    public class DefaultSelectionHandler<T> : ISelectionHandler
    {
        public Keys DownKey = Keys.Down;
        public Keys LeftKey = Keys.Left;
        public Keys RightKey = Keys.Right;
        public Keys SelectKey = Keys.Enter;
        public Keys UpKey = Keys.Up;

        public event EventHandler<EventArgs> ItemSelected;

        public event EventHandler<EventArgs> SelectionChanged;

        public IItemModel<T> Model { private get; set; }
        public Tuple<int, int> SelectedIndex { get; private set; }

        public DefaultSelectionHandler()
        {
            SelectedIndex = new Tuple<int, int>(0, 0);
        }
        public virtual void HandleInput(Keys key)
        {
            if (key == UpKey)
                TrySetRow(SelectedIndex.Item1 - 1);
            else if (key == DownKey)
                TrySetRow(SelectedIndex.Item1 + 1);
            else if (key == LeftKey)
                TrySetColumn(SelectedIndex.Item2 - 1);
            else if (key == RightKey)
                TrySetColumn(SelectedIndex.Item2 + 1);
            else if (key == SelectKey)
            {
                if (ItemSelected != null)
                    ItemSelected(this, null);
            }
            else
                HandleAdditionalKeys(key);
        }

        protected virtual void HandleAdditionalKeys(Keys key)
        {
        }

        private void TrySetColumn(int column)
        {
            if (column == SelectedIndex.Item2)
                return;

            if (column >= Model.Columns || column < 0)
                return;

            SelectedIndex = new Tuple<int, int>(SelectedIndex.Item1, column);
            if (SelectionChanged != null)
                SelectionChanged(this, null);
        }

        private void TrySetRow(int row)
        {
            if (row == SelectedIndex.Item1)
                return;

            if (row >= Model.Rows || row < 0)
                return;

            SelectedIndex = new Tuple<int, int>(row, SelectedIndex.Item2);
            if (SelectionChanged != null)
                SelectionChanged(this, null);
        }
    }
}