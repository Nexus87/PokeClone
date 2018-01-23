using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Autofac;
using GameEngine.Globals;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Loader
{
    public abstract class AbstractGuiComponentBuilder : IBuilder
    {
        public abstract IGuiComponent Build(IContainer container, GuiLoader loader, ScreenConstants screenConstants, XElement xElement, object controller);

        protected void SetUpController(object controller, IGuiComponent component, XElement xElement)
        {
            if (controller == null)
                return;

            var componentId = xElement.Attribute("id")?.Value;
            if (componentId == null)
                return;

            var field = controller
                .GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => Attribute.IsDefined(x, typeof(GuiLoaderIdAttribute)))
                .SingleOrDefault(x => x.GetCustomAttribute<GuiLoaderIdAttribute>().Name == componentId);

            if (field != null)
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

        protected void MapElementsToProperties(XElement xElement, IGuiComponent component)
        {
            var reservedProps = new HashSet<string>(new[] { "X", "Y", "Width", "Height", "id" });
            xElement.Attributes()
                .Where(x => !reservedProps.Contains(x.Name.LocalName))
                .ToList()
                .ForEach(x =>
                {
                    var prop = component.GetType().GetProperty(x.Name.LocalName);
                    if (prop == null)
                        return;

                    prop.SetValue(component, ConvertToType(prop.PropertyType, x.Value));
                });
        }
        protected void MapEventsToFunctions(XElement xElement, IGuiComponent component, object controller)
        {
            var attributes = xElement.Attributes().ToList();
            var events = component
                .GetType()
                .GetEvents()
                .Select(x => (x, attributes.FirstOrDefault(y => y.Name.LocalName == x.Name)))
                .Where(x => x.Item2 != null);

            foreach (var (e, a) in events)
            {
                var method = controller.GetType().GetMethod(a.Value);
                if (method == null)
                    continue;
                try
                {
                    var d = Delegate.CreateDelegate(e.EventHandlerType, controller, method);
                    e.AddEventHandler(component, d);
                }
                catch (Exception ex)
                {
                    System.Console.Error.WriteLine(ex.Message);
                    System.Console.Error.WriteLine(ex.StackTrace);
                }


            }
        }
        private object ConvertToType(Type propertyType, string value)
        {
            if (propertyType == typeof(string))
            {
                return value;
            }
            if (propertyType == typeof(int))
            {
                int.TryParse(value, out var result);
                return result;
            }
            if (propertyType == typeof(float))
            {
                float.TryParse(value, out var result);
                return result;
            }

            return null;
        }

        private static int AttributeToInt(XElement xElement, string attributeName, float referenceSize)
        {
            var attribute = xElement.Attribute(attributeName);
            if (attribute == null)
                return 0;

            if (attribute.Value.EndsWith("*"))
            {
                var scaling = float.Parse(attribute.Value.Replace("*", string.Empty).Trim(), CultureInfo.InvariantCulture);
                return (int)(scaling * referenceSize);
            }
            return int.Parse(attribute.Value);
        }
    }
}