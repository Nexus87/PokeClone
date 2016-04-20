using GameEngine.Graphics;
using GameEngine.Graphics.Basic;
using GameEngine.Wrapper;
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
        public void RealTextHeigth_SetPreferedTextHeight_IsAlwaysLessThanHeight(float width, float height, float TextSize)
        {
            var box = CreateTextBox(width, height);

            box.PreferedTextHeight = TextSize;

            Assert.LessOrEqual(box.RealTextHeight, height);
        }

        [TestCase(100.0f, 50.0f, 32.0f)]
        [TestCase(100.0f, 50.0f, 50.0f)]
        [TestCase(100.0f, 50.0f, 49.0f)]
        [TestCase(100.0f, 50.0f, 0.0f)]
        [TestCase(0.0f, 50.0f, 32.0f)]
        public void RealTextHeigth_SetPreferedTextHeightLessThanHeight_IsEqualToPreferedHeight(float width, float height, float textSize)
        {
            var box = CreateTextBox(width, height);

            box.PreferedTextHeight = textSize;

            Assert.AreEqual(textSize, box.RealTextHeight, 0.0001);
        }

        [TestCase(-1.0f)]
        public void SetPreferedTextHeight_InvalidValue_ThrowsArgumentException(float TextSize)
        {
            var box = CreateTextBox(100, 100);
            Assert.Throws<ArgumentException>(() => box.PreferedTextHeight = TextSize);
        }


        [TestCase(100.0f, 50.0f, 10.0f, 1.0f, 100)]
        [TestCase(100.0f, 150.0f, 10.0f, 100.0f, 1)]
        [TestCase(100.0f, 150.0f, 10.0f, 99.0f, 1)]
        [TestCase(100.0f, 150.0f, 10.0f, 101.0f, 0)]
        public void DisplayableChars_SetPreferedTextHeight_RightNumberOfChars(float width, float height, float TextSize, float charSize, int expectedNumber)
        {
            var textStub = new GraphicalTextStub { SingleCharWidth = charSize };
            var box = CreateTextBox(width, height, textStub);
            box.PreferedTextHeight = TextSize;

            int displayedChars = box.DisplayableChars();
            
            Assert.AreEqual(expectedNumber, displayedChars);
        }



        protected override IGraphicComponent CreateComponent()
        {
            fontMock = new Mock<ISpriteFont>();
            fontMock.Setup(o => o.MeasureString(It.IsAny<string>())).Returns<string>(s => new Vector2(16.0f * s.Length, 16.0f));
            return new TextBox(fontMock.Object, gameMock.Object);
        }

        private TextBox CreateTextBox(float width, float height, GraphicalTextStub stub)
        {
            var box = new TextBox(stub, gameMock.Object);
            box.Width = width;
            box.Height = height;

            return box;
        }
        private TextBox CreateTextBox(float width, float height, float charSize = 16.0f)
        {
            var fontStub = new Mock<ISpriteFont>();
            fontStub.Setup(o => o.MeasureString(It.IsAny<string>())).Returns<string>(s => new Vector2(charSize * s.Length, charSize));
            var textBox = new TextBox(fontMock.Object, gameMock.Object);
            textBox.Height = height;
            textBox.Width = width;

            return textBox;
            
        }
    }
}
