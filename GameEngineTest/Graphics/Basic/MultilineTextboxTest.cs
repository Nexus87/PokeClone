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
        SpriteBatchMock spriteBatch;
        int displayableChars;
        [SetUp]
        public void Setup()
        {
            var fontMock = new Mock<ISpriteFont>();
            fontMock.Setup(o => o.MeasureString(It.IsAny<string>())).Returns<string>(s => new Vector2(16.0f * s.Length, 16.0f));
            box = new MultlineTextBox(2, "", fontMock.Object);

            testObj = box;
            box.Setup(contentMock.Object);
            box.Text = "Very very long text";

            box = new MultlineTextBox(2, "", fontMock.Object);
            box.Setup(contentMock.Object);

            float X = 0.0f;
            float Y = 0.0f;
            float Width = 150.0f;
            float Height = 50.0f;

            box.X = X;
            box.Y = Y;
            box.Width = Width;
            box.Height = Height;

            spriteBatch = new SpriteBatchMock();
            displayableChars = box.CharsPerLine();
        }

        [TestCase]
        public void SimpleTextSplitTest()
        {
            string simple = new string('a', displayableChars);
            string testString = simple;

            box.Text = testString;
            box.Draw(new GameTime(), spriteBatch);

            Assert.AreEqual(2, spriteBatch.DrawnStrings.Count);
            Assert.AreEqual(simple, spriteBatch.DrawnStrings.First.Value);
            Assert.AreEqual("", spriteBatch.DrawnStrings.Last.Value);

        }

        public static List<TestCaseData> Separators = new List<TestCaseData>{
            new TestCaseData(" "),
            new TestCaseData("\t"),
            new TestCaseData("\n"),
            new TestCaseData("  ")
        };

        [TestCaseSource("Separators")]
        public void LineBreaksTest(string s)
        {
            Assert.IsTrue(displayableChars > 11);
            string simple = new string('a', 5);
            string testString;

            testString = simple + s + simple;

            box.Text = testString;
            box.Draw(new GameTime(), spriteBatch);

            Assert.AreEqual(2, spriteBatch.DrawnStrings.Count);
            foreach (var str in spriteBatch.DrawnStrings)
                Assert.AreEqual(simple, str);
        }

        [TestCaseSource("Separators")]
        public void LineBreatAtEndTest(string s)
        {
            Assert.IsTrue(displayableChars > 1);
            string simple = new string('a', displayableChars - 1);
            string testString;

            testString = simple + s + simple;

            box.Text = testString;
            box.Draw(new GameTime(), spriteBatch);

            Assert.AreEqual(2, spriteBatch.DrawnStrings.Count);
            foreach (var str in spriteBatch.DrawnStrings)
                Assert.AreEqual(simple, str);
        }

        [TestCaseSource("Separators")]
        public void LineBreakAtBeginningTest(string s)
        {
            string simple = new string('a', displayableChars - 1);
            string testString = simple + "a" + s + simple;
            
            // First test:
            // testString.Count == 2 * displayableChars
            box.Text = testString;
            box.Draw(new GameTime(), spriteBatch);

            Assert.AreEqual(2, spriteBatch.DrawnStrings.Count);
            Assert.Contains(simple + "a", spriteBatch.DrawnStrings);
            Assert.Contains(simple, spriteBatch.DrawnStrings);

            spriteBatch.DrawnStrings.Clear();

            // Second test
            // testString.Count > 2 * displayableChars, but after the split
            // they should fit in two lines
            box.Text = testString + "a";
            box.Draw(new GameTime(), spriteBatch);

            Assert.AreEqual(2, spriteBatch.DrawnStrings.Count);
            foreach (var str in spriteBatch.DrawnStrings)
                Assert.AreEqual(simple + "a", str);

        }

        [TestCaseSource("Separators")]
        public void LineBreakAtEndOfText(string s)
        {
            string simple = new string('a', displayableChars);
            string testString = simple + simple + s;

            box.Text = testString;
            box.Draw(new GameTime(), spriteBatch);

            Assert.AreEqual(2, spriteBatch.DrawnStrings.Count);
            foreach (var str in spriteBatch.DrawnStrings)
                Assert.AreEqual(simple, str);

            Assert.IsFalse(box.HasNext());
        }

        public static List<int> NumberOfLines = new List<int> { 0, 1, 2, 3, 4 };
        [TestCaseSource("NumberOfLines")]
        public void NextLineTest(int number)
        {
            List<string> lines = new List<string>();
            string testString = "";
            for (int i = 0; i < number; i++)
            {
                var s = new string((char)('a' + i), displayableChars - 1);
                testString += s + " ";
                lines.Add(s);
            }

            while (lines.Count < 2 || lines.Count % 2 != 0)
            {
                lines.Add("");
            }

            for (int i = 0; i < lines.Count; i += 2)
            {
                box.Draw(new GameTime(), spriteBatch);
                Assert.AreEqual(2, spriteBatch.DrawnStrings.Count);
                Assert.Contains(lines[i], spriteBatch.DrawnStrings);
                Assert.Contains(lines[i+1], spriteBatch.DrawnStrings);

                Assert.IsTrue(box.HasNext(), "Error on run " + i);
                box.NextLine();
                spriteBatch.DrawnStrings.Clear();
            }

            Assert.IsFalse(box.HasNext());
        }
    }
}
