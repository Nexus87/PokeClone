using GameEngine;
using GameEngine.Graphics;
using GameEngine.Graphics.Basic;
using GameEngine.Wrapper;
using GameEngineTest.Util;
using GameEngineTest.Views;
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
    public class TestableMultilineTextBox : MultlineTextBox
    {

        public TestableMultilineTextBox(int lines, ITextSplitter splitter) : base(new SpriteFontMock(), splitter, lines, new PokeEngine()) 
        { 
        }

        public List<TextGraphicComponentMock> CreatedComponents = new List<TextGraphicComponentMock>();

        protected override ITextGraphicComponent CreateTextComponent(ISpriteFont font)
        {
            var component = new TextGraphicComponentMock();
            CreatedComponents.Add(component);

            return component;
        }
    }

    public class SplitterStub : ITextSplitter
    {
        public List<string> Strings = new List<string>();
        public Action SplitTextCallback = null;
        public string GetString(int index)
        {
            return index < Strings.Count ? Strings[index] : "";
        }

        public int Count
        {
            get { return Strings.Count; }
        }

        public void SplitText(int charsPerLine, string text)
        {
            if (SplitTextCallback != null)
                SplitTextCallback();
        }
    }

    [TestFixture]
    public class MultilineTextboxTest : IGraphicComponentTest
    {
        private readonly string SAMPLE_STRING = "SomeText";
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
            var splitterMock = new Mock<ITextSplitter>();
            var textBox = CreateTextBox(splitterMock.Object);

            // Return 0 so we don't have to provide the splitted text
            splitterMock.Setup(o => o.Count).Returns(0);
            textBox.Text = SAMPLE_STRING;

            textBox.Draw(new SpriteBatchMock());

            splitterMock.Verify(o => o.SplitText(It.IsAny<int>(), SAMPLE_STRING), Times.AtLeastOnce());
        }

        [TestCase]
        public void HasNext_SetTextWithoutCallingDraw_ReturnsTrue()
        {
            var splittedText = new List<string> { "String", "String", "String" };
            var splitterStub = CreateSplitterStub();
            splitterStub.SplitTextCallback = () => splitterStub.Strings = splittedText;
            var textBox = CreateTextBox(splitterStub);
            
            textBox.Text = SAMPLE_STRING;

            Assert.IsTrue(textBox.HasNext());
        }

        [TestCase]
        public void HasNext_ReduceTheNumberOfCharsPerLine_ReturnsTrueAfterChange()
        {
            var splittedText = new List<string> { "String", "String", "String" };
            var initialLines = new List<string> { "Text", "Text" };

            var splitterStub = CreateSplitterStub(initialLines);
            var textBox = CreateTextBox(splitterStub, 2);

            textBox.Text = SAMPLE_STRING;

            Assert.IsFalse(textBox.HasNext());

            splitterStub.SplitTextCallback = () => splitterStub.Strings = splittedText;
            textBox.SetCoordinates(10, 10, 100, 100);

            Assert.IsTrue(textBox.HasNext());
        }

        private SplitterStub CreateSplitterStub(List<string> initialLines = null)
        {
            var splitter = new SplitterStub();
            if (initialLines != null)
                splitter.Strings = initialLines;

            return splitter;
        }
        private TestableMultilineTextBox CreateTextBox(ITextSplitter splitter, int numberOfLines = 2)
        {
            var box = new TestableMultilineTextBox(numberOfLines, splitter);
            box.SetCoordinates(10, 10, 300, 400);
            box.Setup();
            return box;
        }

        protected override IGraphicComponent CreateComponent()
        {
            var fontMock = new Mock<ISpriteFont>();
            fontMock.Setup(o => o.MeasureString(It.IsAny<string>())).Returns<string>(s => new Vector2(16.0f * s.Length, 16.0f));
            var box = new MultlineTextBox(fontMock.Object, new DefaultTextSplitter(), 2, gameMock.Object);
            box.Text = "Text Text Text Text Text Text";
            box.Setup();
            return box;
        }
    }
}
