﻿using GameEngine.Graphics;
using GameEngineTest.TestUtils;
using NUnit.Framework;

namespace GameEngineTest.Graphics
{
    [TestFixture]
    public class HBoxLayoutTest : ILayoutTest
    {


        [TestCase(4, 0, 0, 200, 200)]
        public void LayoutContainer_NormalSetup_ComponentsInRightOrder(int cnt, float x, float y, float width, float height)
        {
            var layout = new HBoxLayout();
            var container = CreateContainer(x, y, width, height);
            var components = container.SetupContainer(cnt);
            float componentWidth = width / cnt;

            layout.LayoutContainer(container);

            for (int i = 0; i < components.Count; i++)
            {
                var comp = components[i];
                Assert.AreEqual(componentWidth * i, comp.XPosition);
                Assert.AreEqual(y, comp.YPosition);
                Assert.AreEqual(componentWidth, comp.Width);
                Assert.AreEqual(height, comp.Height);
            }
        }

        [TestCase(10, 20, 100, 300, 10, 90, 300)]
        public void LayoutContainer_TwoComponentsFixedAndExpandingHorizontal_ExpandingTakeRemainingSize(
            float x, float y,
            float width, float height,
            float fixedWidth,
            float expectedWith, float expectedHeight
        )
        {
            var layout = new HBoxLayout();
            var container = CreateContainer(x, y, width, height);
            var components = container.SetupContainer(2);

            var fixedComponent = components[0];
            var expandingComponent = components[1];

            fixedComponent.HorizontalPolicy = ResizePolicy.Preferred;
            expandingComponent.HorizontalPolicy = ResizePolicy.Extending;

            fixedComponent.PreferredWidth = fixedWidth;

            layout.LayoutContainer(container);

            Assert.AreEqual(fixedWidth, fixedComponent.Width, 10E-10);
            Assert.AreEqual(expectedHeight, fixedComponent.Height, 10E-10);
            Assert.AreEqual(expectedWith, expandingComponent.Width, 10E-10);
            Assert.AreEqual(expectedHeight, expandingComponent.Height, 10E-10);
        }

        [TestCase(10, 20, 100, 300, 10, 50, 300)]
        public void LayoutContainer_TwoComponentsFixedAndExpandingVertical_HorizontalIsEquallyShared(
            float x, float y,
            float width, float height,
            float fixedHeight,
            float expectedWidth, float expectedHeight
        )
        {
            var layout = new HBoxLayout();
            var container = CreateContainer(x, y, width, height);
            var components = container.SetupContainer(2);

            var fixedComponent = components[0];
            var expandingComponent = components[1];

            fixedComponent.VerticalPolicy = ResizePolicy.Preferred;
            expandingComponent.VerticalPolicy = ResizePolicy.Extending;

            fixedComponent.PreferredHeight = fixedHeight;

            layout.LayoutContainer(container);

            Assert.AreEqual(expectedWidth, fixedComponent.Width, 10E-10);
            Assert.AreEqual(fixedHeight, fixedComponent.Height, 10E-10);
            Assert.AreEqual(expectedWidth, expandingComponent.Width, 10E-10);
            Assert.AreEqual(expectedHeight, expandingComponent.Height, 10E-10);
        }

        [TestCase(10, 20, 100, 200, 10, 10, 20, 65, 20)]
        public void LayoutContainer_SetSpacing_CoordinatesAsExpected(
            float x, float y, float width, float height,
            float spacing,
            float expectedX1, float expectedY1, float expectedX2, float expectedY2)
        {
            var layout = new HBoxLayout();
            var container = CreateContainer(x, y, width, height);
            var components = container.SetupContainer(2);

            layout.Spacing = spacing;

            layout.LayoutContainer(container);

            Assert.AreEqual(expectedX1, components[0].XPosition);
            Assert.AreEqual(expectedY1, components[0].YPosition);
            Assert.AreEqual(expectedX2, components[1].XPosition);
            Assert.AreEqual(expectedY2, components[1].YPosition);
        }

        [TestCase(10, 20, 100, 200, 10, 45, 200, 45, 200)]
        public void LayoutContainer_SetSpacing_SizeAsExpected(
            float x, float y, float width, float height,
            float spacing,
            float expectedWidth1, float expectedHeight1, float expectedWidth2, float expectedHeight2)
        {
            var layout = new HBoxLayout();
            var container = CreateContainer(x, y, width, height);
            var components = container.SetupContainer(2);

            layout.Spacing = spacing;

            layout.LayoutContainer(container);

            Assert.AreEqual(expectedWidth1, components[0].Width);
            Assert.AreEqual(expectedHeight1, components[0].Height);
            Assert.AreEqual(expectedWidth2, components[1].Width);
            Assert.AreEqual(expectedHeight2, components[1].Height);
        }

        protected override ILayout CreateLayout()
        {
            return new HBoxLayout();
        }
    }
}