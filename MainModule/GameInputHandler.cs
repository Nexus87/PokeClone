using System;
using GameEngine;
using Microsoft.Xna.Framework;
using IGameComponent = GameEngine.IGameComponent;

namespace MainModule
{
    public class GameInputHandler : IInputHandler
    {
        private readonly IGameStateComponent gameStateComponent;

        public GameInputHandler(IGameStateComponent gameStateComponent)
        {
            this.gameStateComponent = gameStateComponent;
        }

        public bool HandleInput(CommandKeys key)
        {
            switch (key)
            {
                case CommandKeys.Down:
                    gameStateComponent.Move(Direction.Down);
                    break;
                case CommandKeys.Left:
                    gameStateComponent.Move(Direction.Left);
                    break;
                case CommandKeys.Right:
                    gameStateComponent.Move(Direction.Right);
                    break;
                case CommandKeys.Up:
                    gameStateComponent.Move(Direction.Up);
                    break;
                case CommandKeys.Select:
                    break;
                case CommandKeys.Back:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("key", key, null);
            }

            return true;
        }

    }
}