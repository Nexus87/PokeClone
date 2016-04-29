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
            component.SetCoordinates(XPosition, YPosition, Width, Height);

            SetRemainingComponentsSizeZero(components);
        }

        private static void SetRemainingComponentsSizeZero(IReadOnlyList<IGraphicComponent> components)
        {
            for (int i = 1; i < components.Count; i++)
                components[i].SetCoordinates(0, 0, 0, 0);
        }
    }
}