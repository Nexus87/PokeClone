using System;
using GameEngine.Core;
using GameEngine.Globals;
using GameEngine.TypeRegistry;

namespace MainMode.Core
{
    [GameType]
    public class GameInputHandler : IInputHandler
    {
        private readonly SpriteState _spriteState;
        readonly IEngineInterface _engineInterface;

        public GameInputHandler(SpriteState spriteState, IEngineInterface engineInterface)
        {
            _engineInterface = engineInterface;
            _spriteState = spriteState;
            spriteState.SpriteId = 0;
        }

        public void HandleKeyInput(CommandKeys key)
        {
            switch (key)
            {
                case CommandKeys.Down:
                    _spriteState.MoveDirection(Direction.Down);
                    break;
                case CommandKeys.Left:
                    _spriteState.MoveDirection(Direction.Left);
                    break;
                case CommandKeys.Right:
                    _spriteState.MoveDirection(Direction.Right);
                    break;
                case CommandKeys.Up:
                    _spriteState.MoveDirection(Direction.Up);
                    break;
                case CommandKeys.Select:
                    break;
                case CommandKeys.Back:
                    _engineInterface.Exit();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(key), key, null);
            }
        }

    }
}