using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine.Globals;

namespace GameEngine.GUI.Panels
{
    internal class GridInputHandler
    {
        private int _selectedRow;
        private int _selectedColumn;
        private readonly Table<GridCell> _cells;

        public GridInputHandler(Table<GridCell> cells)
        {
            _cells = cells;
        }


        public void HandleKeyInput(CommandKeys key)
        {
            if(key == CommandKeys.Back || key == CommandKeys.Select)
            {
                _cells[_selectedRow, _selectedColumn].GuiComponent?.HandleKeyInput(key);
                return;
            }

            if (SelectedCellOutOfBounds())
            {
                SelectClosesValidCell();
                return;
            }

            UnselectComponent();
            switch (key)
            {
                case CommandKeys.Up:
                    _selectedRow = PreviousValidComponent(_selectedRow, _cells.EnumerateRows(_selectedColumn), _cells.Rows);
                    break;
                case CommandKeys.Down:
                    _selectedRow = NextValidComponent(_selectedRow, _cells.EnumerateRows(_selectedColumn), _cells.Rows);
                    break;
                case CommandKeys.Left:
                    _selectedColumn = PreviousValidComponent(_selectedColumn, _cells.EnumerateColumns(_selectedRow), _cells.Rows);
                    break;
                case CommandKeys.Right:
                    _selectedColumn = NextValidComponent(_selectedColumn, _cells.EnumerateColumns(_selectedRow), _cells.Rows);
                    break;
                case CommandKeys.Select:
                case CommandKeys.Back:
                    throw new ArgumentOutOfRangeException(nameof(key), key, null);
            }

            SelectComponent();
        }

        private int NextValidComponent(int lowerBound, IEnumerable<GridCell> cells, int listCount)
        {
            var firstIndex = Math.Min(lowerBound + 1, listCount);
            var nextCell = cells
                .Select((x, i) => new {Cell = x, Index = i})
                .Skip(firstIndex)
                .FirstOrDefault(x => x.Cell.IsSelectable);

            return nextCell?.Index ?? lowerBound;
        }

        private int PreviousValidComponent(int upperBound, IEnumerable<GridCell> cells, int listCount)
        {
            var lastIndex = Math.Min(upperBound, listCount);
            var nextCell = cells
                .Select((x, i) => new {Cell = x, Index = i})
                .Take(lastIndex)
                .Reverse()
                .FirstOrDefault(x => x.Cell.IsSelectable);

            return nextCell?.Index ?? upperBound;
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

        public void SetSelection(int row, int column)
        {
            UnselectComponent();
            _selectedColumn = column;
            _selectedRow = row;
            SelectComponent();
        }
    }
}