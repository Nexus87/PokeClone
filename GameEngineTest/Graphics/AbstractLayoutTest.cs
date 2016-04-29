using GameEngine;
using GameEngine.Graphics;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace GameEngineTest.Graphics
{
    class TestLayout : AbstractLayout
    {
        protected override void UpdateComponents(Container container) 
        {
        }

        public void TestProperties(float X, float Y, float Width, float Height)
        {
            // To make the test code easier, we allow negative size as input
            Width = Math.Max(0, Width);
            Height = Math.Max(0, Height);

            Assert.AreEqual(this.XPosition, X);
            Assert.AreEqual(this.YPosition, Y);
            Assert.AreEqual(this.Width, Width);
            Assert.AreEqual(this.Height, Height);
        }

    }
    [TestFixture]
    public class AbstractLayoutTest
    {
        private Mock<AbstractLayout> CreateLayoutMock()
        {
            var layoutMock = new Mock<AbstractLayout>();
            layoutMock.CallBase = true;

            return layoutMock;
        }

        public static List<TestCaseData> ValidPropertyData = new List<TestCaseData>
        {
            new TestCaseData(1.0f, 1.0f, 50.0f, 50.0f, 0),
            new TestCaseData(0.0f, 0.0f, 50.0f, 50.0f, 0),
            new TestCaseData(0.0f, 0.0f, 0.0f, 50.0f, 0),
            new TestCaseData(0.0f, 0.0f, 50.0f, 0.0f, 0),
            new TestCaseData(0.0f, 0.0f, 0.0f, 0.0f, 0),
            new TestCaseData(0.0f, 0.0f, 150.0f, 50.0f, 0),
            new TestCaseData(0.0f, 0.0f, 50.0f, 150.0f, 0),
            new TestCaseData(1.0f, 1.0f, 50.0f, 50.0f, 10),
            new TestCaseData(1.0f, 1.0f, 50.0f, 50.0f, 40),
            new TestCaseData(0.0f, 0.0f, 0.0f, 50.0f, 10),
            new TestCaseData(0.0f, 0.0f, 50.0f, 0.0f, 10),
            new TestCaseData(0.0f, 0.0f, 0.0f, 0.0f, 10),
        };

        [TestCaseSource("ValidPropertyData")]
        public void ProtectedProperties_SetLeftMargin_RespectsSetMargins(float X, float Y, float Width, float Height, int Margin)
        {
            var testObj = new TestLayout();
            var container = CreateContainer();
            container.SetCoordinates(X, Y, Width, Height);

            testObj.SetMargin(left: Margin);
            testObj.LayoutContainer(container);

            testObj.TestProperties(X + Margin, Y, Width - Margin, Height);
        }
            
        [TestCaseSource("ValidPropertyData")]
        public void ProtectedProperties_SetRightMargin_RespectsSetMargins(float X, float Y, float Width, float Height, int Margin)
        {
            var testObj = new TestLayout();
            var container = CreateContainer();
            container.SetCoordinates(X, Y, Width, Height);

            testObj.SetMargin(right: Margin);
            testObj.LayoutContainer(container);

            testObj.TestProperties(X, Y, Width - Margin, Height);
        }

        [TestCaseSource("ValidPropertyData")]
        public void ProtectedProperties_SetTopMargin_RespectsSetMargins(float X, float Y, float Width, float Height, int Margin)
        {
            var testObj = new TestLayout();
            var container = CreateContainer();
            container.SetCoordinates(X, Y, Width, Height);

            testObj.SetMargin(top: Margin);
            testObj.LayoutContainer(container);

            testObj.TestProperties(X, Y + Margin, Width, Height - Margin);
        }

        
        [TestCaseSource("ValidPropertyData")]
        public void ProtectedProperties_SetBottomMargin_RespectsSetMargins(float X, float Y, float Width, float Height, int Margin)
        {
            var testObj = new TestLayout();
            var container = CreateContainer();
            container.SetCoordinates(X, Y, Width, Height);

            testObj.SetMargin(bottom: Margin);
            testObj.LayoutContainer(container);

            testObj.TestProperties(X, Y, Width, Height - Margin);
        }

        [TestCaseSource("ValidPropertyData")]
        public void ProtectedProperties_SetAllMargins_RespectsSetMargins(float X, float Y, float Width, float Height, int Margin)
        {
            var testObj = new TestLayout();
            var container = CreateContainer();
            container.SetCoordinates(X, Y, Width, Height);

            testObj.SetMargin(Margin, Margin, Margin, Margin);
            testObj.LayoutContainer(container);

            testObj.TestProperties(X + Margin, Y + Margin, Width - 2*Margin, Height - 2*Margin);
        }
        

        [TestCase]
        public void LayoutContainer_SetCoordinates_UpdateComponentsIsCalled()
        {
            var container = CreateContainer();
            var layoutMock = CreateLayoutMock();
            var testLayout = layoutMock.Object;

            container.SetCoordinates(200, 200, 200, 200);
            testLayout.LayoutContainer(container);
            
            layoutMock.Protected().Verify("UpdateComponents", Times.AtLeastOnce(), container);
        }

        private Container CreateContainer()
        {
            return new Container();
        }
    }
}
