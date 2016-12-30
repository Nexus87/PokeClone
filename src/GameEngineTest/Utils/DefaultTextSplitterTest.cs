using System.Linq;
using GameEngine.GUI.Utils;
using NUnit.Framework;

namespace GameEngineTest.Utils
{
    [TestFixture]
    public class DefaultTextSplitterTest
    {
        [TestCase(" ")]
        [TestCase("\t")]
        [TestCase("\n")]
        [TestCase("  ")]
        public void SplitText_SimpleTextWithSeparator_GetStringReturnsStrings(string seperator)
        {
            const int charsPerLine = 5;
            var firstString = new string('a', charsPerLine);
            var secondString = new string('b', charsPerLine);
            var splitter = CreateSplitter();

            var lines = splitter.SplitText(charsPerLine, firstString + seperator + secondString).ToList();

            Assert.AreEqual(firstString, lines[0]);
            Assert.AreEqual(secondString, lines[1]);
        }

        [TestCase(" ")]
        [TestCase("\t")]
        [TestCase("\n")]
        [TestCase("  ")]
        public void SplitText_SimpleTextWithSeparator_LineCount(string separator)
        {
            const int charsPerLine = 5;
            var testString = new string('a', charsPerLine);
            var compoundString = testString + separator + testString;
            var splitter = CreateSplitter();

            var lines = splitter.SplitText(charsPerLine, compoundString);

            Assert.AreEqual(2, lines.Count());
        }

        [TestCase(" ")]
        [TestCase("\t")]
        [TestCase("\n")]
        [TestCase("  ")]
        public void SplitText_SeparatorAtTheEnd_IgnoreSeparator(string separator)
        {
            const int charsPerLine = 5;
            var testString = new string('a', charsPerLine);

            var splitter = CreateSplitter();

            var lines = splitter.SplitText(charsPerLine, testString + separator).ToList();

            Assert.AreEqual(1, lines.Count);
            Assert.AreEqual(testString, lines[0]);
        }

        [TestCase]
        public void SplitText_TextFitInLine_NoSplitting()
        {
            const int charsPerLine = 5;
            var testString = new string('a', charsPerLine);
            var splitter = CreateSplitter();

            var lines = splitter.SplitText(charsPerLine, testString).ToList();

            Assert.AreEqual(1, lines.Count);
            Assert.AreEqual(testString, lines[0]);
        }

        [TestCase]
        public void SplitText_LineWithoutSeparator_Split()
        {
            const int charsPerLine = 5;
            var firstLine = new string('a', charsPerLine);
            var secondLine = new string('b', charsPerLine);
            var splitter = CreateSplitter();

            var lines = splitter.SplitText(charsPerLine, firstLine + secondLine).ToList();

            Assert.AreEqual(2, lines.Count);
            Assert.AreEqual(firstLine, lines[0]);
            Assert.AreEqual(secondLine, lines[1]);

        }

        [TestCase]
        public void SplitText_NullString_NoLines()
        {
            const int charsPerLine = 5;
            var splitter = CreateSplitter();

            var lines = splitter.SplitText(charsPerLine, null);

            Assert.AreEqual(0, lines.Count());
        }

        [TestCase]
        public void SplitText_NoCharsPerLine_NoLines()
        {
            const string testString = "test";
            var splitter = CreateSplitter();

            var lines = splitter.SplitText(0, testString);

            Assert.AreEqual(0, lines.Count());

        }

        private static DefaultTextSplitter CreateSplitter()
        {
            return new DefaultTextSplitter();
        }
    }
}
