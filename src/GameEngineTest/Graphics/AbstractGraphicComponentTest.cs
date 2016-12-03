using FakeItEasy;
using GameEngine.Graphics;
using GameEngineTest.TestUtils;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace GameEngineTest.Graphics
{
    [TestFixture]
    public class AbstractGraphicComponentTest : IGraphicComponentTest
    {
        private const int IntialCallTimes = 1;

        private AbstractGraphicComponent CreateComponentMock()
        {
            var componentMock = A.Fake<AbstractGraphicComponent>(options => options.CallsBaseMethods());

            A.CallTo(componentMock)
                .Where(x => x.Method.Name == "DrawComponent")
                .Invokes(call => ((SpriteBatchMock) call.Arguments[1]).Draw(null, Position(componentMock), Color.Black));

            return componentMock;
        }

        private AbstractGraphicComponent CreateComponentMockWithoutInvalidation()
        {
            var mock = CreateComponentMock();
            mock.Draw();
            return mock;
        }

        private static Rectangle Position(IGraphicComponent componentMock)
        {
            return new Rectangle((int)componentMock.XPosition, (int)componentMock.YPosition, (int)componentMock.Width, (int)componentMock.Height);
        }

        [TestCase]
        public void Draw_FirstCall_TriggerUpdateMethod()
        {
            var componentMock = CreateComponentMock();

            componentMock.Draw();

            A.CallTo(componentMock)
                .Where(x => x.Method.Name == "Update")
                .MustHaveHappened(Repeated.AtLeast.Once);
        }

        [TestCase]
        public void Draw_SetXPosition_TriggerUpdateMethod()
        {
            var componentMock = CreateComponentMockWithoutInvalidation();
            componentMock.XPosition += 10.0f;

            componentMock.Draw();

            A.CallTo(componentMock)
                .Where(x => x.Method.Name == "Update")
                .MustHaveHappened(Repeated.Exactly.Times(IntialCallTimes + 1));
        }

        [TestCase]
        public void Draw_SetYPosition_TriggerUpdateMethod()
        {
            var componentMock = CreateComponentMockWithoutInvalidation();
            componentMock.YPosition += 10.0f;

            componentMock.Draw();

            A.CallTo(componentMock)
                .Where(x => x.Method.Name == "Update")
                .MustHaveHappened(Repeated.Exactly.Times(IntialCallTimes + 1));
        }

        [TestCase]
        public void Draw_SetWidth_TriggerUpdateMethod()
        {
            var componentMock = CreateComponentMockWithoutInvalidation();
            componentMock.Width += 10.0f;

            componentMock.Draw();

            A.CallTo(componentMock)
                .Where(x => x.Method.Name == "Update")
                .MustHaveHappened(Repeated.Exactly.Times(IntialCallTimes + 1));
        }

        [TestCase]
        public void Draw_SetHeight_TriggerUpdateMethod()
        {
            var componentMock = CreateComponentMockWithoutInvalidation();
            componentMock.Height += 10.0f;

            componentMock.Draw();

            A.CallTo(componentMock)
                .Where(x => x.Method.Name == "Update")
                .MustHaveHappened(Repeated.Exactly.Times(IntialCallTimes + 1));
        }

        [TestCase]
        public void Draw_SetSameXPostion_NoUpdateCall()
        {
            var componentMock = CreateComponentMockWithoutInvalidation();
            var sameXPosition = componentMock.XPosition;
            componentMock.XPosition = sameXPosition;

            componentMock.Draw();

            A.CallTo(componentMock)
                .Where(x => x.Method.Name == "Update")
                .MustHaveHappened(Repeated.Exactly.Times(IntialCallTimes));
        }

        [TestCase]
        public void Draw_SetSameYPostion_NoUpdateCall()
        {
            var componentMock = CreateComponentMockWithoutInvalidation();
            var sameYPosition = componentMock.YPosition;
            componentMock.YPosition = sameYPosition;

            componentMock.Draw();

            A.CallTo(componentMock)
                .Where(x => x.Method.Name == "Update")
                .MustHaveHappened(Repeated.Exactly.Times(IntialCallTimes));
        }

        [TestCase]
        public void Draw_SetSameWidth_NoUpdateCall()
        {
            var componentMock = CreateComponentMockWithoutInvalidation();
            var sameWidth = componentMock.Width;
            componentMock.Width = sameWidth;

            componentMock.Draw();

            A.CallTo(componentMock)
                .Where(x => x.Method.Name == "Update")
                .MustHaveHappened(Repeated.Exactly.Times(IntialCallTimes));
        }

        [TestCase]
        public void Draw_SetSameHeight_NoUpdateCall()
        {
            var componentMock = CreateComponentMockWithoutInvalidation();
            var sameHeight = componentMock.Height;
            componentMock.Height = sameHeight;

            componentMock.Draw();

            A.CallTo(componentMock)
                .Where(x => x.Method.Name == "Update")
                .MustHaveHappened(Repeated.Exactly.Times(IntialCallTimes));
        }

        [TestCase]
        public void Draw_Call_DrawComponentCalled()
        {
            var componentMock = CreateComponentMock();

            componentMock.Draw();

            A.CallTo(componentMock)
                .Where(x => x.Method.Name == "DrawComponent")
                .MustHaveHappened(Repeated.AtLeast.Once);
        }


        protected override IGraphicComponent CreateComponent()
        {
            return CreateComponentMock();
        }
    }
}
