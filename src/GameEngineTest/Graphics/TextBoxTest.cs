using GameEngine.Graphics;
using GameEngineTest.TestUtils;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using FakeItEasy;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.General;

namespace GameEngineTest.Graphics
{
    [TestFixture]
    public class TextBoxTest : IGraphicComponentTest
    {
        [SetUp]
        public void Setup()
        {
            FontMock = A.Fake<ISpriteFont>();
            A.CallTo(() => FontMock.MeasureString(A<string>.Ignored))
                .ReturnsLazily((string s) => new Vector2(16.0f * s.Length, 16.0f));
        }

        public static List<TestCaseData> ValidData = new List<TestCaseData>
        {
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
        public void RealTextHeigth_SetPreferredTextHeight_IsAlwaysLessThanHeight(float width, float height,
            float textSize)
        {
            var box = CreateTextBox(width, height);

            box.PreferredTextHeight = textSize;

            Assert.LessOrEqual(box.RealTextHeight, height);
        }

        [TestCase(100.0f, 50.0f, 32.0f)]
        [TestCase(100.0f, 50.0f, 50.0f)]
        [TestCase(100.0f, 50.0f, 49.0f)]
        [TestCase(100.0f, 50.0f, 0.0f)]
        [TestCase(0.0f, 50.0f, 32.0f)]
        public void RealTextHeigth_SetPreferredTextHeightLessThanHeight_IsEqualToPreferredHeight(float width,
            float height, float textSize)
        {
            var box = CreateTextBox(width, height);

            box.PreferredTextHeight = textSize;

            Assert.AreEqual(textSize, box.RealTextHeight, 0.0001);
        }

        [TestCase(-1.0f)]
        public void SetPreferredTextHeight_InvalidValue_ThrowsArgumentException(float textSize)
        {
            var box = CreateTextBox(100, 100);
            Assert.Throws<ArgumentException>(() => box.PreferredTextHeight = textSize);
        }


        [TestCase(100.0f, 50.0f, 10.0f, 1.0f, 100)]
        [TestCase(100.0f, 150.0f, 10.0f, 100.0f, 1)]
        [TestCase(100.0f, 150.0f, 10.0f, 99.0f, 1)]
        [TestCase(100.0f, 150.0f, 10.0f, 101.0f, 0)]
        public void DisplayableChars_SetPreferredTextHeight_RightNumberOfChars(float width, float height, float textSize,
            float charSize, int expectedNumber)
        {
            var textStub = new GraphicalTextStub {SingleCharWidth = charSize};
            var box = CreateTextBox(width, height, textStub);
            box.PreferredTextHeight = textSize;

            var displayedChars = box.DisplayableChars();

            Assert.AreEqual(expectedNumber, displayedChars);
        }


        [TestCase(100, 200, 12)]
        public void GetPreferredHeight_SettingPrefredTextSize_PreferredHeightEqualsTextSize(float width, float height,
            float textSize)
        {
            var box = CreateTextBox(width, height);

            box.PreferredTextHeight = textSize;
            box.Draw();
            Assert.AreEqual(textSize, box.PreferredHeight);
        }

        [TestCase(100, 200, 12)]
        public void GetPreferredHeight_CreateWithTextSize_PreferredHeightEqualsTextSize(float width, float height,
            float textSize)
        {
            var textStub = new GraphicalTextStub {CharHeight = textSize};
            var box = CreateTextBox(width, height, textStub);
            box.Draw();
            Assert.AreEqual(textSize, box.PreferredHeight);
        }

        [TestCase(100, 200, "test", 4)]
        public void GetPrefredWidth_SetText_PrefredWidthEqualsTextLenght(float width, float height, string text,
            float expectedWidth)
        {
            var textStub = new GraphicalTextStub {SingleCharWidth = 1};
            var box = CreateTextBox(width, height, textStub);

            box.Text = text;
            box.Draw();
            Assert.AreEqual(expectedWidth, box.PreferredWidth, 10e-5);
        }

        protected override IGraphicComponent CreateComponent()
        {
            return new TextBox(FontMock);
        }

        private static TextBox CreateTextBox(float width, float height, GraphicalTextStub stub)
        {
            var box = new TextBox(stub)
            {
                Area = new Rectangle(0, 0, (int) width, (int) height)
            };

            return box;
        }

        private TextBox CreateTextBox(float width, float height, float charSize = 16.0f)
        {
            A.CallTo(() => FontMock.MeasureString(A<string>.Ignored))
                .ReturnsLazily((string s) => new Vector2(charSize * s.Length, charSize));
            var textBox = new TextBox(FontMock)
            {
                Area = new Rectangle(0, 0, (int) width, (int) height)
            };

            return textBox;
        }
    }
}