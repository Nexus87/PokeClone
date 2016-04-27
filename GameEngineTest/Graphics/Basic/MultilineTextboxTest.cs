﻿using GameEngine;
using GameEngine.Graphics;
using GameEngine.Graphics.Basic;
using GameEngine.Wrapper;
using GameEngineTest.Util;
using GameEngineTest.Views;
using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Graphics.Basic
{
    public class TestableMultilineTextBox : MultlineTextBox
    {

        public TestableMultilineTextBox(int lines, ITextSplitter splitter) : base(new SpriteFontMock(), splitter, lines, new Mock<IPokeEngine>().Object) 
        { 
        }

        public ICollection ComponentStrings()
        {
            return (from c in CreatedComponents select c.Text).ToList();
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

        [TestCase]
        public void Draw_SetValidText_SplittedTextIsDrawn()
        {
            var firstLine = "String1";
            var secondLine = "String2";

            var splitterStub = CreateSplitterStub(new List<string> { firstLine, secondLine });
            var textBox = CreateTextBox(splitterStub, 2);

            textBox.Text = SAMPLE_STRING;
            textBox.Draw(new SpriteBatchMock());

            Assert.Contains(firstLine, textBox.ComponentStrings());
            Assert.Contains(secondLine, textBox.ComponentStrings());
        }

        [TestCase]
        public void NextLine_HasNoNextLines_DoesNothing()
        {
            var firstLine = "String1";
            var secondLine = "String2";

            var splitterStub = CreateSplitterStub(new List<string> { firstLine, secondLine });
            var textBox = CreateTextBox(splitterStub, 2);

            textBox.Text = SAMPLE_STRING;
            textBox.NextLine();
            textBox.Draw(new SpriteBatchMock());

            Assert.Contains(firstLine, textBox.ComponentStrings());
            Assert.Contains(secondLine, textBox.ComponentStrings());
        }

        [TestCase]
        public void NextLine_OnlyOneLineLeft_AllButFirstLineEmpty()
        {
            var firstLine = "String1";
            var secondLine = "String2";
            var thirdLine = "String3";

            var splitterStub = CreateSplitterStub(new List<string> { firstLine, secondLine, thirdLine });
            var textBox = CreateTextBox(splitterStub, 2);

            textBox.Text = SAMPLE_STRING;
            textBox.NextLine();
            textBox.Draw(new SpriteBatchMock());

            Assert.Contains(thirdLine, textBox.ComponentStrings());
            Assert.Contains("", textBox.ComponentStrings());
        }

        [TestCase]
        public void NextLine_EnoughLines_NextLinesAreDrawn()
        {
            var firstLine = "String1";
            var secondLine = "String2";
            var thirdLine = "String3";
            var fourthLine = "String4";

            var splitterStub = CreateSplitterStub(new List<string> { firstLine, secondLine, thirdLine, fourthLine });
            var textBox = CreateTextBox(splitterStub, 2);

            textBox.Text = SAMPLE_STRING;
            textBox.NextLine();
            textBox.Draw(new SpriteBatchMock());

            Assert.Contains(thirdLine, textBox.ComponentStrings());
            Assert.Contains(fourthLine, textBox.ComponentStrings());
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
            return CreateTextBox(new SplitterStub());
        }
    }
}
