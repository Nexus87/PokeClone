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
        public Mock<ISpriteFont> fontMock;

        [SetUp]
        public void Setup()
        {
            fontMock = new Mock<ISpriteFont>();
            fontMock.Setup(o => o.MeasureString(It.IsAny<string>())).Returns<string>(s => new Vector2(16.0f * s.Length, 16.0f));
            box = new TextBox("", fontMock.Object);
            testObj = box;
        }
    }
}
