using GameEngine.Wrapper;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using GameEngine.Graphics.Basic;

namespace GameEngine.Graphics.Layouts
{
    public class TableLayout : AbstractLayout
    {
        private int Rows { get; set; }
        private int Columns { get; set; }
        public TableLayout(int rows, int columns)
        {
            if (rows < 0 || columns < 0)
                throw new ArgumentException("Row and columns need to be positive");
        }

        private void Resize(int row, int column)
        {
            if (row < 0 || column < 0)
                throw new ArgumentException("Negative values are not allowed");
            if (row == Rows && column == Columns)
                return;
        }


        protected override void UpdateComponents(Container container)
        {
            float width = Width / Columns;
            float height = Height / Rows;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    var component = components[i, j];
                    if (component == null)
                        continue;

                    component.X = X + j * width;
                    component.Y = Y + i * height;
                    component.Width = width;
                    component.Height = height;
                }
            }
        }
    }
}