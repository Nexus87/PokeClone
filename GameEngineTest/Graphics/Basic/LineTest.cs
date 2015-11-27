using GameEngine.Graphics.Basic;
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
            var serviceMock = new Mock<IServiceProvider>();
            var contentMock = new Mock<ContentManager>(serviceMock.Object);
            var dev = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.Reach, new PresentationParameters());
            contentMock.Setup(o => o.Load<Texture2D>(It.IsAny<string>())).Returns(new Texture2D(dev, 10, 10));

            var line = new Line();
            line.Setup(contentMock.Object);

            testObj = line;
        }
    }
}
