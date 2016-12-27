using GameEngine.TypeRegistry;

namespace MainMode.Core
{
    [GameType]
    public class SpriteState
    {
        public int SpriteId { get; set; }
        private Direction currentDirection;
        private readonly IGameStateComponent gameStateController;
        private int x;
        private int y;

        public SpriteState(IGameStateComponent gameStateController)
        {
            this.gameStateController = gameStateController;
        }

        public void MoveDirection(Direction direction)
        {
            if (currentDirection != direction)
            {
                gameStateController.Turn(SpriteId, direction);
                currentDirection = direction;
                return;

            }
            
            gameStateController.Move(SpriteId, direction);

        }
    }
}
