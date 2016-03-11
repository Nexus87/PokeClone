using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Graphics
{
    public static class Extensions
    {
        public static void SetCoordinates(this IGraphicComponent component, float X, float Y, float Width, float Height)
        {
            component.XPosition = X;
            component.YPosition = Y;
            component.Width = Width;
            component.Height = Height;
        }
    }
}
