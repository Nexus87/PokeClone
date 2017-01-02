using FakeItEasy;
using GameEngine.Graphics.General;
using GameEngine.GUI;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Renderers.PokemonClassicRenderer;
using GameEngineTest.TestUtils;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace GameEngineTest.Graphics
{
    [TestFixture]
    public class ImageBoxTest : IGraphicComponentTest
    {

        [TestCase]
        public void Draw_NoImage_NothingIsDrawn()
        {
            var testObj = CreateEmptyBox();
            var spriteBatch = new SpriteBatchMock();

            testObj.Draw(spriteBatch);

            Assert.IsEmpty(spriteBatch.DrawnObjects);
        }

        private static ImageBox CreateEmptyBox()
        {
            return new ImageBox(new ClassicImageBoxRenderer());
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
