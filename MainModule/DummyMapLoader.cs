using GameEngine.Graphics;
using GameEngine.Utils;

namespace MainModule
{
    public class DummyMapLoader : IMapLoader
    {
        public void LoadMap()
        {
            throw new System.NotImplementedException();
        }

        public ITable<IGraphicComponent> GetFieldTextures()
        {
            throw new System.NotImplementedException();
        }
    }
}