using System.Collections.Generic;

namespace GameEngine.GUI.Panels
{
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

        public static void AddAutoColumn(this Grid grid)
        {
            grid.AddColumn(new ColumnProperty{Type = ValueType.Auto});
        }

        public static void AddAutoRow(this Grid grid)
        {
            grid.AddRow(new RowProperty{Type = ValueType.Auto});
        }

        public static void AddAbsoluteColumn(this Grid grid, float width)
        {
            grid.AddColumn(new ColumnProperty{Type = ValueType.Absolute, Width = width});
        }

        public static void AddAbsoluteRow(this Grid grid, float height)
        {
            grid.AddRow(new RowProperty{Type = ValueType.Absolute, Height = height});
        }

        public static void AddPercentColumn(this Grid grid, int share = 1)
        {
            grid.AddColumn(new ColumnProperty{Type = ValueType.Percent, Share = share});
        }

        public static void AddPercentRow(this Grid grid, int share = 1)
        {
            grid.AddRow(new RowProperty{Type = ValueType.Percent, Share = share});
        }
    }
}