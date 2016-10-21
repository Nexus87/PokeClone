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
        public float Height { get; set; }
    }

    public class ColumnProperty
    {
        public ValueType Type { get; set; }
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

        private int Rows
        {
            get { return rowProperties.Count; }
        }

        private int Columns
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
            if (component == null) throw new ArgumentNullException("component");
            if (column <= 0 || column >= Columns) throw new ArgumentOutOfRangeException("column");
            if (row <= 0 || row >= Rows) throw new ArgumentOutOfRangeException("row");

            cells[row, column] = component;
        }

        public void SetCellProperty(CellProperty cellProperty, int row, int column)
        {
            if (cellProperty == null) throw new ArgumentNullException("cellProperty");
            if (column <= 0 || column >= Columns) throw new ArgumentOutOfRangeException("column");
            if (row <= 0 || row >= Rows) throw new ArgumentOutOfRangeException("row");
        }
        public override void Update(GameTime time)
        {

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