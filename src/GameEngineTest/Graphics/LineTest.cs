using GameEngine.Graphics;
using GameEngine.Graphics.General;
using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;

namespace GameEngineTest.Graphics
{
    [TestFixture]
    public class LineTest : IGraphicComponentTest
    {
        protected override IGraphicComponent CreateComponent()
        {
            var textureStub = new Mock<ITexture2D>();
            textureStub.Setup(o => o.Height).Returns(1);
            textureStub.Setup(o => o.Width).Returns(1);
            textureStub.Setup(o => o.Bounds).Returns(new Rectangle(0, 0, 1, 1));

            var cupTextureStub = new Mock<ITexture2D>();
            cupTextureStub.Setup(o => o.Height).Returns(10);
            cupTextureStub.Setup(o => o.Width).Returns(10);
            cupTextureStub.Setup(o => o.Bounds).Returns(new Rectangle(0, 0, 10, 10));

            var line = new Line(textureStub.Object, cupTextureStub.Object);
            line.Setup();

            return line;
        }
    }
}
