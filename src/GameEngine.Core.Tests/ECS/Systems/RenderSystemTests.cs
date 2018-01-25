using System;
using FakeItEasy;
using FakeItEasy.Configuration;
using GameEngine.Core.ECS;
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
        private readonly IMessageBus _messageBus = A.Fake<IMessageBus>();
        private IEntityManager _entityManager;
        private ISpriteBatch _spriteBatch;

        [Test]
        public void Draws_every_entity_that_has_Texture_and_Position_Component()
        {
            Setup();
            SetupEntityManager(CreateComponenTuple());

            RenderSystem.Render(_entityManager, _messageBus);

            A.CallTo(() => _spriteBatch.Draw(A<ITexture2D>._, A<Rectangle>._, A<Color>._, A<SpriteEffects>._))
                .MustHaveHappened();
        }

        [Test]
        public void Render_calls_spritebatch_begin_and_end()
        {
            Setup();
            SetupEntityManager();

            RenderSystem.Render(_entityManager, _messageBus);

            A.CallTo(() =>
                    _spriteBatch.Begin(A<SpriteSortMode>._, A<BlendState>._, A<SamplerState>._, A<DepthStencilState>._,
                        A<RasterizerState>._, A<Effect>._, A<Matrix?>._))
                .MustHaveHappened();

            A.CallTo(() => _spriteBatch.End()).MustHaveHappened();
        }

        [Test]
        public void Renders_draws_components_in_right_order()
        {
            Setup();
            var components = new[]
            {
                CreateComponenTuple(3),
                CreateComponenTuple(2),
                CreateComponenTuple(1)
            };
            SetupEntityManager(components);

            RenderSystem.Render(_entityManager, _messageBus);

            ACallToDrawWithComponent(components[2]).MustHaveHappened()
                .Then(ACallToDrawWithComponent(components[1]).MustHaveHappened())
                .Then(ACallToDrawWithComponent(components[0]).MustHaveHappened());
        }

        private IVoidArgumentValidationConfiguration ACallToDrawWithComponent(
            (RenderComponent, PositionComponent) component)
        {
            return A.CallTo(() => _spriteBatch.Draw(component.Item1.Texture, component.Item2.Destination, A<Color>._,
                A<SpriteEffects>._));
        }

        private void SetupEntityManager(params (RenderComponent, PositionComponent)[] returnValues)
        {
            A.CallTo(() => _entityManager.GetComponentsOfType<RenderComponent, PositionComponent>())
                .Returns(returnValues ?? new (RenderComponent, PositionComponent)[0]);
        }

        private (RenderComponent, PositionComponent) CreateComponenTuple(int z = 0)
        {
            var guid = Guid.NewGuid();
            return (
                new RenderComponent(guid) {Texture = A.Dummy<ITexture2D>(), Z = z},
                new PositionComponent(guid) {Destination = A.Dummy<Rectangle>()}
                );
        }

        private void Setup()
        {
            _spriteBatch = A.Fake<ISpriteBatch>();
            _entityManager = A.Fake<IEntityManager>();
            var screen = A.Fake<IScreen>();
            A.CallTo(() => screen.SceneSpriteBatch).Returns(_spriteBatch);
            A.CallTo(() => _entityManager.GetFirstComponentOfType<RenderAreaComponent>())
                .Returns(new RenderAreaComponent(Guid.NewGuid(), screen));
        }
    }
}