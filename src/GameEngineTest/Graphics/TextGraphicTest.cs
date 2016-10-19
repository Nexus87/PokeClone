using GameEngine;
using GameEngine.Graphics;
using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using GameEngine.Graphics.General;

namespace GameEngineTest.Graphics
{
    [TestFixture]
    public class TextGraphicTest
    {
        private TextGraphic CreateTextGraphic(float charHeight = 16.0f)
        {
            var spriteFontMock = new Mock<ISpriteFont>();
            var testObj = new TextGraphic(spriteFontMock.Object);

            spriteFontMock.Setup(o => o.MeasureString(It.IsAny<string>())).Returns<string>(s => new Vector2(charHeight * s.Length, charHeight));

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

        [TestCaseSource("InvalidData")]
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
