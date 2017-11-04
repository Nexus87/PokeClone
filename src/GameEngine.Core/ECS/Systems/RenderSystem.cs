using System.Linq;
using GameEngine.Core.ECS.Actions;
using GameEngine.Core.ECS.Components;
using GameEngine.Graphics.General;
using Microsoft.Xna.Framework;

namespace GameEngine.Core.ECS.Systems
{
    public class RenderSystem
    {
        private readonly ISpriteBatch _spriteBatch;

        public RenderSystem(ISpriteBatch spriteBatch)
        {
            this._spriteBatch = spriteBatch;
        }

        public void Render(TimeAction action, IEntityManager entityManager)
        {

            _spriteBatch.Begin();
            foreach (var (renderComponent, positionComponent) in entityManager.GetComponentsOfType<RenderComponent, PositionComponent>().OrderBy(x => x.Item1.Z))
            {
                _spriteBatch.Draw(renderComponent.Texture, positionComponent.Destination, Color.White, renderComponent.SpriteEffect);
            }
            _spriteBatch.End();
        }
    }
}
