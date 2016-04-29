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

        protected override ILayout CreateLayout()
        {
            return new HBoxLayout();
        }
    }
}
