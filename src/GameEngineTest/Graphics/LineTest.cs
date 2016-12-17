using FakeItEasy;
using GameEngine.Graphics;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.General;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace GameEngineTest.Graphics
{
    [TestFixture]
    public class LineTest : IGraphicComponentTest
    {
        protected override IGraphicComponent CreateComponent()
        {
            var textureStub = A.Fake<ITexture2D>();
            A.CallTo(() => textureStub.Height).Returns(1);
            A.CallTo(() => textureStub.Width).Returns(1);
            A.CallTo(() => textureStub.Bounds).Returns(new Rectangle(0, 0, 1, 1));

            var cupTextureStub = A.Fake<ITexture2D>();
            A.CallTo(() => cupTextureStub.Height).Returns(10);
            A.CallTo(() => cupTextureStub.Width).Returns(10);
            A.CallTo(() => cupTextureStub.Bounds).Returns(new Rectangle(0, 0, 10, 10));

            var line = new Line(textureStub, cupTextureStub);
            line.Setup();

            return line;
        }
    }
}
