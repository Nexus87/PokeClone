using GameEngine.Graphics;
using GameEngineTest.TestUtils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using FakeItEasy;
using GameEngine.Graphics.General;

namespace GameEngineTest.Graphics
{

    public abstract class IGraphicComponentTest
    {
        public ISpriteFont FontMock;

        protected abstract IGraphicComponent CreateComponent();

        protected IGraphicComponentTest()
        {
            FontMock = A.Fake<ISpriteFont>();
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
            var sizeChangedRaised = false;
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
            var sizeChangedRaised = false;
            GraphicComponentSizeChangedEventArgs eventArgs = null;

            testComponent.SizeChanged += (obj, args) => { sizeChangedRaised = true; eventArgs = args; };

            testComponent.Width += 1.0f;

            Assert.True(sizeChangedRaised);
            Assert.NotNull(eventArgs);
            Assert.AreEqual(testComponent.Height, eventArgs.Height);
            Assert.AreEqual(testComponent.Width, eventArgs.Width);
        }

        [TestCase]
        public void Width_SetSameData_NoEventRaised()
        {
            var sizeEventRaised = false;
            var testComponent = CreateComponent();
            var currentWidth = testComponent.Width;
            testComponent.SizeChanged += delegate { sizeEventRaised = true; };

            testComponent.Width = currentWidth;

            Assert.False(sizeEventRaised);
        }

        [TestCase]
        public void Height_SetSameData_NoEventRaised()
        {
            var sizeEventRaised = false;
            var testComponent = CreateComponent();
            var currentHeight = testComponent.Height;
            testComponent.SizeChanged += delegate { sizeEventRaised = true; };

            testComponent.Height = currentHeight;

            Assert.False(sizeEventRaised);
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

        [TestCase]
        public void Visibility_ChangeVisibilityToFalse_RaisesEvent()
        {
            var testComponent = CreateComponent();
            var eventRaised = false;
            testComponent.IsVisible = true;
            testComponent.VisibilityChanged += delegate { eventRaised = true; };

            testComponent.IsVisible = false;

            Assert.True(eventRaised);

        }

        [TestCase]
        public void Visibility_ChangeVisibilityToTrue_RaisesEvent()
        {
            var testComponent = CreateComponent();
            var eventRaised = false;
            testComponent.IsVisible = false;
            testComponent.VisibilityChanged += delegate { eventRaised = true; };

            testComponent.IsVisible = true;

            Assert.True(eventRaised);

        }

        [TestCase]
        public void Visibility_SetSameVisibility_NoEventRaised()
        {
            var testComponent = CreateComponent();
            var eventRaised = false;
            testComponent.IsVisible = false;
            testComponent.VisibilityChanged += delegate { eventRaised = true; };

            testComponent.IsVisible = false;

            Assert.False(eventRaised);

        }

        [TestCase]
        public void Draw_SetVisibilityFalse_ComponentIsNotDrawn()
        {
            var testComponent = CreateComponent();
            var batch = new SpriteBatchMock();
            testComponent.IsVisible = false;

            testComponent.Draw(batch);

            Assert.AreEqual(0, batch.DrawnObjects.Count);
        }

        [TestCase]
        public void IsVisible_Default_IsTrue()
        {
            var testComponent = CreateComponent();
            Assert.True(testComponent.IsVisible);
        }
    }
}
