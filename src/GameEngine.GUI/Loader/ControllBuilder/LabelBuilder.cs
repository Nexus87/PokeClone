using System.Xml.Linq;
using GameEngine.Core;
using GameEngine.GUI.Controlls;
using GameEngine.TypeRegistry;

namespace GameEngine.GUI.Loader.ControllBuilder
{
    public class LabelBuilder : GuiComponentBuilder
    {
        private readonly IGameTypeRegistry _registry;

        public LabelBuilder(IGameTypeRegistry registry, ScreenConstants screenConstants) : base(screenConstants)
        {
            _registry = registry;
        }

        public override IGuiComponent Build(XElement xElement, object controller)
        {
            var label = _registry.ResolveType<Label>();
            label.Area = ReadPosition(xElement);
            SetUpController(controller, label, xElement);

            var text = xElement.Attribute(nameof(Label.Text));
            var textSize = xElement.Attribute(nameof(Label.TextHeight));
            if (text != null)
                label.Text = text.Value;
            if (textSize != null)
                label.TextHeight = int.Parse(textSize.Value);

            return label;
        }
    }
}