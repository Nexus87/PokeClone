using System;
using GameEngine;
using GameEngine.Registry;
using MainModule.Graphics;

namespace MainModule
{
    [GameType]
    public class GameInputHandler : IInputHandler
    {
        private SpriteState spriteState;
        readonly IEngineInterface engineInterface;

        public GameInputHandler(SpriteState spriteState, IEngineInterface engineInterface)
        {
            this.engineInterface = engineInterface;
            this.spriteState = spriteState;
            spriteState.SpriteId = 0;
        }

        public bool HandleInput(CommandKeys key)
        {
            switch (key)
            {
                case CommandKeys.Down:
                    spriteState.MoveDirection(Direction.Down);
                    break;
                case CommandKeys.Left:
                    spriteState.MoveDirection(Direction.Left);
                    break;
                case CommandKeys.Right:
                    spriteState.MoveDirection(Direction.Right);
                    break;
                case CommandKeys.Up:
                    spriteState.MoveDirection(Direction.Up);
                    break;
                case CommandKeys.Select:
                    break;
                case CommandKeys.Back:
                    engineInterface.Exit();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("key", key, null);
            }

            return true;
        }

    }
}