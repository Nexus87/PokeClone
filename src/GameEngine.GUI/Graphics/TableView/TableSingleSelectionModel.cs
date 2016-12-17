using System;
using GameEngine.Registry;
using GameEngine.Utils;

namespace GameEngine.GUI.Graphics.TableView
{
    [GameType]
    public class TableSingleSelectionModel : ITableSelectionModel
    {
        private Tuple<int, int> currentSelection = new Tuple<int, int>(0, 0);
        private int CurrentRow { get { return currentSelection.Item1; } }
        private int CurrentColumn { get { return currentSelection.Item2; } }

        public event EventHandler<SelectionChangedEventArgs> SelectionChanged = delegate { };
        private readonly IndexValidator indexValidator;

        public TableSingleSelectionModel() : this(delegate { return true; }) { }

        public TableSingleSelectionModel(IndexValidator indexValidator)
        {
            indexValidator.CheckNull("indexValidator");
            this.indexValidator = indexValidator;
        }

        public bool SelectIndex(int row, int column)
        {
            
            AssertIndexIsPositive(row, column);
            if (IsSelected(row, column))
                return true;

            if (!indexValidator(row, column))
                return false;

            UnselectIndex(CurrentRow, CurrentColumn);
            SetSelection(row, column);

            return true;
        }

        private void SetSelection(int row, int column)
        {
            currentSelection = new Tuple<int, int>(row, column);
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
            currentSelection = null;
            SelectionChanged(this, new SelectionChangedEventArgs(row, column, false));
        }

        private static void AssertIndexIsPositive(int row, int column)
        {
            if (row < 0 || column < 0)
                throw new ArgumentOutOfRangeException("row and column must be positive");
        }

        public bool IsSelected(int row, int column)
        {
            AssertIndexIsPositive(row, column);

            if (currentSelection == null)
                return false;

            return row == CurrentRow && column == CurrentColumn;
        }
    }
}
