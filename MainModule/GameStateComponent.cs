using GameEngine.Registry;
using MainModule.Graphics;
using Microsoft.Xna.Framework;
using IGameComponent = GameEngine.IGameComponent;

namespace MainModule
{
    [GameService(typeof(IGameStateComponent))]
    public class GameStateComponent : IGameComponent, IGameStateComponent
    {
        private IWorldScreenController controller;

        public GameStateComponent(IWorldScreenController controller)
        {
            this.controller = controller;
        }
        public void Move(int spriteId, Direction direction)
        {
            if (IsPlayer(spriteId))
                controller.PlayerMoveDirection(direction);
        }


        private static bool IsPlayer(int spriteId)
        {
            return spriteId == 0;
        }

        public void Turn(int spriteId, Direction direction)
        {
            if (IsPlayer(spriteId))
                controller.PlayerTurnDirection(direction);
        }

        public void Initialize()
        {
            throw new System.NotImplementedException();
        }

        public void Update(GameTime time)
        {
            throw new System.NotImplementedException();
        }
    }
}