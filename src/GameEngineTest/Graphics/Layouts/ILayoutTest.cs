using System;
using System.Collections.Generic;
using GameEngine.Graphics;
using GameEngine.Graphics.Layouts;
using GameEngineTest.TestUtils;
using NUnit.Framework;

namespace GameEngineTest.Graphics.Layouts
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
        public void LayoutContainer_SetupContainer_ComponentsAreInContainersContraints(float x, float y, float width, float height)
        {
            var testLayout = CreateLayout();
            var testContainer = CreateContainer(x, y, width, height);
            var components = testContainer.SetupContainer(10);

            testLayout.LayoutContainer(testContainer);

            foreach (var obj in components)
                obj.IsInConstraints(testContainer);
        }

        [TestCaseSource(typeof(ILayoutTest), "ValidData")]
        public void LayoutContainer_SetLeftMargin_ComponentsInContainersConstraints(float x, float y, float width, float height)
        {
            var testLayout = CreateLayout();
            int margin = 10;
            var testContainer = CreateContainer(x, y, width, height);
            var components = testContainer.SetupContainer(10);

            testLayout.SetMargin(left: margin);
            testLayout.LayoutContainer(testContainer);
            foreach (var obj in components)
                obj.IsInConstraints(x + margin, y, width - margin, height);
        }

        [TestCaseSource(typeof(ILayoutTest), "ValidData")]
        public void LayoutContainer_SetRightMargin_ComponentsInContainersConstraints(float x, float y, float width, float height)
        {
            var testLayout = CreateLayout();
            int margin = 10;
            var testContainer = CreateContainer(x, y, width, height);
            var components = testContainer.SetupContainer(10);

            testLayout.SetMargin(right: margin);
            testLayout.LayoutContainer(testContainer);
            foreach (var obj in components)
                obj.IsInConstraints(x, y, width - margin, height);
        }

        [TestCaseSource(typeof(ILayoutTest), "ValidData")]
        public void LayoutContainer_SetTopMargin_ComponentsInContainersConstraints(float x, float y, float width, float height)
        {
            var testLayout = CreateLayout();
            int margin = 10;
            var testContainer = CreateContainer(x, y, width, height);
            var components = testContainer.SetupContainer(10);       

            testLayout.SetMargin(top: margin);
            testLayout.LayoutContainer(testContainer);

            foreach (var obj in components)
                obj.IsInConstraints(x, y + margin, width, height - margin);
        }
        
        [TestCaseSource(typeof(ILayoutTest), "ValidData")]
        public void LayoutContainer_SetBottomMargin_ComponentsInContainersConstraints(float x, float y, float width, float height)
        {
            var testLayout = CreateLayout();
            int margin = 10;
            var testContainer = CreateContainer(x, y, width, height);
            var components = testContainer.SetupContainer(10);

            testLayout.SetMargin(bottom: margin);
            testLayout.LayoutContainer(testContainer);

            foreach (var obj in components)
                obj.IsInConstraints(x, y, width, height - margin);
        }

        [TestCaseSource(typeof(ILayoutTest), "ValidData")]
        public void LayoutContainer_SetAllMargins_ComponentsInContainersConstraints(float x, float y, float width, float height)
        {
            var testLayout = CreateLayout();
            int margin = 10;
            var testContainer = CreateContainer(x, y, width, height);
            var components = testContainer.SetupContainer(10);

            testLayout.SetMargin(margin, margin, margin, margin);
            testLayout.LayoutContainer(testContainer);

            foreach (var obj in components)
                obj.IsInConstraints(x + margin, y + margin, width - 2*margin, height - 2*margin);
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
            testContainer.SetupContainer(10);
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
            testContainer.SetupContainer(10);
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
            testContainer.SetupContainer(10);
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
            testContainer.SetupContainer(10);
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
            testContainer.SetupContainer(10);
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
        [TestCase(10, 10, 100, 100, 110, 110)]
        public void LayoutContainer_OneComponentWithFixedVSize_ComponentHasGivenHeight(
            float containerX, float containerY, 
            float containerWidth, float containerHeight, 
            float componentHeight, float expectedHeight)
        {
            var testLayout = CreateLayout();
            var testContainer = CreateContainer(containerX, containerY, containerWidth, containerHeight);
            var component = testContainer.SetupContainer(1);

            component[0].Height = componentHeight;
            component[0].VerticalPolicy = ResizePolicy.Fixed;

            testLayout.LayoutContainer(testContainer);

            Assert.AreEqual(expectedHeight, component[0].Height);

        }

        [TestCase(0, 0, 100, 100, 10, 10)]
        [TestCase(10, 5, 100, 100, 10, 10)]
        [TestCase(10, 10, 100, 100, 110, 110)]
        public void LayoutContainer_OneComponentWithFixedHSize_ComponentGivenWidth(
            float containerX, float containerY,
            float containerWidth, float containerHeight,
            float componentWidth, float expectedWidth)
        {
            var testLayout = CreateLayout();
            var testContainer = CreateContainer(containerX, containerY, containerWidth, containerHeight);
            var component = testContainer.SetupContainer(1);

            component[0].Width = componentWidth;
            component[0].HorizontalPolicy = ResizePolicy.Fixed;

            testLayout.LayoutContainer(testContainer);

            Assert.AreEqual(expectedWidth, component[0].Width);

        }

        [TestCase(0, 0, 100, 100, 10, 10)]
        [TestCase(10, 5, 100, 100, 10, 10)]
        [TestCase(10, 10, 100, 100, 110, 100)]
        public void LayoutContainer_OneComponentWithPreferredVSize_ComponentHasPreferredHeight(
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
        public void LayoutContainer_OneComponentWithPreferredHSize_ComponentHasPreferredWidth(
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
