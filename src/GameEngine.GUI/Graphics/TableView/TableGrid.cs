using System;
using GameEngine.Graphics.General;
using GameEngine.Graphics.Layouts;
using GameEngine.Utils;
using Microsoft.Xna.Framework;

namespace GameEngine.Graphics.TableView
{
    public class TableGrid : ITableGrid
    {
        private readonly Table<ISelectableGraphicComponent> components;
        private readonly Container itemContainer;
        private readonly GridLayout layout;

        private bool needsUpdate = true;

        private TableIndex startIndex;
        private TableIndex endIndex;

        private bool autoResizeStart;
        private bool autoResizeEnd;

        private static readonly TableIndex? Null = null;

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
            get { return autoResizeStart ? Null : startIndex; }
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
            get { return autoResizeEnd ? Null : endIndex; }
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
                throw new ArgumentOutOfRangeException("index");
            if (column > Columns || column < 0)
                throw new ArgumentOutOfRangeException("index");
        }

        public TableGrid(int rows = 0, int columns = 0)
        {
            layout = new GridLayout(rows, columns);

            components = new Table<ISelectableGraphicComponent>();

            itemContainer = new Container {Layout = layout};
        }

        private void FixIndexesRow()
        {
            FixEndRow();
            FixStartRow();
        }

        private void FixEndRow()
        {
            if (autoResizeEnd || endIndex.Row > Rows)
                endIndex.Row = Rows;
        }

        private void FixStartRow()
        {
            if (startIndex.Row >= Rows)
                startIndex.Row = Math.Max(0, Rows - 1);
        }

        private void FixIndexesColumn()
        {
            FixEndColumn();
            FixStartColumn();
        }

        private void FixEndColumn()
        {
            if (autoResizeEnd || endIndex.Column > Columns)
                endIndex.Column = Columns;
        }

        private void FixStartColumn()
        {
            if (startIndex.Column >= Columns)
                startIndex.Column = Math.Max(0, Columns - 1);
        }

        private void Invalidate()
        {
            needsUpdate = true;
        }

        public void SetComponentAt(int row, int column, ISelectableGraphicComponent component)
        {
            CheckRange(row, column);
            components[row, column] = component;
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

        private void Update()
        {
            itemContainer.RemoveAllComponents();
            var subTable = components.CreateSubtable(startIndex, endIndex);

            foreach (var component in subTable.EnumerateAlongRows())
                itemContainer.AddComponent(component);
        }
    }
}