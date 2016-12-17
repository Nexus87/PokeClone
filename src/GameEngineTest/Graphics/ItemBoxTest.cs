using GameEngine.GUI.Graphics;
using GameEngineTest.TestUtils;
using NUnit.Framework;

namespace GameEngineTest.Graphics
{
    [TestFixture]
    public class ItemBoxTest : IGraphicComponentTest
    {

        [TestCase(5.0f, 5.0f, 150.0f, 10.0f)]
        [TestCase(0.0f, 0.0f, 150.0f, 10.0f)]
        [TestCase(0.0f, 0.0f, 0.0f, 10.0f)]
        [TestCase(0.0f, 0.0f, 150.0f, 0.0f)]
        [TestCase(0.0f, 0.0f, 50.0f, 150.0f)]
        public void Draw_UnselectedDraw_ArrowNotDrawn(float x, float y, float width, float height)
        {
            var arrow = new GraphicComponentMock();
            var textBox = new TextGraphicComponentMock();

            var item = CreateItemBox(arrow, textBox);
            item.SetCoordinates(x, y, width, height);

            item.Draw();

            Assert.False(arrow.WasDrawn);
        }

        [TestCase(5.0f, 5.0f, 150.0f, 10.0f)]
        [TestCase(0.0f, 0.0f, 150.0f, 10.0f)]
        [TestCase(0.0f, 0.0f, 0.0f, 10.0f)]
        [TestCase(0.0f, 0.0f, 150.0f, 0.0f)]
        [TestCase(0.0f, 0.0f, 50.0f, 150.0f)]
        public void Draw_SelectedDraw_ArroLeftOfText(float x, float y, float width, float height)
        {
            var arrow = new GraphicComponentMock();
            var textBox = new TextGraphicComponentMock();

            var item = CreateItemBox(arrow, textBox);
            item.SetCoordinates(x, y, width, height);
            item.Select();

            item.Draw();

            Assert.LessOrEqual(arrow.XPosition(), textBox.XPosition());
        }

        private ItemBox CreateItemBox(IGraphicComponent arrow = null, ITextGraphicComponent text = null)
        {
            if (arrow == null)
                arrow = new GraphicComponentMock();
            if (text == null)
                text = new TextGraphicComponentMock();

            return new ItemBox(arrow, text);

        }

        protected override IGraphicComponent CreateComponent()
        {
            ItemBox box = CreateItemBox();
            box.Select();
            return box;
        }
    }
}
