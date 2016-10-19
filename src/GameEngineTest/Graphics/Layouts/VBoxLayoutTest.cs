using GameEngine.Graphics;
using GameEngine.Graphics.Layouts;
using GameEngineTest.TestUtils;
using NUnit.Framework;

namespace GameEngineTest.Graphics.Layouts
{
    [TestFixture]
    public class VBoxLayoutTest : ILayoutTest
    {
        [TestCase(4, 0, 0, 200, 200)]
        public void LayoutContainer_NormalSetup_ComponentsInRightOrder(int cnt, float x, float y, float width, float height)
        {
            var layout = new VBoxLayout();
            var container = CreateContainer(x, y, width, height);
            var components = container.SetupContainer(cnt);
            var componentHeight = height / cnt;

            layout.LayoutContainer(container);
            
            for (var i = 0; i < components.Count; i++ )
            {
                var comp = components[i];
                Assert.AreEqual(x, comp.XPosition);
                Assert.AreEqual(componentHeight * i, comp.YPosition);
                Assert.AreEqual(width, comp.Width);
                Assert.AreEqual(componentHeight, comp.Height);
            }
        }

        [TestCase(10, 20, 100, 300, 10, 100, 290)]
        public void LayoutContainer_TwoComponentsFixedAndExpandingVertical_ExpandingTakeRemainingSize(
            float x, float y, 
            float width, float height,
            float fixedHeight,
            float expectedWidth, float expectedHeight
            )
        {
            var layout = new VBoxLayout();
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

        [TestCase(10, 20, 100, 300, 10, 100, 150)]
        public void LayoutContainer_TwoComponentsFixedAndExpandingHorizontal_VerticalIsEquallyShared(
            float x, float y,
            float width, float height,
            float fixedWidth,
            float expectedWidth, float expectedHeight
            )
        {
            var layout = new VBoxLayout();
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
            Assert.AreEqual(expectedWidth, expandingComponent.Width, 10E-10);
            Assert.AreEqual(expectedHeight, expandingComponent.Height, 10E-10);
        }

        [TestCase(10, 20, 100, 200, 10, 10, 20, 10, 125)]
        public void LayoutContainer_SetSpacing_CoordinatesAsExpected(
            float x, float y, float width, float height,
            float spacing,
            float expectedX1, float expectedY1, float expectedX2, float expectedY2)
        {
            var layout = new VBoxLayout();
            var container = CreateContainer(x, y, width, height);
            var components = container.SetupContainer(2);

            layout.Spacing = spacing;

            layout.LayoutContainer(container);

            Assert.AreEqual(expectedX1, components[0].XPosition);
            Assert.AreEqual(expectedY1, components[0].YPosition);
            Assert.AreEqual(expectedX2, components[1].XPosition);
            Assert.AreEqual(expectedY2, components[1].YPosition);
        }

        [TestCase(10, 20, 100, 200, 10, 100, 95, 100, 95)]
        public void LayoutContainer_SetSpacing_SizeAsExpected(
            float x, float y, float width, float height,
            float spacing,
            float expectedWidth1, float expectedHeight1, float expectedWidth2, float expectedHeight2)
        {
            var layout = new VBoxLayout();
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
            return new VBoxLayout();
        }
    }
}
