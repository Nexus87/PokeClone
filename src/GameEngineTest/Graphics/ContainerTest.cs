using FakeItEasy;
using GameEngine.Graphics;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.Layouts;
using GameEngineTest.TestUtils;
using NUnit.Framework;

namespace GameEngineTest.Graphics
{
    [TestFixture]
    public class ContainerTest
    {
        private ILayout _layoutMock;
        private const int InitialCallTimes = 1;

        [SetUp]
        public void SetUp()
        {
            _layoutMock = A.Fake<ILayout>();
        }

        [TestCase]
        public void Draw_PreferredWidthOfAComponentChange_LayoutContainerIsTriggered()
        {
            var componentMock = new GraphicComponentMock {HorizontalPolicy = ResizePolicy.Preferred};
            var container = CreateContainer(componentMock);

            componentMock.RaisePreferredSizeChanged();
            container.Draw();

            A.CallTo(() => _layoutMock.LayoutContainer(container))
                .MustHaveHappened(Repeated.AtLeast.Times(InitialCallTimes + 1));
        }

        [TestCase]
        public void Draw_PreferredHeightOfAComponentChange_LayoutContainerIsTriggered()
        {
            var componentMock = new GraphicComponentMock {VerticalPolicy = ResizePolicy.Preferred};
            var container = CreateContainer(componentMock);

            componentMock.RaisePreferredSizeChanged();
            container.Draw();

            A.CallTo(() => _layoutMock.LayoutContainer(container))
                .MustHaveHappened(Repeated.AtLeast.Times(InitialCallTimes + 1));
        }

        [TestCase]
        public void Draw_FixeWidthComponentSizeChanged_LayoutContainerIsTriggered()
        {
            var componentMock = new GraphicComponentMock {HorizontalPolicy = ResizePolicy.Fixed};
            var container = CreateContainer(componentMock);

            componentMock.RaiseSizeChanged();
            container.Draw();

            A.CallTo(() => _layoutMock.LayoutContainer(container))
                .MustHaveHappened(Repeated.AtLeast.Times(InitialCallTimes + 1));
        }

        [TestCase]
        public void Draw_FixedHightComponentSizeChanged_LayoutContainerIsTriggered()
        {
            var componentMock = new GraphicComponentMock {VerticalPolicy = ResizePolicy.Fixed};
            var container = CreateContainer(componentMock);

            componentMock.RaiseSizeChanged();
            container.Draw();

            A.CallTo(() => _layoutMock.LayoutContainer(container))
                .MustHaveHappened(Repeated.AtLeast.Times(InitialCallTimes + 1));
        }

        [TestCase]
        public void Draw_NoFixeWidthComponentSizeChanged_LayoutContainerIsNotTriggered()
        {
            var componentMock = new GraphicComponentMock {HorizontalPolicy = ResizePolicy.Extending};
            var container = CreateContainer(componentMock);

            componentMock.RaiseSizeChanged();
            container.Draw();

            A.CallTo(() => _layoutMock.LayoutContainer(container))
                .MustHaveHappened(Repeated.Exactly.Times(InitialCallTimes));
        }

        [TestCase]
        public void Draw_NoFixedHightComponentSizeChanged_LayoutContainerIsNotTriggered()
        {
            var componentMock = new GraphicComponentMock {VerticalPolicy = ResizePolicy.Extending};
            var container = CreateContainer(componentMock);

            componentMock.RaiseSizeChanged();
            container.Draw();

            A.CallTo(() => _layoutMock.LayoutContainer(container))
                .MustHaveHappened(Repeated.Exactly.Times(InitialCallTimes));
        }

        private Container CreateContainer(IGraphicComponent componentMock)
        {
            var container = new Container() {Layout = _layoutMock};
            container.AddComponent(componentMock);
            container.Draw();

            return container;
        }
    }
}