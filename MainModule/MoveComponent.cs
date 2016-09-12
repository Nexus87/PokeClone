using System;
using GameEngine;
using Microsoft.Xna.Framework;
using IGameComponent = GameEngine.IGameComponent;

namespace MainModule
{
    public class MoveComponent : IInputHandler, IGameComponent
    {
        private bool hasDirection = false;
        private CommandKeys keyDirection;
        private readonly IEngineInterface engineInterface;

        public MoveComponent(IEngineInterface engineInterface)
        {
            this.engineInterface = engineInterface;
        }

        public bool HandleInput(CommandKeys key)
        {
            if (hasDirection)
                return true;
            switch (key)
            {
                case CommandKeys.Down:
                case CommandKeys.Left:
                case CommandKeys.Right:
                case CommandKeys.Up:
                    hasDirection = true;
                    this.keyDirection = key;
                    break;
                case CommandKeys.Select:
                    engineInterface.ShowGUI();
                    break;
                case CommandKeys.Back:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("key", key, null);
            }

            return true;
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