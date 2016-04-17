using GameEngine.Graphics.Basic;
using GameEngine.Graphics.Layouts;
using GameEngine.Graphics.Views;
using GameEngine.Utils;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using System;

namespace GameEngine.Graphics
{
    class TableGrid : ITableGrid
    {
        Table<ISelectableGraphicComponent> components;
        Container itemContainer;
        GridLayout layout;

        bool needsUpdate = true;

        TableIndex startIndex;
        TableIndex endIndex;

        bool autoResizeStart = false;
        bool autoResizeEnd = false;

        static readonly TableIndex? NULL = null;

        public int Rows
        {
            get { return layout.Rows; }
            set
            {
                layout.Rows = value;
                Invalidate();
                FixIndexesRow();
            }
        }

        public int Columns
        {
            get { return layout.Columns; }
            set
            {
                layout.Columns = value;
                Invalidate();
                FixIndexesColumn();
            }
        }

        public TableIndex? StartIndex
        {
            get { return autoResizeStart ? NULL : startIndex; }
            set
            {
                Invalidate();
                if (!value.HasValue)
                {
                    autoResizeStart = true;
                    startIndex = new TableIndex(0, 0);
                    return;
                }

                CheckStartRange(value.Value);
                CheckOrder(value, EndIndex);
                startIndex = value.Value;
                autoResizeStart = false;
            }
        }

        public TableIndex? EndIndex
        {
            get { return autoResizeEnd ? NULL : endIndex; }
            set
            {
                Invalidate();
                if (!value.HasValue)
                {
                    autoResizeEnd = true;
                    FixEndColumn();
                    FixEndRow();
                    return;
                }

                CheckEndRange(value.Value);
                CheckOrder(StartIndex, value);
                endIndex = value.Value;
                autoResizeEnd = false;
            }
        }

        void CheckOrder(TableIndex? start, TableIndex? end)
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

        void CheckEndRange(TableIndex index)
        {
            var row = index.Row;
            var column = index.Column;

            if (row > Rows || row < 0)
                throw new ArgumentOutOfRangeException("row");
            if (column > Columns || column < 0)
                throw new ArgumentOutOfRangeException("column");
        }

        public TableGrid(int rows, int columns, PokeEngine game)
        {
            layout = new GridLayout(rows, columns);

            components = new Table<ISelectableGraphicComponent>();

            itemContainer = new Container(game);
            itemContainer.Layout = layout;
        }

        public TableGrid(PokeEngine game) : this(0, 0, game)
        {
        }

        void FixIndexesRow()
        {
            FixEndRow();
            FixStartRow();
        }

        void FixEndRow()
        {
            if (autoResizeEnd || endIndex.Row > Rows)
                endIndex.Row = Rows;
        }

        void FixStartRow()
        {
            if (startIndex.Row >= Rows)
                startIndex.Row = Math.Max(0, Rows - 1);
        }

        void FixIndexesColumn()
        {
            FixEndColumn();
            FixStartColumn();
        }

        void FixEndColumn()
        {
            if (autoResizeEnd || endIndex.Column > Columns)
                endIndex.Column = Columns;
        }

        void FixStartColumn()
        {
            if (startIndex.Column >= Columns)
                startIndex.Column = Math.Max(0, Columns - 1);
        }

        void Invalidate()
        {
            needsUpdate = true;
        }

        public void SetComponentAt(int row, int column, ISelectableGraphicComponent component)
        {
            CheckRange(row, column);
            components[row, column] = component;
            Invalidate();
        }

        void CheckStartRange(TableIndex index)
        {
            CheckRange(index.Row, index.Column);
        }

        void CheckRange(int row, int column)
        {
            if (row >= Rows || row < 0)
                throw new ArgumentOutOfRangeException("row");
            if (column >= Columns || column < 0)
                throw new ArgumentOutOfRangeException("column");
        }

        public ISelectableGraphicComponent GetComponentAt(int row, int column)
        {
            CheckRange(row, column);

            return components[row, column];
        }

        public void SetCoordinates(float x, float y, float width, float height)
        {
            Invalidate();
            itemContainer.SetCoordinates(x, y, width, height);
        }

        public void Draw(GameTime time, ISpriteBatch spriteBatch)
        {
            if (needsUpdate)
            {
                Update();
                needsUpdate = false;
            }
            itemContainer.Draw(time, spriteBatch);
        }

        void Update()
        {
            itemContainer.RemoveAllComponents();
            var subTable = components.CreateSubtable(startIndex, endIndex);

            foreach (var component in subTable.EnumerateAlongRows())
                itemContainer.AddComponent(component);
        }
    }
}