using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using GameEngine.Globals;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Loader
{
    public abstract class AbstractGuiComponentBuilder : IBuilder
    {
        public abstract IGuiComponent Build(ScreenConstants screenConstants, XElement xElement, object controller);

        protected void SetUpController(object controller, IGuiComponent component, XElement xElement)
        {
            if(controller == null)
                return;

            var componentId = xElement.Attribute("id")?.Value;
            if(componentId == null)
                return;

            var field = controller
                .GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => Attribute.IsDefined(x, typeof(GuiLoaderIdAttribute)))
                .SingleOrDefault(x => x.GetCustomAttribute<GuiLoaderIdAttribute>().Name == componentId);

            if(field != null)
                field.SetValue(controller, component);
        }

        protected Rectangle ReadPosition(ScreenConstants screenConstants, XElement xElement)
        {
            var x = AttributeToInt(xElement, "X", screenConstants.ScreenWidth);
            var y = AttributeToInt(xElement, "Y", screenConstants.ScreenHeight);
            var width = AttributeToInt(xElement, "Width", screenConstants.ScreenWidth);
            var height = AttributeToInt(xElement, "Height", screenConstants.ScreenHeight);

            return new Rectangle(x, y, width, height);
        }

        private static int AttributeToInt(XElement xElement, string attributeName, float referenceSize)
        {
            var attribute = xElement.Attribute(attributeName);
            if (attribute == null)
                return 0;

            if (attribute.Value.EndsWith("*"))
            {
                var scaling = float.Parse(attribute.Value.Replace("*", string.Empty).Trim(), CultureInfo.InvariantCulture);
                return (int) (scaling * referenceSize);
            }
            return int.Parse(attribute.Value);
        }
    }
}