using FakeItEasy;
using GameEngine.GUI;
using GameEngineTest.TestUtils;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace GameEngineTest.Graphics
{
    [TestFixture]
    public class AbstractGraphicComponentTest : IGraphicComponentTest
    {
        private const int IntialCallTimes = 1;

        private AbstractGuiComponent CreateComponentMock()
        {
            var componentMock = A.Fake<AbstractGuiComponent>(options => options.CallsBaseMethods());

            A.CallTo(componentMock)
                .Where(x => x.Method.Name == "DrawComponent")
                .Invokes(call => ((SpriteBatchMock) call.Arguments[1]).Draw(null, Position(componentMock), Color.Black));

            return componentMock;
        }

        private AbstractGuiComponent CreateComponentMockWithoutInvalidation()
        {
            var mock = CreateComponentMock();
            mock.Update();
            return mock;
        }

        private static Rectangle Position(IGuiComponent componentMock)
        {
            return componentMock.Area;
        }

        [TestCase]
        public void Draw_FirstCall_TriggerUpdateMethod()
        {
            var componentMock = CreateComponentMock();

            componentMock.Update();

            A.CallTo(componentMock)
                .Where(x => x.Method.Name == "Update")
                .MustHaveHappened(Repeated.AtLeast.Once);
        }

        [TestCase]
        public void Draw_SetXPosition_TriggerUpdateMethod()
        {
            var componentMock = CreateComponentMockWithoutInvalidation();
            componentMock.XPosition(componentMock.XPosition() + 10.0f);

            componentMock.Update();

            A.CallTo(componentMock)
                .Where(x => x.Method.Name == "Update")
                .MustHaveHappened(Repeated.Exactly.Times(IntialCallTimes + 1));
        }

        [TestCase]
        public void Draw_SetYPosition_TriggerUpdateMethod()
        {
            var componentMock = CreateComponentMockWithoutInvalidation();
            componentMock.YPosition(componentMock.YPosition() + 10.0f);

            componentMock.Update();

            A.CallTo(componentMock)
                .Where(x => x.Method.Name == "Update")
                .MustHaveHappened(Repeated.Exactly.Times(IntialCallTimes + 1));
        }

        [TestCase]
        public void Draw_SetWidth_TriggerUpdateMethod()
        {
            var componentMock = CreateComponentMockWithoutInvalidation();
            componentMock.Width(componentMock.Width() + 10.0f);

            componentMock.Update();

            A.CallTo(componentMock)
                .Where(x => x.Method.Name == "Update")
                .MustHaveHappened(Repeated.Exactly.Times(IntialCallTimes + 1));
        }

        [TestCase]
        public void Draw_SetHeight_TriggerUpdateMethod()
        {
            var componentMock = CreateComponentMockWithoutInvalidation();
            componentMock.Height(componentMock.Height() + 10.0f);

            componentMock.Update();

            A.CallTo(componentMock)
                .Where(x => x.Method.Name == "Update")
                .MustHaveHappened(Repeated.Exactly.Times(IntialCallTimes + 1));
        }

        protected override IGuiComponent CreateComponent()
        {
            return CreateComponentMock();
        }
    }
}
