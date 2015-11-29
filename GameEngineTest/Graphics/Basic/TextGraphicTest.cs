using GameEngine.Graphics.Basic;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
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
            testObj = new TextGraphic("FontName", spriteFontMock.Object);

            spriteFontMock.Setup(o => o.MeasureString(It.IsAny<string>())).Returns<string>(s => new Vector2(16.0f * s.Length, 16.0f);
        }

        [TestCase]
        public void PropertiesTest()
        {

        }
    }
}
