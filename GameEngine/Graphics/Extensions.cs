using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics
{
    public static class Extensions
    {

        public static void SetCoordinates(this IGraphicComponent component, IGraphicComponent constraints)
        {
            component.SetCoordinates(constraints.XPosition, constraints.YPosition, constraints.Width, constraints.Height);
        }

        public static void SetCoordinates(this IGraphicComponent component, float X, float Y, float width, float height)
        {
            component.XPosition = X;
            component.YPosition = Y;
            component.Width = width;
            component.Height = height;
        }
    }
}
