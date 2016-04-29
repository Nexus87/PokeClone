using GameEngine.Graphics;
using GameEngineTest.TestUtils;
using Microsoft.Xna.Framework;
using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace GameEngineTest.Graphics
{
    [TestFixture]
    public class AbstractGraphicComponentTest : IGraphicComponentTest
    {
        private Mock<AbstractGraphicComponent> CreateComponentMock()
        {
            var componentMock = new Mock<AbstractGraphicComponent>(gameStub);
            componentMock.CallBase = true;

            return componentMock;
        }

        private Mock<AbstractGraphicComponent> CreateComponentWithoutInvalidation()
        {
            var mock = CreateComponentMock();
            ResetInvalidation(mock);
            return mock;

        }
        private void ResetInvalidation(Mock<AbstractGraphicComponent> componentMock)
        {
            componentMock.Object.Draw();
            componentMock.ResetCalls();
        }

        [TestCase]
        public void Draw_FirstCall_TriggerUpdateMethod()
        {
            var componentMock = CreateComponentMock();

            componentMock.Object.Draw();

            componentMock.Protected().Verify("Update", Times.Once());
        }

        [TestCase]
        public void Draw_SetXPosition_TriggerUpdateMethod()
        {
            var componentMock = CreateComponentWithoutInvalidation();
            componentMock.Object.XPosition += 10.0f;

            componentMock.Object.Draw();

            componentMock.Protected().Verify("Update", Times.Once());
        }

        [TestCase]
        public void Draw_SetYPosition_TriggerUpdateMethod()
        {
            var componentMock = CreateComponentWithoutInvalidation();
            componentMock.Object.YPosition += 10.0f;

            componentMock.Object.Draw();

            componentMock.Protected().Verify("Update", Times.Once());
        }

        [TestCase]
        public void Draw_SetWidth_TriggerUpdateMethod()
        {
            var componentMock = CreateComponentWithoutInvalidation();
            componentMock.Object.Width += 10.0f;

            componentMock.Object.Draw();

            componentMock.Protected().Verify("Update", Times.Once());
        }

        [TestCase]
        public void Draw_SetHeight_TriggerUpdateMethod()
        {
            var componentMock = CreateComponentWithoutInvalidation();
            componentMock.Object.Height += 10.0f;

            componentMock.Object.Draw();

            componentMock.Protected().Verify("Update", Times.Once());
        }

        [TestCase]
        public void Draw_SetSameXPostion_NoUpdateCall()
        {
            var componentMock = CreateComponentWithoutInvalidation();
            componentMock.Object.XPosition = componentMock.Object.XPosition;

            componentMock.Object.Draw();

            componentMock.Protected().Verify("Update", Times.Never());
        }

        [TestCase]
        public void Draw_SetSameYPostion_NoUpdateCall()
        {
            var componentMock = CreateComponentWithoutInvalidation();
            componentMock.Object.YPosition = componentMock.Object.YPosition;

            componentMock.Object.Draw();

            componentMock.Protected().Verify("Update", Times.Never());
        }

        [TestCase]
        public void Draw_SetSameWidth_NoUpdateCall()
        {
            var componentMock = CreateComponentWithoutInvalidation();
            componentMock.Object.Width = componentMock.Object.Width;

            componentMock.Object.Draw();

            componentMock.Protected().Verify("Update", Times.Never());
        }

        [TestCase]
        public void Draw_SetSameHeight_NoUpdateCall()
        {
            var componentMock = CreateComponentWithoutInvalidation();
            componentMock.Object.Height = componentMock.Object.Height;

            componentMock.Object.Draw();

            componentMock.Protected().Verify("Update", Times.Never());
        }

        [TestCase]
        public void Draw_Call_DrawComponentCalled()
        {
            var componentMock = CreateComponentWithoutInvalidation();
            var time = new GameTime();
            var spriteBatchStub = new SpriteBatchMock();

            componentMock.Object.Draw(time, spriteBatchStub);
            componentMock.Protected().Verify("DrawComponent", Times.Once(), time, spriteBatchStub);
        }


        protected override IGraphicComponent CreateComponent()
        {
            return CreateComponentMock().Object;
        }
    }
}
