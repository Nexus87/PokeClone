using GameEngine.GUI;

namespace MainMode.Core.Graphics
{
    public interface IWorldScreenController : IGuiComponent
    {
        void PlayerTurnDirection(Direction direction);
        void PlayerMoveDirection(Direction direction);
        void SetMap(Map map);
    }
}
