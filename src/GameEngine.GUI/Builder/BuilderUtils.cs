using System;
using System.Linq;
using System.Xml.Linq;

namespace GameEngine.GUI.Builder
{
    public class BuilderUtils
    {
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
    }
}