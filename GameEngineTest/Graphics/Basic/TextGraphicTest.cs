using GameEngine.Graphics.Basic;
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

        [TestCase]
        public void PropertiesTest()
        {
            string testText = "TestText";
            float testCoordinate = 1.0f;
            float testSize = 15.0f;

            Assert.AreEqual("", testObj.Text);
            Assert.AreEqual(0, testObj.TextWidth);

            testObj.Text = testText;
            Assert.AreEqual(testText, testObj.Text);
            
            testObj.X = testCoordinate;
            Assert.AreEqual(testCoordinate, testObj.X);

            testObj.Y = testCoordinate;
            Assert.AreEqual(testCoordinate, testObj.Y);

            testObj.TextSize = testSize;
            Assert.AreEqual(testSize, testObj.TextSize);

            Assert.AreEqual(testSize * testText.Length, testObj.TextWidth);
        }

        [TestCase]
        public void SizePredictionTest()
        {
            var spriteMock = new SpriteBatchMock();
            string Text = "TestText";
            float X = 10.0f;
            float Y = 20.0f;
            float TextHeight = 20.0f;
            float TextWidth = TextHeight * Text.Length;
      
            testObj.X = X;
            testObj.Y = Y;
            testObj.TextSize = 20.0f;

            Assert.AreEqual(TextWidth, testObj.CalculateTextLength(Text));

            testObj.Text = Text;
            Assert.AreEqual(TextWidth, testObj.TextWidth);
            
            testObj.Draw(spriteMock);

            foreach (var obj in spriteMock.Objects)
                obj.IsInConstraints(X, Y, TextWidth, TextHeight);

        }
    }
}
