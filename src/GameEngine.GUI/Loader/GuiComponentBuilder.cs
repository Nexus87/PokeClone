using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using GameEngine.Core;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Loader
{
    public abstract class GuiComponentBuilder : IBuilder
    {
        private readonly ScreenConstants _screenConstants;
        public abstract IGuiComponent Build(XElement xElement, object controller);

        protected GuiComponentBuilder(ScreenConstants screenConstants)
        {
            _screenConstants = screenConstants;
        }

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

        protected Rectangle ReadPosition(XElement xElement)
        {
            var x = AttributeToInt(xElement, "X", _screenConstants.ScreenWidth);
            var y = AttributeToInt(xElement, "Y", _screenConstants.ScreenHeight);
            var width = AttributeToInt(xElement, "Width", _screenConstants.ScreenWidth);
            var height = AttributeToInt(xElement, "Height", _screenConstants.ScreenHeight);

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
            else
            {
                return int.Parse(attribute.Value);
            }
        }
    }
}