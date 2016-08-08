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

            component.Width = component.HorizontalPolicy == ResizePolicy.Extending ?
                Width : Math.Min(Width, component.PreferredWidth);
            component.Height = component.VerticalPolicy == ResizePolicy.Extending ?
                Height : Math.Min(Height, component.PreferredHeight);

            SetRemainingComponentsSizeZero(components);
        }

        private static void SetRemainingComponentsSizeZero(IReadOnlyList<IGraphicComponent> components)
        {
            for (int i = 1; i < components.Count; i++)
                components[i].SetCoordinates(0, 0, 0, 0);
        }
    }
}