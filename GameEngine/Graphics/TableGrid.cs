using GameEngine.Graphics.Views;
using GameEngine.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics
{
    internal class TableGrid
    {
        public int Rows { get; set; }
        public int Columns { get; set; }

        public TableIndex StartIndex { get; set; }
        public TableIndex EndIndex { get; set; }

        public void SetComponentAt(int row, int column, ISelectableGraphicComponent component)
        {
            throw new NotImplementedException();
        }
        public ISelectableGraphicComponent GetComponentAt(int row, int column)
        {
            throw new NotImplementedException();
        }

        public void SetCoordinates(float x, float y, float width, float height)
        {
            throw new NotImplementedException();
        }

        public void Draw(ISpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
}
