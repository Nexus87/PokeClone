using System;
using GameEngine.Core;
using GameEngine.Globals;
using GameEngine.TypeRegistry;
using MainMode.Entities;
using MainMode.Globals;

namespace MainMode.Core.Entities
{
    [GameType]
    public class PlayerSpriteEntity : SpriteEntity, IInputHandler
    {
        private readonly IEngineInterface _engineInterface;

        public PlayerSpriteEntity(SpriteId spriteId, GameStateEntity gameStateEntity, IEngineInterface engineInterface)
            : base(gameStateEntity, spriteId)
        {
            _engineInterface = engineInterface;
        }

        public void HandleKeyInput(CommandKeys key)
        {
            Direction direction;
            switch (key)
            {
                case CommandKeys.Down:
                    direction = Direction.Down;
                    break;
                case CommandKeys.Left:
                    direction = Direction.Left;
                    break;
                case CommandKeys.Right:
                    direction = Direction.Right;
                    break;
                case CommandKeys.Up:
                    direction = Direction.Up;
                    break;
                case CommandKeys.Select:
                    return;
                case CommandKeys.Back:
                    _engineInterface.Exit();
                    return;
                default:
                    throw new ArgumentOutOfRangeException(nameof(key), key, null);
            }

            if (Direction == direction)
                Move(direction);
            else
                Turn(direction);
        }

    }
}