using GameEngine.GUI;
using GameEngine.GUI.Graphics;

namespace MainMode.Core.Graphics
{
    public interface IWorldScreenController : IGraphicComponent
    {
        void PlayerTurnDirection(Direction direction);
        void PlayerMoveDirection(Direction direction);
        void SetMap(Map map);
    }
}
