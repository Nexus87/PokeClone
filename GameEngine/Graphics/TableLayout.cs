using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameEngine.Graphics
{
    public class TableLayout : AbstractLayout
    {
        private int columns;
        private IGraphicComponent[,] components;
        private int rows;

        public TableLayout(int rows, int columns)
        {
            if (rows <= 0 || columns <= 0)
                throw new ArgumentException("Row and columns need to be greater than 0");

            this.rows = rows;
            this.columns = columns;
            components = new IGraphicComponent[rows, columns];
        }

        public void AddComponent(int row, int column, IGraphicComponent component)
        {
            components[row, column] = component;
            Invalidate();
        }

        public override void AddComponent(IGraphicComponent component)
        {
            AddComponent(0, 0, component);
        }

        public override void Setup(ContentManager content)
        {
            foreach (var component in components)
            {
                if (component == null)
                    continue;
                component.Setup(content);
            }
        }

        protected override void DrawComponents(GameTime time, SpriteBatch batch)
        {
            foreach (var component in components)
            {
                if (component != null)
                    component.Draw(time, batch);
            }
        }

        protected override void UpdateComponents()
        {
            float width = Width / columns;
            float height = Height / rows;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
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