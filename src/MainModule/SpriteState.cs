using GameEngine.Registry;
using MainModule.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainModule
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
