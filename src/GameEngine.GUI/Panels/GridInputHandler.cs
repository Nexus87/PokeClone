using System;
using GameEngine.Utils;

namespace GameEngine.GUI.Panels
{
    internal class GridInputHandler
    {
        private int _selectedRow;
        private int _selectedColumn;
        private Table<GridCell> _cells;

        public GridInputHandler(Table<GridCell> cells)
        {
            _cells = cells;
        }


        public void HandleKeyInput(CommandKeys key)
        {
            if(key == CommandKeys.Back || key == CommandKeys.Select)
                return;

            if (SelectedCellOutOfBounds())
            {
                SelectClosesValidCell();
                return;
            }

            UnselectComponent();
            switch (key)
            {
                case CommandKeys.Up:
                    _selectedRow = NextValidRow(r => r - 1);
                    break;
                case CommandKeys.Down:
                    _selectedRow = NextValidRow(r => r + 1);
                    break;
                case CommandKeys.Left:
                    _selectedColumn = NextValidColumn(r => r - 1);
                    break;
                case CommandKeys.Right:
                    _selectedColumn = NextValidColumn(r => r + 1);
                    break;
                case CommandKeys.Select:
                case CommandKeys.Back:
                    throw new ArgumentOutOfRangeException(nameof(key), key, null);
            }

            SelectComponent();
        }

        private int NextValidRow(Func<int, int> func)
        {
            var validRow = _selectedRow;
            var nextRow = func(validRow);
            while (IsRowInBound(nextRow) && !_cells[nextRow, _selectedColumn].IsSelectable)
            {
                nextRow = func(nextRow);
            }

            if (IsRowInBound(nextRow))
                validRow = nextRow;
            return validRow;
        }

        private bool IsRowInBound(int nextRow)
        {
            return  0 <= nextRow && nextRow < _cells.Rows;
        }

        private int NextValidColumn(Func<int, int> iterator)
        {
            var validColumn = _selectedColumn;
            var nextColumn = iterator(validColumn);

            while (IsColumnInBound(nextColumn) && !_cells[_selectedRow, nextColumn].IsSelectable)
            {
                nextColumn = iterator(nextColumn);
            }
            if (IsColumnInBound(nextColumn))
                validColumn = nextColumn;

            return validColumn;
        }

        private bool IsColumnInBound(int nextColumn)
        {
            return 0 <= nextColumn && nextColumn < _cells.Columns;
        }

        private void SelectComponent()
        {
            _cells[_selectedRow, _selectedColumn].IsSelected = true;
        }

        private void UnselectComponent()
        {
            _cells[_selectedRow, _selectedColumn].IsSelected = false;
        }

        private void SelectClosesValidCell()
        {
            _selectedColumn = Math.Min(_selectedColumn, _cells.Columns - 1);
            _selectedRow = Math.Max(_selectedRow, _cells.Rows - 1);

            _selectedColumn = Math.Max(0, _selectedRow);
            _selectedRow = Math.Max(0, _selectedColumn);

            SelectComponent();
        }

        private bool SelectedCellOutOfBounds()
        {
            return _selectedColumn >= _cells.Columns || _selectedRow >= _cells.Rows;
        }
    }
}