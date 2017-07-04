using System.Collections.Generic;
using GameEngine.Globals;
using GameEngine.Graphics.General;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Core.GameStates
{
    public class StateManager
    {
        private readonly GraphicsDevice _device;
        private readonly ScreenConstants _screenConstants;
        private readonly Stack<State> _states = new Stack<State>();

        public StateManager(GraphicsDevice device, ScreenConstants screenConstants)
        {
            _device = device;
            _screenConstants = screenConstants;
        }

        public void PushState(State state)
        {
            _states.Push(state);
            CurrentState.ScreenState = new ScreenState
            {
                Scene = new RenderTarget2D(_device, (int) _screenConstants.ScreenWidth, (int) _screenConstants.ScreenWidth),
                Gui = new RenderTarget2D(_device, (int) _screenConstants.ScreenWidth, (int) _screenConstants.ScreenWidth),
                SpriteBatch = new XnaSpriteBatch(_device)
            };
            CurrentState.Init();
        }

        public void PopState()
        {
            _states.Pop();
        }

        public State CurrentState => _states.Peek();
    }
}
