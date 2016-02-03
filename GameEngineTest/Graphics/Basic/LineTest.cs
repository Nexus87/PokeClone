using GameEngine.Graphics.Basic;
using GameEngineTest.Util;
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
    public class LineTest : IGraphicComponentTest
    {
        [SetUp]
        public void Setup()
        {
            contentMock.SetupLoad();
            var line = new Line(gameMock.Object);
            line.Setup(contentMock.Object);

            testObj = line;
        }
    }
}
