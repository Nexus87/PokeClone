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
        private bool[,] selections = new bool[0, 0];
        private bool? currentSelection;

        public event EventHandler<SelectionChangedEventArgs> SelectionChanged;

        public void SelectIndex(int row, int column)
        {
            throw new NotImplementedException();
        }

        public void UnselectIndex(int row, int column)
        {
            throw new NotImplementedException();
        }

        public bool IsSelected(int row, int column)
        {
            if (row >= selections.Rows() || column >= selections.Columns())
                return false;

            return selections[row, column];
        }
    }
}
