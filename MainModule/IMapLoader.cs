using GameEngine.Graphics;
using GameEngine.Utils;

namespace MainModule
{
    public interface IMapLoader
    {
        void LoadMap();
        ITable<IGraphicComponent> GetFieldTextures();
    }
}