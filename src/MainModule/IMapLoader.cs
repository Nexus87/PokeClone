using MainMode.Core.Graphics;

namespace MainMode.Core
{
    public interface IMapLoader
    {
        IMapGraphic LoadMap(Map map);
    }
}