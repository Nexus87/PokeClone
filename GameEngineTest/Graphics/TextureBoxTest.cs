using GameEngine;
using GameEngine.Graphics;
using GameEngineTest.TestUtils;
using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;

namespace GameEngineTest.Graphics
{
    [TestFixture]
    public class TextureBoxTest : IGraphicComponentTest
    {

        [TestCase]
        public void Draw_NoImage_NothingIsDrawn()
        {
            var testObj = CreateEmptyBox();
            var spriteBatch = new SpriteBatchMock();

            testObj.Draw(spriteBatch);

            Assert.IsEmpty(spriteBatch.DrawnObjects);
        }

        private TextureBox CreateEmptyBox()
        {
            return new TextureBox(gameStub);
        }

        protected override IGraphicComponent CreateComponent()
        {
            var box = CreateEmptyBox();
            var textureStub = new Mock<ITexture2D>();
            textureStub.Setup(o => o.Height).Returns(10);
            textureStub.Setup(o => o.Width).Returns(10);
            textureStub.Setup(o => o.Bounds).Returns(new Rectangle(0, 0, 10, 10));
            box.Setup();

            box.Image = textureStub.Object;

            return box;
        }
    }
}
