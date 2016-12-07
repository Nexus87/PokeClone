using System.Xml.Linq;
using System.Xml.Serialization;
using GameEngine.GUI.Controlls;

namespace GameEngine.GUI.Builder.Controls
{
    [XmlRoot("Button")]
    public class IntermediateButton
    {
        private float _textHeight;

        [XmlIgnore]
        public bool HasTextHeight { get; set; }

        [XmlAttribute]
        public string Text { get; set; }

        [XmlAttribute]
        public float TextHeight
        {
            get { return _textHeight; }
            set
            {
                _textHeight = value;
                HasTextHeight = true;
            }
        }
    }

    public class ButtonBuilder : IBuilder
    {
        private readonly ISkin _skin;

        public ButtonBuilder(ISkin skin)
        {
            _skin = skin;
        }

        public IGuiComponent BuildFromNode(XElement element, object controller)
        {
            var button =  BuildButtonFromNode(element);
            WireUpController(element, button, controller);

            return button;
        }

        internal Button BuildButtonFromNode(XElement element)
        {
            var xmlSerializer = new XmlSerializer(typeof(IntermediateButton));
            var intermediateButton = (IntermediateButton) xmlSerializer.Deserialize(element.CreateReader());

            var button = new Button(_skin.BuildButtonRenderer())
            {
                Text = intermediateButton.Text ?? "",
                TextHeight = intermediateButton.HasTextHeight ? intermediateButton.TextHeight : _skin.DefaultTextHeight
            };

            return button;
        }

        public void WireUpController(XElement xmlDocumentRoot, Button button, object controller)
        {
            BuilderUtils.WireUpController(xmlDocumentRoot, button, controller);
        }
    }
}