using GameEngine.Graphics.Basic;
using GameEngine.Wrapper;
using GameEngineTest.Util;
using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Graphics.Basic
{
    [TestFixture]
    public class MultilineTextboxTest : IGraphicComponentTest
    {
        MultlineTextBox box;

        [SetUp]
        public void Setup()
        {
            var fontMock = new Mock<ISpriteFont>();
            fontMock.Setup(o => o.MeasureString(It.IsAny<string>())).Returns<string>(s => new Vector2(16.0f * s.Length, 16.0f));
            box = new MultlineTextBox(2, "", fontMock.Object);

            testObj = box;
            box.Setup(contentMock.Object);
            box.Text = "Very very long text";
        }

        [TestCase]
        public void SimpleTextSplitTest()
        {
            SpriteBatchMock spriteBatch = new SpriteBatchMock();
            float X = 0.0f;
            float Y = 0.0f;
            float Width = 150.0f;
            float Height = 50.0f;

            box.X = X;
            box.Y = Y;
            box.Width = Width;
            box.Height = Height;

            int chars = box.CharsPerLine();
            string simple = new string('a', chars);
            string testString = simple;

            box.Text = testString;
            box.Draw(new GameTime(), spriteBatch);

            Assert.AreEqual(2, spriteBatch.DrawnStrings.Count);
            Assert.AreEqual(simple, spriteBatch.DrawnStrings.First.Value);
            Assert.AreEqual("", spriteBatch.DrawnStrings.Last.Value);

            spriteBatch.DrawnStrings.Clear();
            testString = simple + simple;

            box.Text = testString;
            box.Draw(new GameTime(), spriteBatch);

            Assert.AreEqual(2, spriteBatch.DrawnStrings.Count);
            foreach (var s in spriteBatch.DrawnStrings)
                Assert.AreEqual(simple, s);


            spriteBatch.DrawnStrings.Clear();
            testString = simple + " " + simple;

            box.Text = testString;
            box.Draw(new GameTime(), spriteBatch);

            Assert.AreEqual(2, spriteBatch.DrawnStrings.Count);
            foreach (var s in spriteBatch.DrawnStrings)
                Assert.AreEqual(simple, s);

            spriteBatch.DrawnStrings.Clear();
            testString = simple + "\n" + simple;

            box.Text = testString;
            box.Draw(new GameTime(), spriteBatch);

            Assert.AreEqual(2, spriteBatch.DrawnStrings.Count);
            foreach (var s in spriteBatch.DrawnStrings)
                Assert.AreEqual(simple, s);

            spriteBatch.DrawnStrings.Clear();
            testString = simple + "\t" + simple;

            box.Text = testString;
            box.Draw(new GameTime(), spriteBatch);

            Assert.AreEqual(2, spriteBatch.DrawnStrings.Count);
            foreach (var s in spriteBatch.DrawnStrings)
                Assert.AreEqual(simple, s);
        }
    }
}
