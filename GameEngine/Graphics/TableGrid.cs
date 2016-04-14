using GameEngine.Graphics.Basic;
using GameEngine.Graphics.Layouts;
using GameEngine.Graphics.Views;
using GameEngine.Wrapper;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics
{
    internal class TableGrid
    {
        private Table<ISelectableGraphicComponent> components;
        private Container itemContainer;
        private GridLayout layout;

        private bool needsUpdate = true;

        private TableIndex startIndex;
        private TableIndex endIndex;

        public int Rows
        {
            get { return layout.Rows; }
            set
            {
                layout.Rows = value;
                Invalidate();
                FixIndexesRow();
            }
        }

        public int Columns
        {
            get { return layout.Columns; }
            set
            {
                layout.Columns = value;
                Invalidate();
                FixIndexesColumn();
            }
        }

        public TableIndex StartIndex
        {
            get { return startIndex; }
            set
            {
                startIndex = value;
                Invalidate();
            }
        }
        public TableIndex EndIndex
        {
            get { return endIndex; }
            set
            {
                endIndex = value;
                Invalidate();
            }
        }

        public TableGrid(int rows, int columns, PokeEngine game)
        {
            layout = new GridLayout(rows, columns);

            components = new Table<ISelectableGraphicComponent>();

            itemContainer = new Container(game);
            itemContainer.Layout = layout;
        }

        public TableGrid(PokeEngine game) : this(0, 0, game)
        {
        }


        private void FixIndexesRow()
        {
            if (endIndex.Row >= Rows)
                endIndex.Row = Rows - 1;

            if (startIndex.Row >= Rows)
                startIndex.Row = Rows - 1;
        }

        private void FixIndexesColumn()
        {
            if (endIndex.Column >= Columns)
                endIndex.Column = Columns - 1;

            if (startIndex.Column >= Columns)
                startIndex.Column = Columns - 1;
        }

        private void Invalidate()
        {
            needsUpdate = true;
        }


        public void SetComponentAt(int row, int column, ISelectableGraphicComponent component)
        {
            if (row >= Rows)
                throw new ArgumentOutOfRangeException("row");
            if (column >= Columns)
                throw new ArgumentOutOfRangeException("column");

            components[row, column] = component;
            Invalidate();
        }

        public ISelectableGraphicComponent GetComponentAt(int row, int column)
        {
            if (row >= Rows)
                throw new ArgumentOutOfRangeException("row");
            if (column >= Columns)
                throw new ArgumentOutOfRangeException("column");

            return components[row, column];
        }

        public void SetCoordinates(float x, float y, float width, float height)
        {
            Invalidate();
            itemContainer.SetCoordinates(x, y, width, height);
        }

        public void Draw(GameTime time, ISpriteBatch spriteBatch)
        {
            if (needsUpdate)
            {
                Update();
                needsUpdate = false;
            }
            itemContainer.Draw(time, spriteBatch);
        }

        private void Update()
        {
            itemContainer.RemoveAllComponents();
            foreach(var component in components.EnumerateAlongRows())
            for (int row = startIndex.Row; row <= endIndex.Row; row++)
            {
                for (int column = startIndex.Column; column <= endIndex.Column; column++)
                {
                    itemContainer.AddComponent(components[row, column]);
                }
            }
        }
    }
}
