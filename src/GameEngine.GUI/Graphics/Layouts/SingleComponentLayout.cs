using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Graphics.Layouts
{
    public class SingleComponentLayout : AbstractLayout
    {
        protected override void UpdateComponents(Container container)
        {
            var components = container.Components;

            if (components.Count == 0)
                return;

            var component = components.First();
            var location = new Point((int) XPosition, (int) YPosition);
            var height = component.Area.Height;
            var width = component.Area.Width;

            switch (component.VerticalPolicy)
            {
                case ResizePolicy.Extending:
                    height = (int) Height;
                    break;
                case ResizePolicy.Preferred:
                    height = (int) Math.Min(Height, component.PreferredHeight);
                    break;
            }

            switch (component.HorizontalPolicy)
            {
                case ResizePolicy.Extending:
                    width = (int) Width;
                    break;
                case ResizePolicy.Preferred:
                    width = (int) Math.Min(Width, component.PreferredWidth);
                    break;
            }

            component.Area = new Rectangle(location, new Point(width, height));
            SetRemainingComponentsSizeZero(components);
        }

        private static void SetRemainingComponentsSizeZero(IReadOnlyList<IGraphicComponent> components)
        {
            for (int i = 1; i < components.Count; i++)
                components[i].SetCoordinates(0, 0, 0, 0);
        }
    }
}