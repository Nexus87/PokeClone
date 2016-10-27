using System.Collections.Generic;

namespace GameEngine.Graphics.NewGUI.Panels
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
    }
}