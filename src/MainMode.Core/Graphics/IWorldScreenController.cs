using GameEngine.Graphics;

namespace MainMode.Core.Graphics
{
    public interface IWorldScreenController
    {
        void PlayerTurnDirection(Direction direction);
        void PlayerMoveDirection(Direction direction);
        void SetMap(Map map);

        Scene Scene { get; }
    }
}
