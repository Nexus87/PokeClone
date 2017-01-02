using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using GameEngine.GUI.Components;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Renderers.PokemonClassicRenderer;
using GameEngineTest.Graphics;
using GameEngineTest.TestUtils;
using NUnit.Framework;

namespace GameEngine.GUI.Test.Controls
{
    [TestFixture]
    public class TextAreaTest : IGraphicComponentTest
    {
        private const string SampleString = "SomeText";

        [TestCase]
        public void HasNext_NoLine_ReturnsFalse()
        {
            var splitter = new SplitterStub();
            var textBox = CreateTextBox(splitter);

            Assert.IsFalse(textBox.HasNext());
        }

        [TestCase]
        public void SetText_ValidText_CallsSplitter()
        {
            var splitterMock = A.Fake<ITextSplitter>();
            var textBox = CreateTextBox(splitterMock);

            // Return empty list so we don't have to provide the splitted text
            A.CallTo(() => splitterMock.SplitText(A<int>.Ignored, A<string>.Ignored)).Returns(new List<string>());
            textBox.Text = SampleString;

            textBox.Draw(new SpriteBatchMock());

            A.CallTo(() => splitterMock.SplitText(A<int>.Ignored, SampleString))
                .MustHaveHappened(Repeated.AtLeast.Once);
        }

        [TestCase]
        public void HasNext_SetTextWithoutCallingDraw_ReturnsTrue()
        {
            var splittedText = new List<string> { "String", "String", "String" };
            var splitterStub = CreateSplitterStub();
            splitterStub.SplitTextCallback = () => splitterStub.Strings = splittedText;
            var textBox = CreateTextBox(splitterStub);

            textBox.Text = SampleString;

            Assert.IsTrue(textBox.HasNext());
        }

        [TestCase]
        public void HasNext_ReduceTheNumberOfCharsPerLine_ReturnsTrueAfterChange()
        {
            var splittedText = new List<string> { "String", "String", "String" };
            var initialLines = new List<string> { "Text", "Text" };

            var splitterStub = CreateSplitterStub(initialLines);
            var textBox = CreateTextBox(splitterStub);

            textBox.Text = SampleString;

            Assert.IsFalse(textBox.HasNext());

            splitterStub.SplitTextCallback = () => splitterStub.Strings = splittedText;
            textBox.SetCoordinates(10, 10, 100, 100);

            Assert.IsTrue(textBox.HasNext());
        }

        [TestCase]
        public void Draw_SetValidText_SplittedTextIsDrawn()
        {
            const string firstLine = "String1";
            const string secondLine = "String2";

            var splitterStub = CreateSplitterStub(new List<string> { firstLine, secondLine });
            var textBox = CreateTextBox(splitterStub);

            textBox.Text = SampleString;
            textBox.Draw(new SpriteBatchMock());

            var componentStrings = textBox.Lines.Select(x => x.Text).ToList();
            Assert.Contains(firstLine, componentStrings);
            Assert.Contains(secondLine, componentStrings);
        }

        [TestCase]
        public void NextLine_HasNoNextLines_DoesNothing()
        {
            const string firstLine = "String1";
            const string secondLine = "String2";

            var splitterStub = CreateSplitterStub(new List<string> { firstLine, secondLine });
            var textBox = CreateTextBox(splitterStub);

            textBox.Text = SampleString;
            textBox.NextLine();
            textBox.Draw(new SpriteBatchMock());

            var componentStrings = textBox.Lines.Select(x => x.Text).ToList();
            Assert.Contains(firstLine, componentStrings);
            Assert.Contains(secondLine, componentStrings);
        }

        [TestCase]
        public void NextLine_CalledTwiceWithNoNextLine_DoesNothing()
        {
            const string firstLine = "String1";
            const string secondLine = "String2";

            var splitterStub = CreateSplitterStub(new List<string> { firstLine, secondLine });
            var textBox = CreateTextBox(splitterStub);

            textBox.Text = SampleString;
            textBox.NextLine();
            textBox.NextLine();
            textBox.Draw(new SpriteBatchMock());

            var componentStrings = textBox.Lines.Select(x => x.Text).ToList();
            Assert.Contains(firstLine, componentStrings);
            Assert.Contains(secondLine, componentStrings);
        }

        [TestCase]
        public void NextLine_OnlyOneLineLeft_AllButFirstLineEmpty()
        {
            const string firstLine = "String1";
            const string secondLine = "String2";
            const string thirdLine = "String3";

            var splitterStub = CreateSplitterStub(new List<string> { firstLine, secondLine, thirdLine });
            var textBox = CreateTextBox(splitterStub);

            textBox.Text = SampleString;
            textBox.NextLine();
            textBox.Draw(new SpriteBatchMock());

            var componentStrings = textBox.Lines.Select(x => x.Text).ToList();
            Assert.Contains(thirdLine, componentStrings);
        }

        [TestCase]
        public void NextLine_EnoughLines_NextLinesAreDrawn()
        {
            const string firstLine = "String1";
            const string secondLine = "String2";
            const string thirdLine = "String3";
            const string fourthLine = "String4";

            var splitterStub = CreateSplitterStub(new List<string> { firstLine, secondLine, thirdLine, fourthLine });
            var textBox = CreateTextBox(splitterStub);

            textBox.Text = SampleString;
            textBox.NextLine();

            var componentStrings = textBox.Lines.Select(x => x.Text).ToList();
            Assert.Contains(thirdLine, componentStrings);
            Assert.Contains(fourthLine, componentStrings);
        }

        private static SplitterStub CreateSplitterStub(List<string> initialLines = null)
        {
            var splitter = new SplitterStub();
            if (initialLines != null)
                splitter.Strings = initialLines;

            return splitter;
        }
        private TextArea CreateTextBox(ITextSplitter splitter)
        {
            var box = new TextArea(new ClassicTextAreaRenderer(FontMock), splitter);
            box.SetCoordinates(10, 10, 300, 400);
            return box;
        }

        protected override IGraphicComponent CreateComponent()
        {
            return CreateTextBox(new SplitterStub());
        }
    }
}
