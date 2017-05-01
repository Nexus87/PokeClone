using GameEngine.Core;
using GameEngine.ECS;
using GameEngine.ECS.Messages;
using GameEngine.Graphics.General;
using GameEngine.GUI.Messages;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.System
{
    public class GuiSystem : ISystem
    {
        private bool _guiIsVisible;
        private readonly GuiManager _guiManager;
        private readonly ISpriteBatch _spriteBatch;

        public GuiSystem(GuiManager guiManager, ISpriteBatch spriteBatch)
        {
            _guiManager = guiManager;
            _spriteBatch = spriteBatch;
        }

        public void Init(MessagingSystem messagingSystem)
        {
            messagingSystem.ListenForMessage<ShowGuiMessage>(m => _guiIsVisible = m.ShowGui);
            messagingSystem.ListenForMessage<KeyInputMessage>(HandleInput);
        }

        private void HandleInput(KeyInputMessage message)
        {
            if(_guiIsVisible)
                _guiManager.HandleKeyInput(message.Key);
        }
        public void Update(GameTime time)
        {
            if(_guiIsVisible)
                _guiManager.Draw(time, _spriteBatch);
        }
    }
}
