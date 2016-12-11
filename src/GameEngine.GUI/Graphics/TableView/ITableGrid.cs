using GameEngine.Graphics.General;
using Microsoft.Xna.Framework;

namespace GameEngine.Graphics.TableView
{
    public interface ITableGrid
    {
        int Columns { get; set; }
        TableIndex? EndIndex { get; set; }
        int Rows { get; set; }
        TableIndex? StartIndex { get; set; }

        void Draw(GameTime time, ISpriteBatch spriteBatch);
        ISelectableGraphicComponent GetComponentAt(int row, int column);
        void SetComponentAt(int row, int column, ISelectableGraphicComponent component);
        void SetCoordinates(float x, float y, float width, float height);
    }
}