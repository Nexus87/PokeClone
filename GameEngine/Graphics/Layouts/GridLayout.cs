using GameEngine.Wrapper;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using GameEngine.Graphics.Basic;

namespace GameEngine.Graphics.Layouts
{
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

            float width = Width / realColumns;
            float height = Height / realRows;

            // i % realColumns will be 0 for i == 0, so rowCount is incremented
            int rowCount = -1;
            int columnCount = 0;

            for (int i = 0; i < components.Count; i++)
            {
                if (i % realColumns == 0)
                {
                    columnCount = 0;
                    rowCount++;
                }
                else
                    columnCount++;

                components[i].XPosition = XPosition + columnCount * width;
                components[i].YPosition = YPosition + rowCount * height;
                components[i].Width = width;
                components[i].Height = height;
            }
        }
    }
}