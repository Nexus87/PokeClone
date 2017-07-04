using System.Linq;
using GameEngine.Core.ECS.Components;
using GameEngine.Core.ECS.Messages;
using GameEngine.Core.GameStates;
using Microsoft.Xna.Framework;

namespace GameEngine.Core.ECS.Systems
{
    public class RenderSystem : ISystem
    {
        private readonly ScreenState _screenState;
        private readonly EntityManager _entityManager;

        public RenderSystem(ScreenState screenState, EntityManager entityManager)
        {
            _screenState = screenState;
            _entityManager = entityManager;
        }

        public void Init(MessagingSystem messagingSystem)
        {
            messagingSystem.ListenForMessage<TimerAction>(Update);
        }

        private void Update(TimerAction action)
        {

            _screenState.SpriteBatch.GraphicsDevice.SetRenderTarget(_screenState.Scene);
            _screenState.SpriteBatch.Begin();
            foreach (var renderComponent in _entityManager.GetComponentsOfType<RenderComponent>().OrderBy(x => x.Z))
            {
                _screenState.SpriteBatch.Draw(renderComponent.Texture, renderComponent.Destination, Color.White, renderComponent.SpriteEffect);
            }
            _screenState.SpriteBatch.End();
            _screenState.SpriteBatch.GraphicsDevice.SetRenderTarget(null);
        }
    }
}
