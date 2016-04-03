using GameEngine.Graphics;
using GameEngine.Graphics.Basic;
using GameEngine.Graphics.Views;
using GameEngineTest.Util;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Views
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
            SpriteBatchMock spriteBatch = new SpriteBatchMock();
            var arrow = new TableComponentMock<TestType>();
            var textBox = new TableComponentMock<TestType>();

            var item = CreateItemBox(arrow, textBox);
            item.SetCoordinates(x, y, width, height);

            item.Draw(spriteBatch);

            Assert.False(arrow.WasDrawn);
        }

        [TestCase(5.0f, 5.0f, 150.0f, 10.0f)]
        [TestCase(0.0f, 0.0f, 150.0f, 10.0f)]
        [TestCase(0.0f, 0.0f, 0.0f, 10.0f)]
        [TestCase(0.0f, 0.0f, 150.0f, 0.0f)]
        [TestCase(0.0f, 0.0f, 50.0f, 150.0f)]
        public void Draw_SelectedDraw_ArroLeftOfText(float x, float y, float width, float height)
        {
            SpriteBatchMock spriteBatch = new SpriteBatchMock();
            var arrow = new TableComponentMock<TestType>();
            var textBox = new TableComponentMock<TestType>();

            var item = CreateItemBox(arrow, textBox);
            item.SetCoordinates(x, y, width, height);
            item.Select();

            item.Draw(spriteBatch);

            Assert.LessOrEqual(arrow.XPosition, textBox.XPosition);
        }

        private ItemBox CreateItemBox(IGraphicComponent arrow = null, ITextGraphicComponent text = null)
        {
            if (arrow == null)
                arrow = new TableComponentMock<TestType>();
            if (text == null)
                text = new TableComponentMock<TestType>();

            return new ItemBox(arrow, text, gameMock.Object);

        }

        protected override IGraphicComponent CreateComponent()
        {
            ItemBox box = CreateItemBox();
            box.Select();
            return box;
        }
    }
}
