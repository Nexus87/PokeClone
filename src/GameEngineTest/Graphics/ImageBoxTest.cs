using FakeItEasy;
using GameEngine.Graphics.Textures;
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
            var renderer = new ClassicImageBoxRenderer();
            var testObj = CreateEmptyBox();
            var spriteBatch = new SpriteBatchMock();

            testObj.Update();
            renderer.Render(spriteBatch, testObj);

            Assert.IsEmpty(spriteBatch.DrawnObjects);
        }

        private static ImageBox CreateEmptyBox()
        {
            return new ImageBox();
        }

        protected override IGuiComponent CreateComponent()
        {
            var box = CreateEmptyBox();
            var textureStub = A.Fake<ITexture2D>();
            A.CallTo(() => textureStub.Height).Returns(10);
            A.CallTo(() => textureStub.Width).Returns(10);
            A.CallTo(() => textureStub.Bounds).Returns(new Rectangle(0, 0, 10, 10));

            box.Image = textureStub;

            return box;
        }
    }
}
