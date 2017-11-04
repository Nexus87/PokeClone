using System;
using FakeItEasy;
using GameEngine.Core.ECS;
using GameEngine.Core.ECS.Actions;
using GameEngine.Core.ECS.Components;
using GameEngine.Core.ECS.Systems;
using GameEngine.Graphics.General;
using GameEngine.Graphics.Textures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NUnit.Framework;

namespace GameEngine.Core.Tests.ECS.Systems
{
    public class RenderSystemTests
    {
        private ISpriteBatch _spriteBatch;
        private IEntityManager _entityManager;


        [Test]
        public void Draws_every_entity_that_has_Texture_and_Position_Component()
        {
            var sut = GetRenderSystem();
            A.CallTo(() => _entityManager.GetComponentsOfType<RenderComponent, PositionComponent>())
                .Returns(new[]
                {
                    (new RenderComponent(Guid.NewGuid()), new PositionComponent(Guid.NewGuid())),
                });

            sut.Render(new TimeAction(), _entityManager);

            A.CallTo(() => _spriteBatch.Draw(A<ITexture2D>._, A<Rectangle>._, Color.White, A<SpriteEffects>._))
                .MustHaveHappened();
        }

        private RenderSystem GetRenderSystem()
        {
            _spriteBatch = A.Fake<ISpriteBatch>();
            _entityManager  = A.Fake<IEntityManager>();
            return new RenderSystem(_spriteBatch);
        }
    }
}