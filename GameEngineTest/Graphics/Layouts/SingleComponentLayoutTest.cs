using GameEngine.Graphics;
using GameEngine.Graphics.Layouts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Graphics.Layouts
{
    [TestFixture]
    public class SingleComponentLayoutTest : ILayoutTest
    {
        SingleComponentLayout layout;
        [SetUp]
        public void Setup()
        {
            var componentMock = new Mock<IGraphicComponent>();
            layout = new SingleComponentLayout();
            layout.AddComponent(componentMock.Object);

            testLayout = layout;
        }
    }
}
