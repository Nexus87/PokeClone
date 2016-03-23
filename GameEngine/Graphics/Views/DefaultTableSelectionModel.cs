using GameEngine.Utils;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics.Views
{
    public class DefaultTableSelectionModel : ITableSelectionModel
    {
        private Tuple<int, int> currentSelection = new Tuple<int, int>(0, 0);
        private int CurrentRow { get { return currentSelection.Item1; } }
        private int CurrentColumn { get { return currentSelection.Item2; } }

        public event EventHandler<SelectionChangedEventArgs> SelectionChanged = delegate { };

        public virtual bool SelectIndex(int row, int column)
        {
            
            CheckRange(row, column);

            if (IsSelected(row, column))
                return true;

            if (currentSelection != null)
                UnselectIndexImpl(CurrentRow, CurrentColumn);

            currentSelection = new Tuple<int, int>(row, column);
            SelectionChanged(this, new SelectionChangedEventArgs(row, column, true));

            return true;

        }

        public virtual bool UnselectIndex(int row, int column)
        {
            CheckRange(row, column);

            if (!IsSelected(row, column))
                return false;

            UnselectIndexImpl(row, column);
            return true;
        }

        private void UnselectIndexImpl(int row, int column)
        {
            currentSelection = null;
            SelectionChanged(this, new SelectionChangedEventArgs(row, column, false));
        }

        private void CheckRange(int row, int column)
        {
            if (row < 0 || column < 0)
                throw new ArgumentOutOfRangeException("row and column must be positive");
        }

        public bool IsSelected(int row, int column)
        {
            CheckRange(row, column);

            return currentSelection != null && row == CurrentRow && column == CurrentColumn;
        }
    }
}
