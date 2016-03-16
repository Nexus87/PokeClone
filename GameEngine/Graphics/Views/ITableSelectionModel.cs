using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics.Views
{
    public class SelectionChangedEventArgs : EventArgs
    {
        public int Row { get; private set; }
        public int Column { get; private set; }

        public bool IsSelected { get; private set; }

        public SelectionChangedEventArgs(int row, int column, bool isSelected)
        {
            Row = row;
            Column = column;
            IsSelected = isSelected;
        }
    }

    public interface ITableSelectionModel
    {
        event EventHandler<SelectionChangedEventArgs> SelectionChanged;

        void SelectIndex(int row, int column);
        void UnselectIndex(int row, int column);

        bool IsSelected(int row, int column);

    }
}
