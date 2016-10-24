﻿using System;
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
            var grid = new Table<Rectangle>(Rows, Columns);
            grid = SetAbsoluteWidhts(grid);
            grid = LayoutPercent(grid);
            grid = SetPosition(grid);
            ApplyGridToComponents(grid);
        }

        private void ApplyGridToComponents(ITable<Rectangle> grid)
        {
            Utils.Extensions.LoopOverTable(Rows, Columns, (row, column) =>
            {
                cells[row, column].Constraints = grid[row, column];
            });
        }

        private Table<Rectangle> SetPosition(Table<Rectangle> grid)
        {
            Utils.Extensions.LoopOverTable(Rows, Columns, (row, column) =>
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

        private Table<Rectangle> SetAbsoluteWidhts(Table<Rectangle> grid)
        {
            Utils.Extensions.LoopOverTable(Rows, Columns, (row, column) =>
            {
                var width = GetColumnWidth(column);
                var height = GetRowHeight(row);

                grid[row, column] = new Rectangle(0, 0, (int) width, (int) height);
            });

            return grid;
        }

        private float GetColumnWidth(int column)
        {
            var columnProperty = columnPoperties[column];
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
            return cells.EnumerateRows(column).Max(c => c.PreferedWidth);
        }

        private float GetRowHeight(int row)
        {
            var rowProperty = rowProperties[row];
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
            return cells.EnumerateColumns(row).Max(c => c.PreferedHeight);
        }

        private void LayoutAbsolute()
        {

        }

        private Table<Rectangle> LayoutPercent(Table<Rectangle> grid)
        {

            var height = Constraints.Height -
                         rowProperties.Sum(property => property.Type == ValueType.Percent ? 0 : property.Height);
            var width = Constraints.Width -
                        columnPoperties.Sum(property => property.Type == ValueType.Percent ? 0 : property.Width);

            var totalShareColumns = columnPoperties.Sum(p => p.Type == ValueType.Percent ? p.Share : 0);
            var totalShareRows = rowProperties.Sum(p => p.Type == ValueType.Percent ? p.Share : 0);

            Utils.Extensions.LoopOverTable(Rows, Columns, (row, column) =>
            {
                var constraints = grid[row, column];
                if (rowProperties[row].Type == ValueType.Percent)
                    constraints.Height = (int) ((height * rowProperties[row].Share) / totalShareRows);
                if (columnPoperties[column].Type == ValueType.Percent)
                    constraints.Width = (int) ((width * columnPoperties[column].Share) / totalShareColumns);

                grid[row, column] = constraints;
            });

            return grid;

        }

        private Rectangle GetComponentConstaints(int row, int column, Table<Rectangle> grid)
        {
            var rec = new Rectangle();
            if (row < 0)
            {
                rec.Y = Constraints.Y;
                rec.Height = 0;
            }
            else
            {
                rec.Y = grid[row, 0].Y;
                rec.Height = grid[row, 0].Height;
            }

            if (column < 0)
            {
                rec.X = Constraints.X;
                rec.Width = 0;
            }
            else
            {
                rec.X = grid[0, column].X;
                rec.Width = grid[0, column].Width;
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
