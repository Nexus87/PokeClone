using System.Linq;
using GameEngine.Core.ECS.Components;
using GameEngine.Graphics.General;
using Microsoft.Xna.Framework;

namespace GameEngine.Core.ECS.Systems
{
    public class RenderSystem : ISystem
    {
        private readonly ISpriteBatch _batch;

        public RenderSystem(ISpriteBatch batch)
        {
            _batch = batch;
        }

        public void Init(MessagingSystem messagingSystem)
        {
        }

        public void Update(GameTime time, EntityManager entityManager)
        {
            foreach (var renderComponent in entityManager.GetComponentsOfType<RenderComponent>().OrderBy(x => x.Z))
            {
                _batch.Draw(renderComponent.Texture, renderComponent.Destination, Color.White, renderComponent.SpriteEffect);
            }
        }
    }
}
