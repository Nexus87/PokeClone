using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine.Utils;
using Microsoft.Xna.Framework;

namespace GameEngine.Graphics.NewGUI

{
    public class GridProperty
    {
        public int RowSpan { get; set; }
        public int ColumnSpan { get; set; }
    }

    public enum ValueType
    {
        Percent,
        Absolute,
        Auto
    }
    public class RowProperty
    {
        public ValueType Type { get; set; }
        public int Share { get; set; }
        public float Height { get; set; }
    }

    public class ColumnProperty
    {
        public ValueType Type { get; set; }
        public int Share { get; set; }
        public float Width { get; set; }
    }

    public class CellProperty
    {
        public Vector2? Size { get; set; }
        public float[] Margins = {0, 0, 0, 0};
    }

    public class Grid : AbstractGraphicComponent
    {

        private readonly Table<IGraphicComponent> cells = new Table<IGraphicComponent>();
        private readonly List<RowProperty> rowProperties = new List<RowProperty>();
        private readonly List<ColumnProperty> columnPoperties = new List<ColumnProperty>();

        internal int Rows
        {
            get { return rowProperties.Count; }
        }

        internal int Columns
        {
            get { return columnPoperties.Count; }
        }

        public void AddRow(RowProperty property)
        {
            rowProperties.Add(property);
        }

        public void AddColumn(ColumnProperty property)
        {
            columnPoperties.Add(property);
        }

        public void SetComponent(IGraphicComponent component, int row, int column)
        {
            if (component == null)
                throw new ArgumentNullException("component");
            if (column < 0 || column >= Columns)
                throw new ArgumentOutOfRangeException("column", "Was " + column);
            if (row < 0 || row >= Rows)
                throw new ArgumentOutOfRangeException("row", "Was " + row);

            cells[row, column] = component;
        }

        public void SetCellProperty(CellProperty cellProperty, int row, int column)
        {
            if (cellProperty == null) throw new ArgumentNullException("cellProperty");
            if (column < 0 || column >= Columns) throw new ArgumentOutOfRangeException("column");
            if (row < 0 || row >= Rows) throw new ArgumentOutOfRangeException("row");
        }
        public override void Update(GameTime time)
        {
            Layout();
        }

        private void Layout()
        {

            var height = (float) Constraints.Height;
            var width = (float)Constraints.Width;

            var totalShareColumns = columnPoperties.Sum(p => p.Type == ValueType.Percent ? p.Share : 0);
            var totalShareRows = rowProperties.Sum(p => p.Type == ValueType.Percent ? p.Share : 0);

            Utils.Extensions.LoopOverTable(Rows, Columns, (row, column) =>
            {
                var leftRec = GetComponentConstaints(row, column - 1);
                var topRec = GetComponentConstaints(row - 1, column);
                var rowProperty = rowProperties[row];
                var columnProperty = columnPoperties[column];

                var currentComponentRec = new Rectangle(
                    x: leftRec.X + leftRec.Width, y: topRec.Y + topRec.Height,
                    width: (int) ((width * columnProperty.Share) / totalShareColumns), height: (int) ((height * rowProperty.Share) / totalShareRows)
                );

                cells[row, column].Constraints = currentComponentRec;
            });

        }

        private Rectangle GetComponentConstaints(int row, int column)
        {
            var rec = new Rectangle();
            if (row < 0)
            {
                rec.Y = Constraints.Y;
                rec.Height = 0;
            }
            else
            {
                rec.Y = cells[row, 0].Constraints.Y;
                rec.Height = cells[row, 0].Constraints.Height;
            }

            if (column < 0)
            {
                rec.X = Constraints.X;
                rec.Width = 0;
            }
            else
            {
                rec.X = cells[0, column].Constraints.X;
                rec.Width = cells[0, column].Constraints.Width;
            }

            return rec;

        }

    }

    public static class GridExtensions
    {
        public static void AddAllRows(this Grid grid, IEnumerable<RowProperty> rowProperties)
        {
            foreach (var rowProperty in rowProperties)
            {
                grid.AddRow(rowProperty);
            }
        }

        public static void AddAllColumns(this Grid grid, IEnumerable<ColumnProperty> columnProperties)
        {
            foreach (var columnProperty in columnProperties)
            {
                grid.AddColumn(columnProperty);
            }
        }
    }
}
