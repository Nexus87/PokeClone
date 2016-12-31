using GameEngine.GUI;

namespace MainMode.Core.Graphics
{
    public interface IWorldScreenController : IGraphicComponent
    {
        void PlayerTurnDirection(Direction direction);
        void PlayerMoveDirection(Direction direction);
        void SetMap(Map map);
    }
}
