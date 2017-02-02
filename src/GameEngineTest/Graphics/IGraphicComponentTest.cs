using System.Collections.Generic;
using FakeItEasy;
using GameEngine.Graphics.Textures;
using GameEngine.GUI;
using GameEngineTest.TestUtils;
using NUnit.Framework;

namespace GameEngineTest.Graphics
{

    public abstract class IGraphicComponentTest
    {
        public ISpriteFont FontMock;

        protected abstract IGuiComponent CreateComponent();

        protected IGraphicComponentTest()
        {
            FontMock = A.Fake<ISpriteFont>();
        }

        public static List<TestCaseData> InvalidData = new List<TestCaseData>{
            new TestCaseData(0.0f, 0.0f, -1.0f, 1.0f),
            new TestCaseData(0.0f, 0.0f, 1.0f, -1.0f),
            new TestCaseData(0.0f, 0.0f, -1.0f, -1.0f)
        };

        [TestCase]
        public void Height_SetProperty_Succeeds()
        {
            var testComponent = CreateComponent();
            var newHeight = testComponent.Height() + 10.0f;

            testComponent.Height(newHeight);

            Assert.AreEqual(newHeight, testComponent.Height());
        }

        [TestCase]
        public void Width_SetProperty_Succeeds()
        {
            var testComponent = CreateComponent();
            var newWidth = testComponent.Width() + 10.0f;

            testComponent.Width(newWidth);

            Assert.AreEqual(newWidth, testComponent.Width());
        }

        [TestCase]
        public void XPosition_SetProperty_Succeeds()
        {
            var testComponent = CreateComponent();
            var newX = testComponent.XPosition() + 10.0f;

            testComponent.XPosition(newX);

            Assert.AreEqual(newX, testComponent.XPosition());
        }

        [TestCase]
        public void YPosition_SetProperty_Succeeds()
        {
            var testComponent = CreateComponent();
            var newY = testComponent.YPosition() + 10.0f;

            testComponent.YPosition(newY);

            Assert.AreEqual(newY, testComponent.YPosition());
        }

        [TestCase( 5.0f, 5.0f, 150.0f,  10.0f )]
        [TestCase( 0.0f, 0.0f, 150.0f,  10.0f )]
        [TestCase( 0.0f, 0.0f,   0.0f,  10.0f )]
        [TestCase( 0.0f, 0.0f, 150.0f,   0.0f )]
        [TestCase( 0.0f, 0.0f,  50.0f, 150.0f )]
        public void Draw_WithValidData_DrawnObjectAreInConstraints(float x, float y, float width, float height)
        {
            var testComponent = CreateComponent();
            var batch = new SpriteBatchMock();
            testComponent.SetCoordinates(x, y, width, height);

            testComponent.Draw(batch);

            foreach (var obj in batch.DrawnObjects)
                obj.IsInConstraints(testComponent);
        }

    }
}
