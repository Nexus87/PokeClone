using System.Globalization;
using System.Xml;
using System.Xml.Serialization;
using GameEngine.GUI;
using GameEngine.GUI.Builder.Controls;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.Renderers;

namespace GameEngine.GUI.Builder.Controls
{
    [XmlRoot("Button")]
    public class IntermediateButton
    {
        private float _textHeight;

        [XmlIgnore]
        public bool HasTextHeight { get; set; } = false;

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

    public class ButtonBuilder
    {
        private ISkin skin;

        public ButtonBuilder(ISkin skin)
        {
            this.skin = skin;
        }

        public Button ParseValue(XmlNode xmlNode)
        {
            var xmlSerializer = new XmlSerializer(typeof(IntermediateButton));
            var intermediateButton = (IntermediateButton) xmlSerializer.Deserialize(new XmlNodeReader(xmlNode));

            var button = new Button(skin.GetRendererForType<IButtonRenderer>())
            {
                Text = intermediateButton.Text ?? "",
                TextHeight = intermediateButton.HasTextHeight ? intermediateButton.TextHeight : skin.DefaultTextHeight
            };


            return button;
        }
    }
}