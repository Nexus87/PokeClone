using GameEngine;
using GameEngine.Graphics;
using GameEngineTest.TestUtils;
using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace GameEngineTest.Graphics
{
    public class GraphicalTextStub : IGraphicalText
    {
        public float SingleCharWidth { get; set; }

        public float CalculateTextLength(string testText)
        {
            return SingleCharWidth * testText.Length;
        }

        public void Draw(ISpriteBatch batch)
        {
        }

        public float GetSingleCharWidth()
        {
            return SingleCharWidth;
        }

        public void Setup()
        {
        }

        public ISpriteFont SpriteFont { get; set; }

        public string Text { get; set; }

        public float CharHeight { get; set; }

        public float TextWidth
        {
            get { throw new NotImplementedException(); }
        }

        public float XPosition { get; set; }
        public float YPosition { get; set; }


        public float GetSingleCharWidth(float charHeight)
        {
            return SingleCharWidth;
        }
    }

    [TestFixture]
    public class TextBoxTest : IGraphicComponentTest
    {
        static public List<TestCaseData> ValidData = new List<TestCaseData>{
            new TestCaseData(150.0f, 50.0f, 32.0f),
            new TestCaseData(0.0f, 50.0f, 32.0f),
            new TestCaseData(150.0f, 0.0f, 32.0f),
            new TestCaseData(150.0f, 40.0f, 0.0f),
            new TestCaseData(150.0f, 40.0f, 50.0f)
        };

        [TestCase(150.0f, 50.0f, 32.0f)]
        [TestCase(0.0f, 50.0f, 32.0f)]
        [TestCase(150.0f, 0.0f, 32.0f)]
        [TestCase(150.0f, 40.0f, 0.0f)]
        [TestCase(150.0f, 40.0f, 50.0f)]
        public void RealTextHeigth_SetPreferredTextHeight_IsAlwaysLessThanHeight(float width, float height, float TextSize)
        {
            var box = CreateTextBox(width, height);

            box.PreferredTextHeight = TextSize;

            Assert.LessOrEqual(box.RealTextHeight, height);
        }

        [TestCase(100.0f, 50.0f, 32.0f)]
        [TestCase(100.0f, 50.0f, 50.0f)]
        [TestCase(100.0f, 50.0f, 49.0f)]
        [TestCase(100.0f, 50.0f, 0.0f)]
        [TestCase(0.0f, 50.0f, 32.0f)]
        public void RealTextHeigth_SetPreferredTextHeightLessThanHeight_IsEqualToPreferredHeight(float width, float height, float textSize)
        {
            var box = CreateTextBox(width, height);

            box.PreferredTextHeight = textSize;

            Assert.AreEqual(textSize, box.RealTextHeight, 0.0001);
        }

        [TestCase(-1.0f)]
        public void SetPreferredTextHeight_InvalidValue_ThrowsArgumentException(float TextSize)
        {
            var box = CreateTextBox(100, 100);
            Assert.Throws<ArgumentException>(() => box.PreferredTextHeight = TextSize);
        }


        [TestCase(100.0f, 50.0f, 10.0f, 1.0f, 100)]
        [TestCase(100.0f, 150.0f, 10.0f, 100.0f, 1)]
        [TestCase(100.0f, 150.0f, 10.0f, 99.0f, 1)]
        [TestCase(100.0f, 150.0f, 10.0f, 101.0f, 0)]
        public void DisplayableChars_SetPreferredTextHeight_RightNumberOfChars(float width, float height, float TextSize, float charSize, int expectedNumber)
        {
            var textStub = new GraphicalTextStub { SingleCharWidth = charSize };
            var box = CreateTextBox(width, height, textStub);
            box.PreferredTextHeight = TextSize;

            int displayedChars = box.DisplayableChars();
            
            Assert.AreEqual(expectedNumber, displayedChars);
        }


        [TestCase(100, 200, 12)]
        public void GetPreferredHeight_SettingPrefredTextSize_PreferredHeightEqualsTextSize(float width, float height, float textSize)
        {
            var box = CreateTextBox(width, height);

            box.PreferredTextHeight = textSize;
            box.Draw();
            Assert.AreEqual(textSize, box.PreferredHeight);
        }

        [TestCase(100, 200, 12)]
        public void GetPreferredHeight_CreateWithTextSize_PreferredHeightEqualsTextSize(float width, float height, float textSize)
        {
            var textStub = new GraphicalTextStub { CharHeight = textSize };
            var box = CreateTextBox(width, height, textStub);
            box.Draw();
            Assert.AreEqual(textSize, box.PreferredHeight);
        }

        [TestCase(100, 200, "test", 4)]
        public void GetPrefredWidth_SetText_PrefredWidthEqualsTextLenght(float width, float height, String text, float expectedWidth)
        {
            var textStub = new GraphicalTextStub { SingleCharWidth = 1 };
            var box = CreateTextBox(width, height, textStub);

            box.Text = text;
            box.Draw();
            Assert.AreEqual(expectedWidth, box.PreferredWidth, 10e-5);
        }

        protected override IGraphicComponent CreateComponent()
        {
            fontMock = new Mock<ISpriteFont>();
            fontMock.Setup(o => o.MeasureString(It.IsAny<string>())).Returns<string>(s => new Vector2(16.0f * s.Length, 16.0f));
            return new TextBox(fontMock.Object);
        }

        private TextBox CreateTextBox(float width, float height, GraphicalTextStub stub)
        {
            var box = new TextBox(stub);
            box.Width = width;
            box.Height = height;

            return box;
        }
        private TextBox CreateTextBox(float width, float height, float charSize = 16.0f)
        {
            var fontStub = new Mock<ISpriteFont>();
            fontStub.Setup(o => o.MeasureString(It.IsAny<string>())).Returns<string>(s => new Vector2(charSize * s.Length, charSize));
            var textBox = new TextBox(fontMock.Object);
            textBox.Height = height;
            textBox.Width = width;

            return textBox;
            
        }
    }
}
