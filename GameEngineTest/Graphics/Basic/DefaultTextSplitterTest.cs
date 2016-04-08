using GameEngine.Graphics;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Graphics.Basic
{
    [TestFixture]
    public class DefaultTextSplitterTest
    {
        [TestCase(" ")]
        public void SplitText_SimpleTextWithSeparator_GetStringReturnsStrings(string seperator)
        {
            int charsPerLine = 5;
            string firstString = new string('a', charsPerLine);
            string secondString = new string('b', charsPerLine);
            var splitter = CreateSplitter();

            splitter.SplitText(charsPerLine, firstString + seperator + secondString);

            Assert.AreEqual(firstString, splitter.GetString(0));
            Assert.AreEqual(secondString, splitter.GetString(1));
        }

        [TestCase(" ")]
        public void SplitText_SimpleTextWithSeparator_LineCount(string separator)
        {
            int charsPerLine = 5;
            string testString = new string('a', charsPerLine);
            string compoundString = testString + separator + testString;
            var splitter = CreateSplitter();

            splitter.SplitText(charsPerLine, compoundString);

            Assert.AreEqual(2, splitter.Count);
        }

        [TestCase(" ")]
        public void SplitText_SeparatorAtTheEnd_IgnoreSeparator(string separator)
        {
            int charsPerLine = 5;
            string testString = new string('a', charsPerLine);

            var splitter = CreateSplitter();

            splitter.SplitText(charsPerLine, testString + separator);

            Assert.AreEqual(1, splitter.Count);
            Assert.AreEqual(testString, splitter.GetString(0));
        }

        [TestCase]
        public void SplitText_TextFitInLine_NoSplitting()
        {
            int charsPerLine = 5;
            string testString = new string('a', charsPerLine);
            var splitter = CreateSplitter();

            splitter.SplitText(charsPerLine, testString);

            Assert.AreEqual(1, splitter.Count);
            Assert.AreEqual(testString, splitter.GetString(0));
        }

        [TestCase]
        public void SplitText_LineWithoutSeparator_Split()
        {
            int charsPerLine = 5;
            string firstLine = new string('a', charsPerLine);
            string secondLine = new string('b', charsPerLine);
            var splitter = CreateSplitter();

            splitter.SplitText(charsPerLine, firstLine + secondLine);

            Assert.AreEqual(2, splitter.Count);
            Assert.AreEqual(firstLine, splitter.GetString(0));
            Assert.AreEqual(secondLine, splitter.GetString(1));

        }

        [TestCase]
        public void SplitText_SameStringTwice_Split()
        {
            int charsPerLine = 5;
            string firstLine = new string('a', charsPerLine);
            string secondLine = new string('b', charsPerLine);
            var stringToSplit = firstLine + secondLine;
            var splitter = CreateSplitter();

            splitter.SplitText(charsPerLine, stringToSplit);
            splitter.SplitText(charsPerLine, stringToSplit);

            Assert.AreEqual(2, splitter.Count);
            Assert.AreEqual(firstLine, splitter.GetString(0));
            Assert.AreEqual(secondLine, splitter.GetString(1));
        }

        [TestCase]
        public void SplitText_NullString_NoLines()
        {
            int charsPerLine = 5;
            var splitter = CreateSplitter();

            splitter.SplitText(charsPerLine, null);

            Assert.AreEqual(0, splitter.Count);
        }

        [TestCase]
        public void SplitText_NoCharsPerLine_NoLines()
        {
            string testString = "test";
            var splitter = CreateSplitter();

            splitter.SplitText(0, testString);

            Assert.AreEqual(0, splitter.Count);

        }

        [TestCase]
        public void GetString_IndexMoreThanLines_ReturnDefaultString()
        {
            string defaultString = "";
            var splitter = CreateSplitter();

            Assert.AreEqual(defaultString, splitter.GetString(0));
        }

        private DefaultTextSplitter CreateSplitter()
        {
            return new DefaultTextSplitter();
        }
    }
}
