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

            splitter.SplitText(charsPerLine, firstString + seperator + secondString);

            Assert.AreEqual(firstString, splitter.GetString(0));
            Assert.AreEqual(secondString, splitter.GetString(1));
        }

        [TestCase(" ")]
        [TestCase("\t")]
        [TestCase("\n")]
        [TestCase("  ")]
        public void SplitText_SimpleTextWithSeparator_LineCount(string separator)
        {
            var charsPerLine = 5;
            var testString = new string('a', charsPerLine);
            var compoundString = testString + separator + testString;
            var splitter = CreateSplitter();

            splitter.SplitText(charsPerLine, compoundString);

            Assert.AreEqual(2, splitter.Count);
        }

        [TestCase(" ")]
        [TestCase("\t")]
        [TestCase("\n")]
        [TestCase("  ")]
        public void SplitText_SeparatorAtTheEnd_IgnoreSeparator(string separator)
        {
            var charsPerLine = 5;
            var testString = new string('a', charsPerLine);

            var splitter = CreateSplitter();

            splitter.SplitText(charsPerLine, testString + separator);

            Assert.AreEqual(1, splitter.Count);
            Assert.AreEqual(testString, splitter.GetString(0));
        }

        [TestCase]
        public void SplitText_TextFitInLine_NoSplitting()
        {
            var charsPerLine = 5;
            var testString = new string('a', charsPerLine);
            var splitter = CreateSplitter();

            splitter.SplitText(charsPerLine, testString);

            Assert.AreEqual(1, splitter.Count);
            Assert.AreEqual(testString, splitter.GetString(0));
        }

        [TestCase]
        public void SplitText_LineWithoutSeparator_Split()
        {
            var charsPerLine = 5;
            var firstLine = new string('a', charsPerLine);
            var secondLine = new string('b', charsPerLine);
            var splitter = CreateSplitter();

            splitter.SplitText(charsPerLine, firstLine + secondLine);

            Assert.AreEqual(2, splitter.Count);
            Assert.AreEqual(firstLine, splitter.GetString(0));
            Assert.AreEqual(secondLine, splitter.GetString(1));

        }

        [TestCase]
        public void SplitText_SameStingAndCharsPerLineTwice_Split()
        {
            var charsPerLine = 5;
            var firstLine = new string('a', charsPerLine);
            var secondLine = new string('b', charsPerLine);
            var stringToSplit = firstLine + secondLine;
            var splitter = CreateSplitter();

            splitter.SplitText(charsPerLine, stringToSplit);
            splitter.SplitText(charsPerLine, stringToSplit);

            Assert.AreEqual(2, splitter.Count);
            Assert.AreEqual(firstLine, splitter.GetString(0));
            Assert.AreEqual(secondLine, splitter.GetString(1));
        }


        [TestCase]
        public void SplitText_SameStingDiffrentCharsPerLineTwice_Split()
        {
            var charsPerLine = 5;
            var firstLine = new string('a', charsPerLine);
            var secondLine = new string('b', charsPerLine);
            var stringToSplit = firstLine + secondLine;
            var splitter = CreateSplitter();

            splitter.SplitText(charsPerLine, stringToSplit);
            splitter.SplitText(1, stringToSplit);

            Assert.Less(2, splitter.Count);
        }
        [TestCase]
        public void SplitText_NullString_NoLines()
        {
            var charsPerLine = 5;
            var splitter = CreateSplitter();

            splitter.SplitText(charsPerLine, null);

            Assert.AreEqual(0, splitter.Count);
        }

        [TestCase]
        public void SplitText_NoCharsPerLine_NoLines()
        {
            var testString = "test";
            var splitter = CreateSplitter();

            splitter.SplitText(0, testString);

            Assert.AreEqual(0, splitter.Count);

        }

        [TestCase]
        public void GetString_IndexMoreThanLines_ReturnDefaultString()
        {
            var defaultString = "";
            var splitter = CreateSplitter();

            Assert.AreEqual(defaultString, splitter.GetString(0));
        }

        private DefaultTextSplitter CreateSplitter()
        {
            return new DefaultTextSplitter();
        }
    }
}
