using GameEngine;
using GameEngine.Graphics;
using GameEngine.Graphics.Basic;
using GameEngine.Graphics.Layouts;
using GameEngine.Wrapper;
using GameEngineTest.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Graphics.Layouts
{
    public abstract class ILayoutTest
    {
        protected abstract ILayout CreateLayout();

        public Mock<PokeEngine> engineMock = new Mock<PokeEngine>();

        public static List<TestCaseData> ValidData = new List<TestCaseData>
        {
            new TestCaseData(1.0f, 1.0f, 50.0f, 50.0f),
            new TestCaseData(0.0f, 0.0f, 50.0f, 50.0f),
            new TestCaseData(0.0f, 0.0f, 0.0f, 50.0f),
            new TestCaseData(0.0f, 0.0f, 50.0f, 0.0f),
            new TestCaseData(0.0f, 0.0f, 0.0f, 0.0f),
            new TestCaseData(0.0f, 0.0f, 150.0f, 50.0f),
            new TestCaseData(0.0f, 0.0f, 50.0f, 150.0f)
        };

        [TestCaseSource(typeof(ILayoutTest), "ValidData")]
        public void LayoutContainer_SetupContainer_ComponentsAreInContainersContraints(float X, float Y, float Width, float Height)
        {
            var testLayout = CreateLayout();
            var testContainer = new Container(engineMock.Object);
            var components = testContainer.SetupContainer(10);
            testContainer.SetCoordinates(X, Y, Width, Height);

            testLayout.LayoutContainer(testContainer);

            foreach (var obj in components)
                obj.IsInConstraints(testContainer);
        }

        [TestCaseSource(typeof(ILayoutTest), "ValidData")]
        public void LayoutContainer_SetLeftMargin_ComponentsInContainersConstraints(float X, float Y, float Width, float Height)
        {
            var testLayout = CreateLayout();
            int Margin = 10;
            var testContainer = new Container(engineMock.Object);
            var components = testContainer.SetupContainer(10);
            testContainer.SetCoordinates(X, Y, Width, Height);

            testLayout.SetMargin(left: Margin);
            testLayout.LayoutContainer(testContainer);
            foreach (var obj in components)
                obj.IsInConstraints(X + Margin, Y, Width - Margin, Height);
        }

        [TestCaseSource(typeof(ILayoutTest), "ValidData")]
        public void LayoutContainer_SetRightMargin_ComponentsInContainersConstraints(float X, float Y, float Width, float Height)
        {
            var testLayout = CreateLayout();
            int Margin = 10;
            var testContainer = new Container(engineMock.Object);
            var components = testContainer.SetupContainer(10);
            testContainer.SetCoordinates(X, Y, Width, Height);

            testLayout.SetMargin(right: Margin);
            testLayout.LayoutContainer(testContainer);
            foreach (var obj in components)
                obj.IsInConstraints(X, Y, Width - Margin, Height);
        }

        [TestCaseSource(typeof(ILayoutTest), "ValidData")]
        public void LayoutContainer_SetTopMargin_ComponentsInContainersConstraints(float X, float Y, float Width, float Height)
        {
            var testLayout = CreateLayout();
            int Margin = 10;
            var testContainer = new Container(engineMock.Object);
            var components = testContainer.SetupContainer(10);
            testContainer.SetCoordinates(X, Y, Width, Height);

            testLayout.SetMargin(top: Margin);
            testLayout.LayoutContainer(testContainer);

            foreach (var obj in components)
                obj.IsInConstraints(X, Y + Margin, Width, Height - Margin);
        }
        
        [TestCaseSource(typeof(ILayoutTest), "ValidData")]
        public void LayoutContainer_SetBottomMargin_ComponentsInContainersConstraints(float X, float Y, float Width, float Height)
        {
            var testLayout = CreateLayout();
            int Margin = 10;
            var testContainer = new Container(engineMock.Object);
            var components = testContainer.SetupContainer(10);
            testContainer.SetCoordinates(X, Y, Width, Height);

            testLayout.SetMargin(bottom: Margin);
            testLayout.LayoutContainer(testContainer);

            foreach (var obj in components)
                obj.IsInConstraints(X, Y, Width, Height - Margin);
        }

        [TestCaseSource(typeof(ILayoutTest), "ValidData")]
        public void LayoutContainer_SetAllMargins_ComponentsInContainersConstraints(float X, float Y, float Width, float Height)
        {
            var testLayout = CreateLayout();
            int Margin = 10;
            var testContainer = new Container(engineMock.Object);
            var components = testContainer.SetupContainer(10);
            testContainer.SetCoordinates(X, Y, Width, Height);

            testLayout.SetMargin(Margin, Margin, Margin, Margin); 
            testLayout.LayoutContainer(testContainer);

            foreach (var obj in components)
                obj.IsInConstraints(X + Margin, Y + Margin, Width - 2*Margin, Height - 2*Margin);
        }

        [TestCase]
        public void LayoutContainer_NullAsArgument_ThrowsArgumentNullException()
        {
            var testLayout = CreateLayout();
            Assert.Throws(typeof(ArgumentNullException), delegate { testLayout.LayoutContainer(null); });
        }


        [TestCase(10.0f, 10.0f, 10)]
        [TestCase(10.0f, 10.0f, 11)]
        public void LayoutContainer_SetLeftMarginTooLarge_ComponentsHaveSizeZero(float width, float height, int margin)
        {
            var testLayout = CreateLayout();
            var testContainer = new Container(engineMock.Object);
            var components = testContainer.SetupContainer(10, 30.0f);
            testContainer.SetCoordinates(0, 0, width, height);
            testLayout.SetMargin(left: margin);

            testLayout.LayoutContainer(testContainer);

            foreach (var c in testContainer.Components)
                Assert.IsTrue(c.Width.CompareTo(0) == 0 || c.Height.CompareTo(0) == 0);
        }

        [TestCase(10.0f, 10.0f, 10)]
        [TestCase(10.0f, 10.0f, 11)]
        public void LayoutContainer_SetRightMarginTooLarge_ComponentsHaveSizeZero(float width, float height, int margin)
        {
            var testLayout = CreateLayout();
            var testContainer = new Container(engineMock.Object);
            var components = testContainer.SetupContainer(10, 30.0f);
            testContainer.SetCoordinates(0, 0, width, height);
            testLayout.SetMargin(right: margin);

            testLayout.LayoutContainer(testContainer);

            foreach (var c in testContainer.Components)
                Assert.IsTrue(c.Width.CompareTo(0) == 0 || c.Height.CompareTo(0) == 0);
        }

        [TestCase(10.0f, 10.0f, 10)]
        [TestCase(10.0f, 10.0f, 11)]
        public void LayoutContainer_SetTopMarginTooLarge_ComponentsHaveSizeZero(float width, float height, int margin)
        {
            var testLayout = CreateLayout();
            var testContainer = new Container(engineMock.Object);
            var components = testContainer.SetupContainer(10, 30.0f);
            testContainer.SetCoordinates(0, 0, width, height);
            testLayout.SetMargin(top: margin);

            testLayout.LayoutContainer(testContainer);

            foreach (var c in testContainer.Components)
                Assert.IsTrue(c.Width.CompareTo(0) == 0 || c.Height.CompareTo(0) == 0);
        }

        [TestCase(10.0f, 10.0f, 10)]
        [TestCase(10.0f, 10.0f, 11)]
        public void LayoutContainer_SetBottomMarginTooLarge_ComponentsHaveSizeZero(float width, float height, int margin)
        {
            var testLayout = CreateLayout();
            var testContainer = new Container(engineMock.Object);
            var components = testContainer.SetupContainer(10, 30.0f);
            testContainer.SetCoordinates(0, 0, width, height);
            testLayout.SetMargin(bottom: margin);

            testLayout.LayoutContainer(testContainer);

            foreach (var c in testContainer.Components)
                Assert.IsTrue(c.Width.CompareTo(0) == 0 || c.Height.CompareTo(0) == 0);
        }

        [TestCase(10.0f, 10.0f, 10)]
        [TestCase(10.0f, 10.0f, 11)]
        public void LayoutContainer_SetAllMarginsTooLarge_ComponentsHaveSizeZero(float width, float height, int margin)
        {
            var testLayout = CreateLayout();
            var testContainer = new Container(engineMock.Object);
            var components = testContainer.SetupContainer(10, 30.0f);
            testContainer.SetCoordinates(0, 0, width, height);
            testLayout.SetMargin(margin, margin, margin, margin);

            testLayout.LayoutContainer(testContainer);

            foreach (var c in testContainer.Components)
                Assert.IsTrue(c.Width.CompareTo(0) == 0 || c.Height.CompareTo(0) == 0);
        }

        [TestCase]
        public void LayoutContainer_EmptyContainer_DoesNotThrow()
        {
            var testLayout = CreateLayout();
            var testContainer = new Container(engineMock.Object);

            Assert.DoesNotThrow(() => testLayout.LayoutContainer(testContainer));
        }
    }
}
