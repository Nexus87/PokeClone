using System;
using GameEngine.GUI.Utils;
using GameEngine.TypeRegistry;

namespace GameEngine.GUI.Graphics.TableView
{
    [GameType]
    public class TableSingleSelectionModel : ITableSelectionModel
    {
        private Tuple<int, int> _currentSelection = new Tuple<int, int>(0, 0);
        private int CurrentRow => _currentSelection.Item1;
        private int CurrentColumn => _currentSelection.Item2;

        public event EventHandler<SelectionChangedEventArgs> SelectionChanged = delegate { };
        private readonly IndexValidator _indexValidator;

        public TableSingleSelectionModel() : this(delegate { return true; }) { }

        public TableSingleSelectionModel(IndexValidator indexValidator)
        {
            indexValidator.CheckNull("indexValidator");
            _indexValidator = indexValidator;
        }

        public bool SelectIndex(int row, int column)
        {
            
            AssertIndexIsPositive(row, column);
            if (IsSelected(row, column))
                return true;

            if (!_indexValidator(row, column))
                return false;

            UnselectIndex(CurrentRow, CurrentColumn);
            SetSelection(row, column);

            return true;
        }

        private void SetSelection(int row, int column)
        {
            _currentSelection = new Tuple<int, int>(row, column);
            SelectionChanged(this, new SelectionChangedEventArgs(row, column, true));
        }

        public virtual bool UnselectIndex(int row, int column)
        {
            AssertIndexIsPositive(row, column);

            if (!IsSelected(row, column))
                return false;

            UnselectIndexImpl(row, column);
            return true;
        }

        private void UnselectIndexImpl(int row, int column)
        {
            _currentSelection = null;
            SelectionChanged(this, new SelectionChangedEventArgs(row, column, false));
        }

        private static void AssertIndexIsPositive(int row, int column)
        {
            if (column < 0) throw new ArgumentOutOfRangeException(nameof(column));
            if (row < 0) throw new ArgumentOutOfRangeException(nameof(row));
        }

        public bool IsSelected(int row, int column)
        {
            AssertIndexIsPositive(row, column);

            if (_currentSelection == null)
                return false;

            return row == CurrentRow && column == CurrentColumn;
        }
    }
}
