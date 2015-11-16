using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameEngine.Graphics
{
    public class TableLayout : AbstractLayout
    {
        public int Columns { get; private set; }
        private IGraphicComponent[,] components;
        public int Rows { get; private set; }

        public TableLayout(int rows, int columns)
        {
            if (rows < 0 || columns < 0)
                throw new ArgumentException("Row and columns need to be positive");

            this.Rows = rows;
            this.Columns = columns;
            components = new IGraphicComponent[rows, columns];
        }

        public void SetComponent(int row, int column, IGraphicComponent component)
        {
            components[row, column] = component;
            Invalidate();
        }

        public override void AddComponent(IGraphicComponent component)
        {
            SetComponent(0, 0, component);
        }

        public override void RemoveComponent(IGraphicComponent component)
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (components[i, j] == component)
                    {
                        components[i, j] = null;
                    }
                }
            }
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