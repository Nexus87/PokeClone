﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine.Graphics.Layouts
{
    /// <summary>
    /// A layout that orders the components in a grid.
    /// The number of rows and columns can be set by the corresponding
    /// properties.
    /// If not enough components are given for one row, the number of
    /// columns are reduced to the number of components in the container.
    /// </summary>
    /// <remarks>
    /// The components are set from left to right. This means:
    /// | c1 | c2 | c3 | c4 |
    /// becomes:
    /// | c1 | c2 |
    ///  ---- ----
    /// | c3 | c4 |
    /// </remarks>
    public class GridLayout : AbstractLayout
    {
        public int Rows { get; set; }
        public int Columns { get; set; }

        public GridLayout(int rows, int columns)
        {
            if (rows < 0 || columns < 0)
                throw new ArgumentException("Row and columns need to be positive");
            Rows = rows;
            Columns = columns;
        }

        protected override void UpdateComponents(Container container)
        {
            var components = container.Components;

            int realColumns, realRows;
            if (Columns != 0)
            {
                // If there are not enough components, just resize the number of columns
                realColumns = Math.Min(Columns, components.Count);
                realRows = (int)Math.Ceiling(((float)components.Count) / Columns);
            }
            else
            {
                realRows = Rows;
                realColumns = (int)Math.Ceiling(((float)components.Count) / Rows);
            }

            OrderComponents(components, realColumns, realRows);
        }

        private void OrderComponents(IReadOnlyList<IGraphicComponent> components, int realColumns, int realRows)
        {
            var cellWidth = Width / realColumns;
            var cellHeight = Height / realRows;
            var currentY = YPosition;

            for (var row = 0; row < realRows; row++)
            {
                var rowComponents = components.Where((c, i) => i >= row * realColumns && i < (row + 1) * realColumns);
                OrderRow(rowComponents, currentY, cellWidth, cellHeight);

                currentY += cellHeight;
            }
        }

        private void OrderRow(IEnumerable<IGraphicComponent> rowComponents, float currentY, float cellWidth, float cellHeight)
        {
            var currentX = XPosition;
            foreach (var c in rowComponents)
            {
                c.XPosition = currentX;
                c.YPosition = currentY;

                SetComponentSize(c, cellWidth, cellHeight);

                currentX += cellWidth;
            }
        }

        private static void SetComponentSize(IGraphicComponent c, float cellWidth, float cellHeight)
        {
            switch (c.HorizontalPolicy)
            {
                case ResizePolicy.Preferred:
                    c.Width = Math.Min(cellWidth, c.PreferredWidth);
                    break;
                case ResizePolicy.Extending:
                    c.Width = cellWidth;
                    break;
            }

            switch (c.VerticalPolicy)
            {
                case ResizePolicy.Preferred:
                    c.Height = Math.Min(cellHeight, c.PreferredHeight);
                    break;
                case ResizePolicy.Extending:
                    c.Height = cellHeight;
                    break;
            }
        }
    }
}