using System;
using GameEngine.GUI.Graphics.General;
using GameEngine.GUI.Panels;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using ValueType = GameEngine.GUI.Panels.ValueType;

namespace GameEngine.GUI.Graphics.TableView
{
    public class TableGrid : ITableGrid
    {
        private readonly Table<IGraphicComponent> _components;
        private readonly Grid _itemContainer;

        private bool _needsUpdate = true;

        private TableIndex _startIndex;
        private TableIndex _endIndex;

        private bool _autoResizeStart;
        private bool _autoResizeEnd;

        private static readonly TableIndex? Null = null;
        private int _rows;
        private int _columns;

        public int Rows
        {
            get { return _rows; }
            set
            {
                _rows = value;
                Invalidate();
                FixIndexesRow();
            }
        }

        public int Columns
        {
            get { return _columns; }
            set
            {
                _columns = value;
                Invalidate();
                FixIndexesColumn();
            }
        }

        public TableIndex? StartIndex
        {
            get { return _autoResizeStart ? Null : _startIndex; }
            set
            {
                Invalidate();
                if (!value.HasValue)
                {
                    _autoResizeStart = true;
                    _startIndex = new TableIndex(0, 0);
                    return;
                }

                CheckStartRange(value.Value);
                CheckOrder(value, EndIndex);
                _startIndex = value.Value;
                _autoResizeStart = false;
            }
        }

        public TableIndex? EndIndex
        {
            get { return _autoResizeEnd ? Null : _endIndex; }
            set
            {
                Invalidate();
                if (!value.HasValue)
                {
                    _autoResizeEnd = true;
                    FixEndColumn();
                    FixEndRow();
                    return;
                }

                CheckEndRange(value.Value);
                CheckOrder(StartIndex, value);
                _endIndex = value.Value;
                _autoResizeEnd = false;
            }
        }

        private void CheckOrder(TableIndex? start, TableIndex? end)
        {
            if (start == null || end == null)
                return;

            var startValue = start.Value;
            var endValue = end.Value;

            if (startValue.Row > endValue.Row || startValue.Column > endValue.Column)
            {
                throw new ArgumentOutOfRangeException("", "StartIndex needs to be less than EndIndex");
            }
        }

        private void CheckEndRange(TableIndex index)
        {
            var row = index.Row;
            var column = index.Column;

            if (row > Rows || row < 0)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (column > Columns || column < 0)
                throw new ArgumentOutOfRangeException(nameof(index));
        }

        public TableGrid(int rows = 0, int columns = 0)
        {
            _rows = rows;
            _columns = columns;
            _components = new Table<IGraphicComponent>();

            _itemContainer = new Grid();
        }

        private void FixIndexesRow()
        {
            FixEndRow();
            FixStartRow();
        }

        private void FixEndRow()
        {
            if (_autoResizeEnd || _endIndex.Row > Rows)
                _endIndex.Row = Rows;
        }

        private void FixStartRow()
        {
            if (_startIndex.Row >= Rows)
                _startIndex.Row = Math.Max(0, Rows - 1);
        }

        private void FixIndexesColumn()
        {
            FixEndColumn();
            FixStartColumn();
        }

        private void FixEndColumn()
        {
            if (_autoResizeEnd || _endIndex.Column > Columns)
                _endIndex.Column = Columns;
        }

        private void FixStartColumn()
        {
            if (_startIndex.Column >= Columns)
                _startIndex.Column = Math.Max(0, Columns - 1);
        }

        private void Invalidate()
        {
            _needsUpdate = true;
        }

        public void SetComponentAt(int row, int column, IGraphicComponent component)
        {
            CheckRange(row, column);
            _components[row, column] = component;
            Invalidate();
        }

        private void CheckStartRange(TableIndex index)
        {
            if (index.Row == 0 && index.Column == 0)
                return;
            CheckRange(index.Row, index.Column);
        }

        private void CheckRange(int row, int column)
        {
            if (row >= Rows || row < 0)
                throw new ArgumentOutOfRangeException(nameof(row));
            if (column >= Columns || column < 0)
                throw new ArgumentOutOfRangeException(nameof(column));
        }

        public IGraphicComponent GetComponentAt(int row, int column)
        {
            CheckRange(row, column);

            return _components[row, column];
        }

        public void SetCoordinates(float x, float y, float width, float height)
        {
            Invalidate();
            _itemContainer.SetCoordinates(x, y, width, height);
        }

        public void Draw(GameTime time, ISpriteBatch spriteBatch)
        {
            if (_needsUpdate)
            {
                Update();
                _needsUpdate = false;
            }
            _itemContainer.Draw(time, spriteBatch);
        }

        private void Update()
        {
            UpdateRows();
            UpdateColumns();
            var subTable = _components.CreateSubtable(_startIndex, _endIndex);
            for (var i = 0; i < subTable.Rows; i++)
            {
                for (var j = 0; j < subTable.Columns; j++)
                {
                    _itemContainer.SetComponent(subTable[i, j], i, j);
                }
            }
        }

        private void UpdateColumns()
        {
            while (_columns > _itemContainer.Columns)
            {
                _itemContainer.AddColumn(new ColumnProperty {Type = ValueType.Percent, Share = 1});
            }

            while (_columns < _itemContainer.Columns)
            {
                _itemContainer.RemoveColumn(0);
            }
        }

        private void UpdateRows()
        {
            while (_rows > _itemContainer.Rows)
            {
                _itemContainer.AddRow(new RowProperty{Type = ValueType.Percent, Share = 1});
            }

            while (_rows < _itemContainer.Rows)
            {
                _itemContainer.RemoveRow(0);
            }
        }
    }
}