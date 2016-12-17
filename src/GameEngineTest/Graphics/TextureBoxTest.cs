using FakeItEasy;
using GameEngine.Graphics;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.General;
using GameEngineTest.TestUtils;
using Microsoft.Xna.Framework;
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
            return new TextureBox();
        }

        protected override IGraphicComponent CreateComponent()
        {
            var box = CreateEmptyBox();
            var textureStub = A.Fake<ITexture2D>();
            A.CallTo(() => textureStub.Height).Returns(10);
            A.CallTo(() => textureStub.Width).Returns(10);
            A.CallTo(() => textureStub.Bounds).Returns(new Rectangle(0, 0, 10, 10));
            box.Setup();

            box.Image = textureStub;

            return box;
        }
    }
}
