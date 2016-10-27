using MainModule.Graphics;

namespace MainModule
{
    public interface IMapLoader
    {
        IMapGraphic LoadMap(Map map);
    }
}