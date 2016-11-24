using System;
using System.Linq;
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

        public IGraphicComponent BuildFromNode(XElement element, object controller = null)
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

        public static void WireUpController<TComponent>(XElement xmlDocumentRoot, TComponent component, object controller)
        {
            if(controller == null)
                return;

            var events = typeof(TComponent).GetEvents();
            var attributes = events
                .Where(e => xmlDocumentRoot.Attribute(e.Name) != null)
                .Select(e => new {Event = e, Value = xmlDocumentRoot.Attribute(e.Name).Value});

            foreach (var attribute in attributes)
            {
                var d = Delegate.CreateDelegate(attribute.Event.EventHandlerType, controller, attribute.Value);
                attribute.Event.AddEventHandler(component, d);
            }

        }
        public void WireUpController(XElement xmlDocumentRoot, Button button, object controller)
        {
            WireUpController<Button>(xmlDocumentRoot, button, controller);
//
//
//            var xAttribute = xmlDocumentRoot.Attribute(nameof(Button.ButtonPressed));
//            if (xAttribute == null)
//                return;
//
//            var methodname = xAttribute.Value;
//
//            var eventInfo = button.GetType().GetEvent(nameof(button.ButtonPressed));
//            var handler = Delegate.CreateDelegate(eventInfo.EventHandlerType, controller, methodname);
//            eventInfo.AddEventHandler(button, handler);
        }
    }
}