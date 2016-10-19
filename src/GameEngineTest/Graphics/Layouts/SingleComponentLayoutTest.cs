using System.Linq;
using GameEngine.Graphics;
using GameEngine.Graphics.Layouts;
using GameEngine.Utils;
using GameEngineTest.TestUtils;
using NUnit.Framework;

namespace GameEngineTest.Graphics.Layouts
{
    [TestFixture]
    public class SingleComponentLayoutTest : ILayoutTest
    {
        [TestCase]
        public void LayoutContainer_ContainerWithMultipleComponents_FirstIsLayouted()
        {
            var layout = CreateLayout();
            var testContainer = CreateContainer(10.0f, 10.0f, 50.0f, 50.0f);
            var components = testContainer.SetupContainer(10);

            layout.LayoutContainer(testContainer);

            var firstComponent = components.First();
            firstComponent.IsInConstraints(testContainer);
        }

        [TestCase]
        public void LayoutContainer_ContainerWithMultipleComponents_OtherComponentsAreSizedZero()
        {
            var layout = CreateLayout();
            var testContainer = CreateContainer(10.0f, 10.0f, 50.0f, 50.0f);
            var components = testContainer.SetupContainer(10);

            layout.LayoutContainer(testContainer);

            for (int i = 1; i < components.Count; i++)
                Assert.IsTrue(components[i].Width.AlmostEqual(0) || components[i].Height.AlmostEqual(0));
        }
        protected override ILayout CreateLayout()
        {
            return new SingleComponentLayout();
        }
    }
}
