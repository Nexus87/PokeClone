﻿using GameEngine.Graphics.Basic;
using GameEngine.Wrapper;
using GameEngineTest.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
    public class TextGraphicTest
    {
        TextGraphic testObj;
        
        [SetUp]
        public void Setup()
        {
            var spriteFontMock = new Mock<ISpriteFont>();
            var serviceMock = new Mock<IServiceProvider>();
            var contentMock = new Mock<ContentManager>(serviceMock.Object);
            testObj = new TextGraphic("FontName", spriteFontMock.Object);

            spriteFontMock.Setup(o => o.MeasureString(It.IsAny<string>())).Returns<string>(s => new Vector2(16.0f * s.Length, 16.0f));
            testObj.Setup(contentMock.Object);
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
        public void InvalidDataTest(float textHeight)
        {
            Assert.Throws<ArgumentException>(() => testObj.TextSize = textHeight);
        }

        [TestCaseSource("ValidPropertyData")]
        public void PropertiesTest(string testText, float testSize, float X, float Y)
        {

            Assert.AreEqual("", testObj.Text);
            Assert.AreEqual(0, testObj.TextWidth);

            testObj.Text = testText;
            Assert.AreEqual(testText, testObj.Text);
            
            testObj.X = X;
            Assert.AreEqual(X, testObj.X);

            testObj.Y = Y;
            Assert.AreEqual(Y, testObj.Y);

            testObj.TextSize = testSize;
            Assert.AreEqual(testSize, testObj.TextSize);

            Assert.AreEqual(testSize * testText.Length, testObj.TextWidth);
        }

        [TestCaseSource("ValidPropertyData")]
        public void SizePredictionTest(string Text, float TextHeight, float X, float Y)
        {
            var spriteMock = new SpriteBatchMock();
            float TextWidth = TextHeight * Text.Length;
      
            testObj.X = X;
            testObj.Y = Y;
            testObj.TextSize = TextHeight;

            Assert.AreEqual(TextWidth, testObj.CalculateTextLength(Text));

            testObj.Text = Text;
            Assert.AreEqual(TextWidth, testObj.TextWidth);
            
            testObj.Draw(spriteMock);

            foreach (var obj in spriteMock.Objects)
                obj.IsInConstraints(X, Y, TextWidth, TextHeight);

        }
    }
}