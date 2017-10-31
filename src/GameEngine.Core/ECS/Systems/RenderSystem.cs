using System.Linq;
using GameEngine.Core.ECS.Components;
using GameEngine.Graphics.General;
using Microsoft.Xna.Framework;

namespace GameEngine.Core.ECS.Systems
{
    public class RenderSystem
    {

        public void Update(ISpriteBatch spriteBatch, EntityManager entityManager)
        {

            spriteBatch.Begin();
            foreach (var renderComponent in entityManager.GetComponentsOfType<RenderComponent>().OrderBy(x => x.Z))
            {
                spriteBatch.Draw(renderComponent.Texture, renderComponent.Destination, Color.White, renderComponent.SpriteEffect);
            }
            spriteBatch.End();
        }
    }
}
