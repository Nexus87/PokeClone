using GameEngine.Core.ECS.Messages;
using GameEngine.Core.GameStates;
using GameEngine.Graphics.General;
using Microsoft.Xna.Framework;

namespace GameEngine.Core.ECS.Systems
{
    public class GuiSystem : ISystem
    {
        private readonly ScreenState _screenState;

        internal GuiSystem(ScreenState screenState)
        {
            _screenState = screenState;
        }

        public void Init(MessagingSystem messagingSystem)
        {
            messagingSystem.ListenForMessage<ShowGuiAction>(m => { _screenState.IsGuiVisible = m.ShowGui; });

            messagingSystem.ListenForMessage<KeyInputAction>(HandleInput);
            messagingSystem.ListenForMessage<TimerAction>(Update);
        }

        private void HandleInput(KeyInputAction action)
        {
            if (_screenState.IsGuiVisible)
                _screenState.GuiManager.HandleKeyInput(action.Key);
        }

        private void Update(TimerAction action)
        {
            _screenState.SpriteBatch.GraphicsDevice.SetRenderTarget(_screenState.Gui);
            _screenState.SpriteBatch.GraphicsDevice.Clear(Color.Transparent);
            _screenState.SpriteBatch.Begin();
            if (_screenState.IsGuiVisible)
                _screenState.GuiManager.Draw(action.Time, _screenState.SpriteBatch);
            _screenState.SpriteBatch.End();
        }
    }
}