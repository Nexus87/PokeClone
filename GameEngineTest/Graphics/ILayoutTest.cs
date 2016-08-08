using GameEngine;
using GameEngine.Graphics;
using GameEngineTest.TestUtils;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace GameEngineTest.Graphics
{
    public abstract class ILayoutTest
    {
        protected abstract ILayout CreateLayout();

        protected Container CreateContainer(float x = 0, float y = 0, float width = 0, float height = 0)
        {
            var container = new Container();
            container.SetCoordinates(x, y, width, height);

            return container;
        }

        public IEngineInterface gameStub = new Mock<IEngineInterface>().Object;

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
            var testContainer = CreateContainer(X, Y, Width, Height);
            var components = testContainer.SetupContainer(10);

            testLayout.LayoutContainer(testContainer);

            foreach (var obj in components)
                obj.IsInConstraints(testContainer);
        }

        [TestCaseSource(typeof(ILayoutTest), "ValidData")]
        public void LayoutContainer_SetLeftMargin_ComponentsInContainersConstraints(float X, float Y, float Width, float Height)
        {
            var testLayout = CreateLayout();
            int Margin = 10;
            var testContainer = CreateContainer(X, Y, Width, Height);
            var components = testContainer.SetupContainer(10);

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
            var testContainer = CreateContainer(X, Y, Width, Height);
            var components = testContainer.SetupContainer(10);

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
            var testContainer = CreateContainer(X, Y, Width, Height);
            var components = testContainer.SetupContainer(10);       

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
            var testContainer = CreateContainer(X, Y, Width, Height);
            var components = testContainer.SetupContainer(10);

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
            var testContainer = CreateContainer(X, Y, Width, Height);
            var components = testContainer.SetupContainer(10);

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
            var testContainer = CreateContainer(0, 0, width, height);
            var components = testContainer.SetupContainer(10);
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
            var testContainer = CreateContainer(0, 0, width, height);
            var components = testContainer.SetupContainer(10);
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
            var testContainer = CreateContainer(0, 0, width, height);
            var components = testContainer.SetupContainer(10);
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
            var testContainer = CreateContainer(0, 0, width, height);
            var components = testContainer.SetupContainer(10);
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
            var testContainer = CreateContainer(0, 0, width, height);
            var components = testContainer.SetupContainer(10);
            testLayout.SetMargin(margin, margin, margin, margin);

            testLayout.LayoutContainer(testContainer);

            foreach (var c in testContainer.Components)
                Assert.IsTrue(c.Width.CompareTo(0) == 0 || c.Height.CompareTo(0) == 0);
        }

        [TestCase]
        public void LayoutContainer_EmptyContainer_DoesNotThrow()
        {
            var testLayout = CreateLayout();
            var testContainer = new Container();

            Assert.DoesNotThrow(() => testLayout.LayoutContainer(testContainer));
        }

        [TestCase(0, 0, 100, 100, 10, 10)]
        [TestCase(10, 5, 100, 100, 10, 10)]
        [TestCase(10, 10, 100, 100, 110, 100)]
        public void LayoutContainer_OneComponentWithFixedVSize_ComponentHasPreferredHeight(
            float containerX, float containerY, 
            float containerWidth, float containerHeight, 
            float preferredHeight, float expectedHeight)
        {
            var testLayout = CreateLayout();
            var testContainer = CreateContainer(containerX, containerY, containerWidth, containerHeight);
            var component = testContainer.SetupContainer(1);

            component[0].PreferredHeight = preferredHeight;
            component[0].VerticalPolicy = ResizePolicy.Preferred;

            testLayout.LayoutContainer(testContainer);

            Assert.AreEqual(expectedHeight, component[0].Height);

        }

        [TestCase(0, 0, 100, 100, 10, 10)]
        [TestCase(10, 5, 100, 100, 10, 10)]
        [TestCase(10, 10, 100, 100, 110, 100)]
        public void LayoutContainer_OneComponentWithFixedHSize_ComponentHasPreferredWidth(
            float containerX, float containerY,
            float containerWidth, float containerHeight,
            float preferredWidth, float expectedWidth)
        {
            var testLayout = CreateLayout();
            var testContainer = CreateContainer(containerX, containerY, containerWidth, containerHeight);
            var component = testContainer.SetupContainer(1);

            component[0].PreferredWidth = preferredWidth;
            component[0].HorizontalPolicy = ResizePolicy.Preferred;

            testLayout.LayoutContainer(testContainer);

            Assert.AreEqual(expectedWidth, component[0].Width);

        }
    }
}
