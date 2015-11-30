using GameEngine.Graphics.Basic;
using GameEngine.Wrapper;
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
            box = new TextBox("", fontMock.Object);
            testObj = box;
        }
    }
}
