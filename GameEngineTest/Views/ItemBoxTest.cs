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
            item = new ItemBox("TestText", fontMock.Object);
            fontMock.SetupMeasureString();
            contentMock.SetupLoad();
            item.Setup(contentMock.Object);

            testObj = item;
        }

        [TestCaseSource(typeof(IGraphicComponentTest), "ValidCoordinates")]
        public void SelectedDrawInConstraints(float X, float Y, float Width, float Height)
        {
            item.IsSelected = true;
            DrawInConstraintsTest(X, Y, Height, Width);
        }

        [TestCase]
        public void ArrowPositionTest()
        {
            SpriteBatchMock spriteBatch = new SpriteBatchMock();

            item.X = 50.0f;
            item.Y = 50.0f;
            item.Width = 100.0f;
            item.Height = 100.0f;

            item.Draw(spriteBatch);

            Assert.AreEqual(1, spriteBatch.Objects.Count);
            
            var text = spriteBatch.Objects[0];
            spriteBatch.Objects.Clear();
            item.IsSelected = true;

            item.Draw(spriteBatch);

            Assert.AreEqual(2, spriteBatch.Objects.Count);
            
            var first = spriteBatch.Objects[0];
            var second = spriteBatch.Objects[1];

            // One of them is the text, the other one is the arrow
            // none of them is allowed to be left of the initial text
            Assert.LessOrEqual(first.Position.X, text.Position.X);
            Assert.LessOrEqual(second.Position.X, text.Position.X);

            if (first.Position.X.CompareTo(text.Position.X) < 0)
            {
                // First one is the Arrow, second one the text
                Assert.AreEqual(text.Position, second.Position);
                Assert.AreEqual(text.Size, second.Size);
            }
            else if (first.Position.X.CompareTo(text.Position.X) == 0)
            {
                //First one is the text, the second one is the arrow
                Assert.AreEqual(text.Position, first.Position);
                Assert.AreEqual(text.Size, first.Size);
                Assert.Less(second.Position.X, text.Position.X);
            }
        }
    }
}
