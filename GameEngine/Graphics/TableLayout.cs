using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics
{
    public class TableLayout : ILayout
    {
        Vector2 position;
        Vector2 size;

        int marginLeft;
        int marginRight;
        int marginTop;
        int marginBottom;

        int columns;
        int rows;

        IGraphicComponent parent;
        IGraphicComponent[,] components;

        public TableLayout(int rows, int columns )
        {
            if (rows <= 0 || columns <= 0)
                throw new ArgumentException("Row and columns need to be greater than 0");

            this.rows = rows;
            this.columns = columns;
            components = new IGraphicComponent[rows, columns];
        }

        public void Setup(ContentManager content)
        {
            foreach (var component in components)
            {
                if (component == null)
                    continue;
                component.Setup(content);
            }
        }
        public void Init(IGraphicComponent component)
        {
            parent = component;
            position.X = component.X;
            position.Y = component.Y;

            size.X = component.Width;
            size.Y = component.Height;

            CaclulateOffsets();
        }

        public void AddComponent(int row, int column, IGraphicComponent component)
        {
            components[row, column] = component;
        }

        public void Draw(GameTime time, SpriteBatch batch)
        {
            foreach (var component in components)
            {
                if(component != null)
                    component.Draw(time, batch);
            }
        }

        void CaclulateOffsets()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    var component = components[i, j];
                    if (component == null)
                        continue;

                    float width = size.X - marginLeft - marginRight;
                    float height = size.Y - marginTop - marginBottom;

                    component.X = position.X + j * size.X / columns + marginLeft;
                    component.Y = position.Y + i * size.Y / rows + marginTop;
                    component.Width = width / columns;
                    component.Height = height / rows;
                }
            }
        }


        public void AddComponent(IGraphicComponent component)
        {
            AddComponent(0, 0, component);
        }


        public void SetMargin(int left = 0, int right = 0, int top = 0, int bottom = 0)
        {
            marginLeft = left;
            marginRight = right;
            marginTop = top;
            marginBottom = bottom;
        }
    }
}
