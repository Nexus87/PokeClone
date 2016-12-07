using System.Xml.Linq;
using GameEngine.GUI.Builder;
using GameEngine.GUI.ComponentRegistry;
using NUnit.Framework;

namespace GameEngine.GUI.Test.ComponentRegistry
{
    [TestFixture]
    public class GuiComponentRegistryTest
    {
        public class DummyComponent : AbstractGraphicComponent
        {
            public override void HandleKeyInput(CommandKeys key)
            {
                throw new System.NotImplementedException();
            }
        }

        public class DummyComponentBuilder : IBuilder
        {
            public IGuiComponent BuildFromNode(XElement element, object controller)
            {
                throw new System.NotImplementedException();
            }
        }

        [Test]
        public void GetBuilder_AfterRegistering_ReturnsExpectedType()
        {
            var registry = new GuiComponentRegistry();
            registry.RegisterGuiComponent<DummyComponent, DummyComponentBuilder>();
            registry.Init();

            var builder = registry.GetBuilder(nameof(DummyComponent));

            Assert.IsInstanceOf<DummyComponentBuilder>(builder);
        }
    }
}