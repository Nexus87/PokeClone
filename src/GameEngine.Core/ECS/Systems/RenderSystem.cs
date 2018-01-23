using System.Linq;
using GameEngine.Core.ECS.Actions;
using GameEngine.Core.ECS.Components;
using Microsoft.Xna.Framework;

namespace GameEngine.Core.ECS.Systems
{
    public class RenderSystem
    {
        public virtual void Render(TimeAction _, IEntityManager entityManager)
        {
            var renderComonent = entityManager.GetFirstComponentOfType<RenderAreaComponent>();
            var spriteBatch = renderComonent.SpriteBatch;

            spriteBatch.Begin();
            foreach (var (renderComponent, positionComponent) in entityManager.GetComponentsOfType<RenderComponent, PositionComponent>().OrderBy(x => x.Item1.Z))
            {
                spriteBatch.Draw(renderComponent.Texture, positionComponent.Destination, Color.White, renderComponent.SpriteEffect);
            }
            spriteBatch.End();
        }
    }
}
