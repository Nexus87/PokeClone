using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine.Globals;
using GameEngine.Graphics.General;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Panels

{
    [GameType]
    public class Grid : AbstractPanel
    {
        private readonly Table<GridCell> _cells = new Table<GridCell>();
        private readonly List<RowProperty> _rowProperties = new List<RowProperty>();
        private readonly List<ColumnProperty> _columnPoperties = new List<ColumnProperty>();
        private readonly GridInputHandler _gridInputHandler;

        public void SelectComponent(int row, int column)
        {
            if (row < 0 || row >= Rows)
                throw new ArgumentOutOfRangeException(nameof(row), row, "Number of rows: " + Rows);
            if(column < 0 ||  column >= Columns)
                throw new ArgumentOutOfRangeException(nameof(column), column, "Number of columns: " + Columns);
            _gridInputHandler.SetSelection(row, column);
        }

        public Grid()
        {
            _gridInputHandler = new GridInputHandler(_cells);
        }

        internal int Rows => _rowProperties.Count;

        internal int Columns => _columnPoperties.Count;

        public bool HandleDirectionInput { get; set; } = true;
        
        public void AddRow(RowProperty property)
        {
            _rowProperties.Add(property);
            InitRow(Rows - 1);
            Invalidate();
        }

        private void InitRow(int row)
        {
            for(var column = 0; column < _columnPoperties.Count; column++)
            {
                _cells[row, column] = CreateGridCell(row, column);
            }
        }

        private GridCell CreateGridCell(int row, int column)
        {
            var cell = new GridCell(row, column, _cells);
            cell.PreferredSizeChanged += PreferredSizeChangedHandler;
            return cell;
        }

        private void PreferredSizeChangedHandler(object sender, EventArgs e)
        {
            Invalidate();
        }

        public void AddColumn(ColumnProperty property)
        {
            _columnPoperties.Add(property);
            InitColumn(_columnPoperties.Count - 1);
            Invalidate();
        }

        private void InitColumn(int column)
        {
            for (var row = 0; row < _rowProperties.Count; row++)
            {
                _cells[row, column] = CreateGridCell(row, column);
            }
        }

        public void SetComponent(IGuiComponent component, int row, int column, int rowSpan = 1, int columnSpan = 1)
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));
            if (column < 0 || column >= Columns)
                throw new ArgumentOutOfRangeException(nameof(column), "Was " + column);
            if (row < 0 || row >= Rows)
                throw new ArgumentOutOfRangeException(nameof(row), "Was " + row);

            if (_cells[row, column].GuiComponent != null)
                RemoveChild(_cells[row, column].GuiComponent);

            AddChild(component);
            _cells[row, column].SetComponent(component, rowSpan, columnSpan);
            Invalidate();
        }

        protected override void Update()
        {
            if (Rows == 0 || Columns == 0)
                return;


            var grid = new Table<Rectangle>(Rows, Columns);
            grid = SetAbsoluteWidths(grid);
            grid = LayoutPercent(grid);
            grid = SetPosition(grid);
            ApplyGridToCells(grid);
            foreach (var cell in _cells)
                cell.ApplySizeToComponent();

            CalculatePreferredSize();
        }

        private void CalculatePreferredSize()
        {
            var fixedRowSize = _rowProperties.Where(x => x.Type == ValueType.Absolute).Sum(x => x.Height);
            var autoRowSize = _rowProperties.Where(x => x.Type == ValueType.Auto)
                .Select((y, row) => row)
                .Sum(row => _cells[row, 0].PreferedHeight);

            PreferredHeight = autoRowSize + fixedRowSize;

            var fixedColumnSize = _columnPoperties.Where(x => x.Type == ValueType.Absolute).Sum(x => x.Width);
            var autoColumnSize = _columnPoperties.Where(x => x.Type == ValueType.Auto)
                .Select((y, column) => column)
                .Sum(column => _cells[0, column].PreferedWidth);

            PreferredWidth = autoColumnSize + fixedColumnSize;
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            foreach (var gridCell in _cells)
            {
                gridCell.Draw(time, batch);
            }
        }

        public override void HandleKeyInput(CommandKeys key)
        {
            if (HandleDirectionInput)
                _gridInputHandler.HandleKeyInput(key);
        }

        private void ApplyGridToCells(ITable<Rectangle> grid)
        {
            _cells.LoopOverTable((row, column) =>
                _cells[row, column].SetConstraints(grid[row, column], Area)
            );
        }

        private Table<Rectangle> SetPosition(Table<Rectangle> grid)
        {
            Globals.Extensions.LoopOverTable(Rows, Columns, (row, column) =>
            {
                var leftRec = GetComponentConstaints(row, column - 1, grid);
                var topRec = GetComponentConstaints(row - 1, column, grid);
                var currentRec = grid[row, column];
                currentRec.X = leftRec.X + leftRec.Width;
                currentRec.Y = topRec.Y + topRec.Height;

                grid[row, column] = currentRec;
            });

            return grid;
        }

        private Table<Rectangle> SetAbsoluteWidths(Table<Rectangle> grid)
        {
            Globals.Extensions.LoopOverTable(Rows, Columns, (row, column) =>
            {
                var width = GetColumnWidth(column);
                var height = GetRowHeight(row);

                grid[row, column] = new Rectangle(0, 0, (int) width, (int) height);
            });

            return grid;
        }

        private float GetColumnWidth(int column)
        {
            var columnProperty = _columnPoperties[column];
            switch (columnProperty.Type)
            {
                case ValueType.Percent:
                    return 0;
                case ValueType.Absolute:
                    return columnProperty.Width;
                case ValueType.Auto:
                    return ColumnMaxWidth(column);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private float ColumnMaxWidth(int column)
        {
            return _cells.EnumerateRows(column).Max(c => c.PreferedWidth);
        }

        private float GetRowHeight(int row)
        {
            var rowProperty = _rowProperties[row];
            switch (rowProperty.Type)
            {
                case ValueType.Percent:
                    return 0;
                case ValueType.Absolute:
                    return rowProperty.Height;
                case ValueType.Auto:
                    return RowMaxHeight(row);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private float RowMaxHeight(int row)
        {
            return _cells.EnumerateColumns(row).Max(c => c.PreferedHeight);
        }

        private Table<Rectangle> LayoutPercent(Table<Rectangle> grid)
        {
            var height = (float) Area.Height - grid.EnumerateRows(0).Sum(rec => rec.Height);
            var width = (float) Area.Width - grid.EnumerateColumns(0).Sum(rec => rec.Width);

            if (height < 0)
                height = 0;
            if (width < 0)
                width = 0;

            var totalShareColumns = _columnPoperties.Sum(p => p.Type == ValueType.Percent ? p.Share : 0);
            var totalShareRows = _rowProperties.Sum(p => p.Type == ValueType.Percent ? p.Share : 0);

            Globals.Extensions.LoopOverTable(Rows, Columns, (row, column) =>
            {
                var constraints = grid[row, column];
                if (_rowProperties[row].Type == ValueType.Percent)
                    constraints.Height = (int) ((height * _rowProperties[row].Share) / totalShareRows);
                if (_columnPoperties[column].Type == ValueType.Percent)
                    constraints.Width = (int) ((width * _columnPoperties[column].Share) / totalShareColumns);

                grid[row, column] = constraints;
            });

            return grid;
        }

        private Rectangle GetComponentConstaints(int row, int column, Table<Rectangle> grid)
        {
            var rec = new Rectangle();
            if (row < 0)
            {
                rec.Y = Area.Y;
                rec.Height = 0;
            }
            else
            {
                rec.Y = grid[row, 0].Y;
                rec.Height = grid[row, 0].Height;
            }

            if (column < 0)
            {
                rec.X = Area.X;
                rec.Width = 0;
            }
            else
            {
                rec.X = grid[0, column].X;
                rec.Width = grid[0, column].Width;
            }

            return rec;
        }

        public void RemoveColumn(int columnToBeRemoved)
        {
            if (columnToBeRemoved < 0 || columnToBeRemoved >= Columns) throw new ArgumentOutOfRangeException(nameof(columnToBeRemoved));

            foreach (var cell in _cells.EnumerateRows(columnToBeRemoved))
            {
                RemoveChild(cell.GuiComponent);
            }
            _cells.RemoveColumn(columnToBeRemoved);
            _columnPoperties.RemoveAt(columnToBeRemoved);
            Invalidate();
        }

        public void RemoveRow(int rowToBeRemoved)
        {
            if (rowToBeRemoved < 0 || rowToBeRemoved >= Rows) throw new ArgumentOutOfRangeException(nameof(rowToBeRemoved));

            foreach (var cell in _cells.EnumerateColumns(rowToBeRemoved))
            {
                RemoveChild(cell.GuiComponent);
            }
            _cells.RemoveRow(rowToBeRemoved);
            _rowProperties.RemoveAt(rowToBeRemoved);
            Invalidate();
        }

        public IGuiComponent GetComponent(int row, int column)
        {
            return _cells[row, column].GuiComponent;
        }
    }
}