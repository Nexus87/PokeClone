using System.Xml.Linq;
using System.Xml.Serialization;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Renderers;

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

        public IGraphicComponent BuildFromNode(XElement element)
        {
            return BuildButtonFromNode(element);
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
    }
}