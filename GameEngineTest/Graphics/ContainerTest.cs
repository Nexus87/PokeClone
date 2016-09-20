using GameEngine.Graphics;
using GameEngineTest.TestUtils;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Graphics
{
    [TestFixture]
    public class ContainerTest
    {
        private Mock<ILayout> layoutMock;

        [SetUp]
        public void SetUp()
        {
            layoutMock = new Mock<ILayout>();
        }

        [TestCase]
        public void Draw_PreferredWidthOfAComponentChange_LayoutContainerIsTriggered()
        {
            var componentMock = new GraphicComponentMock { HorizontalPolicy = ResizePolicy.Preferred };
            var container = CreateContainer(componentMock);

            componentMock.RaisePreferredSizeChanged();
            container.Draw();

            layoutMock.Verify(l => l.LayoutContainer(container), Times.Once);
        }

        [TestCase]
        public void Draw_PreferredHeightOfAComponentChange_LayoutContainerIsTriggered()
        {
            var componentMock = new GraphicComponentMock { VerticalPolicy = ResizePolicy.Preferred };
            var container = CreateContainer(componentMock);

            componentMock.RaisePreferredSizeChanged();
            container.Draw();

            layoutMock.Verify(l => l.LayoutContainer(container), Times.Once);
        }

        [TestCase]
        public void Draw_FixeWidthComponentSizeChanged_LayoutContainerIsTriggered()
        {
            var componentMock = new GraphicComponentMock { HorizontalPolicy = ResizePolicy.Fixed };
            var container = CreateContainer(componentMock);

            componentMock.RaiseSizeChanged();
            container.Draw();

            layoutMock.Verify(l => l.LayoutContainer(container), Times.Once);
        }

        [TestCase]
        public void Draw_FixedHightComponentSizeChanged_LayoutContainerIsTriggered()
        {
            var componentMock = new GraphicComponentMock { VerticalPolicy = ResizePolicy.Fixed };
            var container = CreateContainer(componentMock);

            componentMock.RaiseSizeChanged();
            container.Draw();

            layoutMock.Verify(l => l.LayoutContainer(container), Times.Once);
        }

        [TestCase]
        public void Draw_NoFixeWidthComponentSizeChanged_LayoutContainerIsNotTriggered()
        {
            var componentMock = new GraphicComponentMock { HorizontalPolicy = ResizePolicy.Extending };
            var container = CreateContainer(componentMock);

            componentMock.RaiseSizeChanged();
            container.Draw();

            layoutMock.Verify(l => l.LayoutContainer(container), Times.Never);
        }

        [TestCase]
        public void Draw_NoFixedHightComponentSizeChanged_LayoutContainerIsNotTriggered()
        {
            var componentMock = new GraphicComponentMock { VerticalPolicy = ResizePolicy.Extending };
            var container = CreateContainer(componentMock);

            componentMock.RaiseSizeChanged();
            container.Draw();

            layoutMock.Verify(l => l.LayoutContainer(container), Times.Never);
        }

        private Container CreateContainer(GraphicComponentMock componentMock)
        {
            var container = new Container() { Layout = layoutMock.Object};
            container.AddComponent(componentMock);
            container.Draw();
            layoutMock.ResetCalls();

            return container;
        }
    }
}
