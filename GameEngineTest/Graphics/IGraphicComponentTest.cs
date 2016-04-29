using GameEngine;
using GameEngine.Graphics;
using GameEngineTest.TestUtils;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace GameEngineTest.Graphics
{

    public abstract class IGraphicComponentTest
    {
        public Mock<ISpriteFont> fontMock;
        public IPokeEngine gameStub = new Mock<IPokeEngine>().Object;

        protected abstract IGraphicComponent CreateComponent();

        public IGraphicComponentTest()
        {
            fontMock = new Mock<ISpriteFont>();
        }

        public static List<TestCaseData> InvalidData = new List<TestCaseData>{
            new TestCaseData(0.0f, 0.0f, -1.0f, 1.0f),
            new TestCaseData(0.0f, 0.0f, 1.0f, -1.0f),
            new TestCaseData(0.0f, 0.0f, -1.0f, -1.0f)
        };

        [TestCase(-1)]
        public void Width_SetInvalidData_ThrowsException(float value)
        {
            var testComponent = CreateComponent();
            Assert.Throws<ArgumentException>(() => testComponent.Width = value);
        }

        [TestCase(-1)]
        public void Height_SetInvalidData_ThrowsException(float value)
        {
            var testComponent = CreateComponent();
            Assert.Throws<ArgumentException>(() => testComponent.Height = value);
        }

        [TestCase]
        public void Height_SetData_RaiseSizeChangedEvent()
        {
            var testComponent = CreateComponent();
            bool sizeChangedRaised = false;
            GraphicComponentSizeChangedEventArgs eventArgs = null;

            testComponent.SizeChanged += (obj, args) => { sizeChangedRaised = true; eventArgs = args; };

            testComponent.Height += 1.0f;

            Assert.True(sizeChangedRaised);
            Assert.NotNull(eventArgs);
            Assert.AreEqual(testComponent.Height, eventArgs.Height);
            Assert.AreEqual(testComponent.Width, eventArgs.Width);
        }

        [TestCase]
        public void Width_SetData_RaiseSizeChangedEvent()
        {
            var testComponent = CreateComponent();
            bool sizeChangedRaised = false;
            GraphicComponentSizeChangedEventArgs eventArgs = null;

            testComponent.SizeChanged += (obj, args) => { sizeChangedRaised = true; eventArgs = args; };

            testComponent.Width += 1.0f;

            Assert.True(sizeChangedRaised);
            Assert.NotNull(eventArgs);
            Assert.AreEqual(testComponent.Height, eventArgs.Height);
            Assert.AreEqual(testComponent.Width, eventArgs.Width);
        }

        [TestCase]
        public void XPosition_SetData_RaisePositionChangedEvent()
        {
            var testComponent = CreateComponent();
            bool positionChangedRaised = false;
            GraphicComponentPositionChangedEventArgs eventArgs = null;
            testComponent.PositionChanged += (obj, args) => { positionChangedRaised = true; eventArgs = args; };

            testComponent.XPosition += 1.0f;

            Assert.True(positionChangedRaised);
            Assert.NotNull(eventArgs);
            Assert.AreEqual(testComponent.XPosition, eventArgs.XPosition);
            Assert.AreEqual(testComponent.YPosition, eventArgs.YPosition);
        }

        [TestCase]
        public void YPosition_SetData_RaisePositionChangedEvent()
        {
            var testComponent = CreateComponent();
            bool positionChangedRaised = false;
            GraphicComponentPositionChangedEventArgs eventArgs = null;
            testComponent.PositionChanged += (obj, args) => { positionChangedRaised = true; eventArgs = args; };

            testComponent.YPosition += 1.0f;

            Assert.True(positionChangedRaised);
            Assert.NotNull(eventArgs);
            Assert.AreEqual(testComponent.XPosition, eventArgs.XPosition);
            Assert.AreEqual(testComponent.YPosition, eventArgs.YPosition);
        }

        [TestCase]
        public void Width_SetSameData_NoEventRaised()
        {
            bool sizeEventRaised = false;
            var testComponent = CreateComponent();
            float currentWidth = testComponent.Width;
            testComponent.SizeChanged += delegate { sizeEventRaised = true; };

            testComponent.Width = currentWidth;

            Assert.False(sizeEventRaised);
        }

        [TestCase]
        public void Height_SetSameData_NoEventRaised()
        {
            bool sizeEventRaised = false;
            var testComponent = CreateComponent();
            float currentHeight = testComponent.Height;
            testComponent.SizeChanged += delegate { sizeEventRaised = true; };

            testComponent.Height = currentHeight;

            Assert.False(sizeEventRaised);
        }

        [TestCase]
        public void XPosition_SetSameData_NoEventRaised()
        {
            bool positionEventRaised = false;
            var testComponent = CreateComponent();
            float xPosition = testComponent.XPosition;
            testComponent.PositionChanged += delegate { positionEventRaised = true; };

            testComponent.XPosition = xPosition;

            Assert.False(positionEventRaised);
        }

        [TestCase]
        public void YPosition_SetSameData_NoEventRaised()
        {
            bool positionEventRaised = false;
            var testComponent = CreateComponent();
            float yPosition = testComponent.YPosition;
            testComponent.PositionChanged += delegate { positionEventRaised = true; };

            testComponent.YPosition = yPosition;

            Assert.False(positionEventRaised);
        }

        [TestCase]
        public void Height_SetProperty_Succeeds()
        {
            var testComponent = CreateComponent();
            var newHeight = testComponent.Height + 10.0f;

            testComponent.Height = newHeight;

            Assert.AreEqual(newHeight, testComponent.Height);
        }

        [TestCase]
        public void Width_SetProperty_Succeeds()
        {
            var testComponent = CreateComponent();
            var newWidth = testComponent.Width + 10.0f;

            testComponent.Width = newWidth;

            Assert.AreEqual(newWidth, testComponent.Width);
        }

        [TestCase]
        public void XPosition_SetProperty_Succeeds()
        {
            var testComponent = CreateComponent();
            var newX = testComponent.XPosition + 10.0f;

            testComponent.XPosition = newX;

            Assert.AreEqual(newX, testComponent.XPosition);
        }

        [TestCase]
        public void YPosition_SetProperty_Succeeds()
        {
            var testComponent = CreateComponent();
            var newY = testComponent.YPosition + 10.0f;

            testComponent.YPosition = newY;

            Assert.AreEqual(newY, testComponent.YPosition);
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
