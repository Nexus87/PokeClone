using System.Linq;
using GameEngine.Core.ECS.Components;
using GameEngine.Graphics.General;
using Microsoft.Xna.Framework;

namespace GameEngine.Core.ECS.Systems
{
    public class RenderSystem : ISystem
    {
        private readonly EntityManager _entityManager;
        private readonly ISpriteBatch _batch;

        public RenderSystem(EntityManager entityManager, ISpriteBatch batch)
        {
            _entityManager = entityManager;
            _batch = batch;
        }

        public void Init(MessagingSystem messagingSystem)
        {
        }

        public void Update(GameTime time)
        {
            foreach (var renderComponent in _entityManager.GetComponentsOfType<RenderComponent>().OrderBy(x => x.Z))
            {
                _batch.Draw(renderComponent.Texture, renderComponent.Destination, Color.White, renderComponent.SpriteEffect);
            }
        }
    }
}
