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
        private Tuple<int, int> currentSelection;

        public event EventHandler<SelectionChangedEventArgs> SelectionChanged;

        public void SelectIndex(int row, int column)
        {
            CheckRange(row, column);

            currentSelection = new Tuple<int, int>(row, column);

        }

        public void UnselectIndex(int row, int column)
        {
            CheckRange(row, column);

            if (IsSelected(row, column))
                currentSelection = null;
        }

        private void CheckRange(int row, int column)
        {
            if (row < 0 || column < 0)
                throw new ArgumentOutOfRangeException("row and column must be positive");
        }

        public bool IsSelected(int row, int column)
        {
            CheckRange(row, column);

            return currentSelection != null && row == currentSelection.Item1 && column == currentSelection.Item2;
        }
    }
}
