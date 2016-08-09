using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine.Graphics
{
    public class SingleComponentLayout : AbstractLayout
    {
        protected override void UpdateComponents(Container container)
        {
            var components = container.Components;

            if (components.Count == 0)
                return;

            var component = components.First();
            component.XPosition = XPosition;
            component.YPosition = YPosition;

            switch (component.VerticalPolicy)
            {
                case ResizePolicy.Extending:
                    component.Height = Height;
                    break;
                case ResizePolicy.Preferred:
                    component.Height = Math.Min(Height, component.PreferredHeight);
                    break;
            }

            switch (component.HorizontalPolicy)
            {
                case ResizePolicy.Extending:
                    component.Width = Width;
                    break;
                case ResizePolicy.Preferred:
                    component.Width = Math.Min(Width, component.PreferredWidth);
                    break;
            }

            SetRemainingComponentsSizeZero(components);
        }

        private static void SetRemainingComponentsSizeZero(IReadOnlyList<IGraphicComponent> components)
        {
            for (int i = 1; i < components.Count; i++)
                components[i].SetCoordinates(0, 0, 0, 0);
        }
    }
}