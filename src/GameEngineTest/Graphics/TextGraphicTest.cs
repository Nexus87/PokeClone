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
    public class TextGraphicTest
    {
        private static TextGraphic CreateTextGraphic(float charHeight = 16.0f)
        {
            var spriteFontMock = A.Fake<ISpriteFont>();
            var testObj = new TextGraphic(spriteFontMock);

            A.CallTo(() => spriteFontMock.MeasureString(A<string>.Ignored))
                .ReturnsLazily((string s) => new Vector2(charHeight * s.Length, charHeight));

            return testObj;
        }

        public static List<TestCaseData> InvalidData = new List<TestCaseData>{
            new TestCaseData(-1.0f),
            new TestCaseData(-100.0f)
        };

        public static List<TestCaseData> ValidPropertyData = new List<TestCaseData>{
            new TestCaseData("TestText", 32.0f, 0.0f, 0.0f),
            new TestCaseData("", 32.0f, 0.0f, 0.0f),
            new TestCaseData(" ", 32.0f, 0.0f, 0.0f),
            new TestCaseData("TestText", 0.0f, 0.0f, 0.0f)
        };

        [TestCaseSource(nameof(InvalidData))]
        public void SetCharHeight_InvalidData_ThrowsArgumentException(float textHeight)
        {
            var testObj = CreateTextGraphic();
            Assert.Throws<ArgumentException>(() => testObj.CharHeight = textHeight);
        }

        [TestCase("12345", 16.0f, 5*16.0f)]
        [TestCase(null, 16.0f, 0)]
        [TestCase("", 16.0f, 0)]
        [TestCase("1", 16.0f, 16.0f)]
        public void CalcualteTextLength_ValidString_ReturnsExpectedSize(string text, float textHeight, float expectedWidth)
        {
            var testObj = CreateTextGraphic();
            testObj.CharHeight = textHeight;

            var returnedLength = testObj.CalculateTextLength(text);
            Assert.AreEqual(expectedWidth, returnedLength);
            

        }
    }
}
