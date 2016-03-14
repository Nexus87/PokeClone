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

            testObj = item;
        }

        [TestCaseSource(typeof(IGraphicComponentTest), "ValidCoordinates")]
        public void SelectedDrawInConstraints(float X, float Y, float Width, float Height)
        {
            item.Select();
            DrawInConstraintsTest(X, Y, Height, Width);
        }

        [TestCaseSource(typeof(IGraphicComponentTest), "ValidCoordinates")]
        public void ArrowPositionTest(float X, float Y, float Width, float Height)
        {
            SpriteBatchMock spriteBatch = new SpriteBatchMock();

            item.XPosition = X;
            item.YPosition = Y;
            item.Width = Width;
            item.Height = Height;

            item.Draw(spriteBatch);

            Assert.AreEqual(1, spriteBatch.Objects.Count);
            
            var text = spriteBatch.Objects[0];
            spriteBatch.Objects.Clear();
            item.Select();

            item.Draw(spriteBatch);

            Assert.AreEqual(2, spriteBatch.Objects.Count);
            
            var first = spriteBatch.Objects[0];
            var second = spriteBatch.Objects[1];

            // One of them is the text, the other one is the arrow
            // none of them is allowed to be left of the initial text
            Assert.LessOrEqual(first.Position.X, text.Position.X);
            Assert.LessOrEqual(second.Position.X, text.Position.X);

            Assert.IsTrue((first.Position.Equals(text.Position) && first.Size.Equals(text.Size)) ||
                (second.Position.Equals(text.Position) && second.Size.Equals(text.Size)));
        }
    }
}
