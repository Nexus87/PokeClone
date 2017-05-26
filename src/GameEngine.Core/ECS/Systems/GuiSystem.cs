using GameEngine.Core.ECS.Messages;
using GameEngine.Graphics.General;
using Microsoft.Xna.Framework;

namespace GameEngine.Core.ECS.Systems
{
    public class GuiSystem : ISystem
    {
        private bool _guiIsVisible;
        private readonly GuiManager _guiManager;
        private readonly ISpriteBatch _spriteBatch;

        internal GuiSystem(GuiManager guiManager, ISpriteBatch spriteBatch)
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
        public void Update(GameTime time, EntityManager entityManager)
        {
            if(_guiIsVisible)
                _guiManager.Draw(time, _spriteBatch);
        }
    }
}
