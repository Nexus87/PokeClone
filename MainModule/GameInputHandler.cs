using System;
using GameEngine;
using GameEngine.Registry;
using MainModule.Graphics;

namespace MainModule
{
    [GameType]
    public class GameInputHandler : IInputHandler
    {
        private readonly IGameStateComponent gameStateComponent;
        private readonly WorldScreen worldScreen;

        public GameInputHandler(IGameStateComponent gameStateComponent, WorldScreen worldScreen)
        {
            this.gameStateComponent = gameStateComponent;
            this.worldScreen = worldScreen;
        }

        public bool HandleInput(CommandKeys key)
        {
            switch (key)
            {
                case CommandKeys.Down:
                    gameStateComponent.Move(Direction.Down);
                    worldScreen.PlayerMove(Direction.Down);
                    break;
                case CommandKeys.Left:
                    gameStateComponent.Move(Direction.Left);
                    worldScreen.PlayerMove(Direction.Left);
                    break;
                case CommandKeys.Right:
                    gameStateComponent.Move(Direction.Right);
                    worldScreen.PlayerMove(Direction.Right);
                    break;
                case CommandKeys.Up:
                    gameStateComponent.Move(Direction.Up);
                    worldScreen.PlayerMove(Direction.Up);
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