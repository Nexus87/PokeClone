using GameEngine;
using GameEngine.Graphics;
using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;

namespace GameEngineTest.Graphics
{
    [TestFixture]
    public class SpriteSheetTextureTest : IGraphicComponentTest
    {
        protected override IGraphicComponent CreateComponent()
        {
            var textureMock = new Mock<ITexture2D>().Object;
            var source = new Rectangle(10, 10, 200, 200);
            return new SpriteSheetTexture(textureMock, source);
        }
    }
}