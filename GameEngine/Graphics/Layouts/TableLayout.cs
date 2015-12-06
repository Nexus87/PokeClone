using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;

namespace GameEngine.Graphics.Layouts
{
    public class TableLayout : AbstractLayout
    {
        private IGraphicComponent[,] components;

        public TableLayout(int rows, int columns)
        {
            if (rows < 0 || columns < 0)
                throw new ArgumentException("Row and columns need to be positive");
            components = new IGraphicComponent[rows, columns];
        }

        public int Columns { get { return components.GetLength(1); } }
        public int Rows { get { return components.GetLength(0); } }

        public override void AddComponent(IGraphicComponent component)
        {
            SetComponent(0, 0, component);
        }

        public IGraphicComponent GetComponent(int row, int column)
        {
            if (row >= Rows || column >= Columns)
                throw new ArgumentException(row >= Rows ? "Row " : "Column" + " index out of bound");

            return components[row, column];

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

        public void SetComponent(int row, int column, IGraphicComponent component)
        {
            if (row >= Rows || column >= Columns)
                Resize(row, column);

            components[row, column] = component;
            Invalidate();
        }

        private void Resize(int row, int column)
        {
            var comp = new IGraphicComponent[row + 1, column + 1];
            Array.Copy(components, comp, components.Length);
            components = comp;
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

        protected override void DrawComponents(GameTime time, ISpriteBatch batch)
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