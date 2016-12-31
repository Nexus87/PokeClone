using GameEngine.GUI;
using GameEngine.GUI.Graphics;

namespace MainMode.Core.Graphics
{
    public interface IMapController : IGraphicComponent
    {
        void CenterField(int fieldX, int fieldY);
        void MoveMap(Direction moveDirection);
        void LoadMap(Map map);
    }
}