using GameEngine.Graphics;
using GameEngineTest.TestUtils;
using NUnit.Framework;

namespace GameEngineTest.Graphics
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
            float componentHeight = height / cnt;

            layout.LayoutContainer(container);
            
            for (int i = 0; i < components.Count; i++ )
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

        protected override ILayout CreateLayout()
        {
            return new VBoxLayout();
        }
    }

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

        protected override ILayout CreateLayout()
        {
            return new HBoxLayout();
        }
    }
}
