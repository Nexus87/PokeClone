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
    [TestFixture]
    public class TextBoxTest : IGraphicComponentTest
    {
        public TextBox box;

        [SetUp]
        public void Setup()
        {
            fontMock = new Mock<ISpriteFont>();
            fontMock.Setup(o => o.MeasureString(It.IsAny<string>())).Returns<string>(s => new Vector2(16.0f * s.Length, 16.0f));
            box = new TextBox("", fontMock.Object, gameMock.Object);
            box.Text = "TestString";
        }

        static public List<TestCaseData> ValidData = new List<TestCaseData>{
            new TestCaseData(150.0f, 50.0f, 32.0f),
            new TestCaseData(0.0f, 50.0f, 32.0f),
            new TestCaseData(150.0f, 0.0f, 32.0f),
            new TestCaseData(150.0f, 40.0f, 0.0f),
            new TestCaseData(150.0f, 40.0f, 50.0f)
        };

        static public List<TestCaseData> InvalidTextSize = new List<TestCaseData>{
            new TestCaseData(-1.0f)
        };

        [TestCaseSource("ValidData")]
        public void RealTextSizeTest(float Width, float Height, float TextSize)
        {
            box.Width = Width;
            box.Height = Height;
            box.PreferedTextSize = TextSize;

            Assert.AreEqual(Width, box.Width);
            Assert.AreEqual(Height, box.Height);
            Assert.AreEqual(TextSize, box.PreferedTextSize);

            Assert.LessOrEqual(box.RealTextHeight, Height);
            if (TextSize <= Height)
                Assert.AreEqual(TextSize, box.RealTextHeight);
        }

        [TestCaseSource("InvalidTextSize")]
        public void InvalidTextSizeTest(float TextSize)
        {
            float size = box.RealTextHeight;
            Assert.Throws<ArgumentException>(() => box.PreferedTextSize = TextSize);
            Assert.AreEqual(size, box.RealTextHeight);
        }


        [TestCaseSource("ValidData")]
        public void DisplayableCharsTest(float Width, float Heigth, float TextSize)
        {
            box.Width = Width;
            box.Height = Heigth;
            box.PreferedTextSize = TextSize;

            int num = box.DisplayableChars();
            Assert.LessOrEqual(num * TextSize, Width);
        }

        protected override GameEngine.Graphics.IGraphicComponent CreateComponent()
        {
            fontMock = new Mock<ISpriteFont>();
            fontMock.Setup(o => o.MeasureString(It.IsAny<string>())).Returns<string>(s => new Vector2(16.0f * s.Length, 16.0f));
            return new TextBox("", fontMock.Object, gameMock.Object);
        }
    }
}
