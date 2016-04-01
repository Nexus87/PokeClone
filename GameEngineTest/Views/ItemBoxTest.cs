using GameEngine.Graphics;
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
        ItemBox item;
        
        [SetUp]
        public void Setup()
        {         
            item = new ItemBox("TestText", fontMock.Object, gameMock.Object);
            fontMock.SetupMeasureString();
            contentMock.SetupLoad();
            item.Setup(contentMock.Object);
        }

        [TestCase(5.0f, 5.0f, 150.0f, 10.0f)]
        [TestCase(0.0f, 0.0f, 150.0f, 10.0f)]
        [TestCase(0.0f, 0.0f, 0.0f, 10.0f)]
        [TestCase(0.0f, 0.0f, 150.0f, 0.0f)]
        [TestCase(0.0f, 0.0f, 50.0f, 150.0f)]
        public void SelectedDrawInConstraints(float X, float Y, float Width, float Height)
        {
            item.Select();
            Draw_WithValidData_DrawnObjectAreInConstraints(X, Y, Height, Width);
        }

        [TestCase(5.0f, 5.0f, 150.0f, 10.0f)]
        [TestCase(0.0f, 0.0f, 150.0f, 10.0f)]
        [TestCase(0.0f, 0.0f, 0.0f, 10.0f)]
        [TestCase(0.0f, 0.0f, 150.0f, 0.0f)]
        [TestCase(0.0f, 0.0f, 50.0f, 150.0f)]
        public void ArrowPositionTest(float X, float Y, float Width, float Height)
        {
            SpriteBatchMock spriteBatch = new SpriteBatchMock();

            item.XPosition = X;
            item.YPosition = Y;
            item.Width = Width;
            item.Height = Height;

            item.Draw(spriteBatch);

            Assert.AreEqual(1, spriteBatch.DrawnObjects.Count);
            
            var text = spriteBatch.DrawnObjects[0];
            spriteBatch.DrawnObjects.Clear();
            item.Select();

            item.Draw(spriteBatch);

            Assert.AreEqual(2, spriteBatch.DrawnObjects.Count);
            
            var first = spriteBatch.DrawnObjects[0];
            var second = spriteBatch.DrawnObjects[1];

            // One of them is the text, the other one is the arrow
            // none of them is allowed to be left of the initial text
            Assert.LessOrEqual(first.Position.X, text.Position.X);
            Assert.LessOrEqual(second.Position.X, text.Position.X);

            Assert.IsTrue((first.Position.Equals(text.Position) && first.Size.Equals(text.Size)) ||
                (second.Position.Equals(text.Position) && second.Size.Equals(text.Size)));
        }

        protected override IGraphicComponent CreateComponent()
        {
            return new ItemBox("TestText", fontMock.Object, gameMock.Object);
        }
    }
}
