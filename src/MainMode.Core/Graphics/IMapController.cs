using GameEngine.GUI;

namespace MainMode.Core.Graphics
{
    public interface IMapController : IGuiComponent
    {
        void CenterField(int fieldX, int fieldY);
        void MoveMap(Direction moveDirection);
        void LoadMap(Map map);
    }
}