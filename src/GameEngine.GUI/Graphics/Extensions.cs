
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Graphics
{
    public static class Extensions
    {

        public static void SetCoordinates(this IGraphicComponent component, IGraphicComponent constraints)
        {
            component.Area = constraints.Area;
        }

        public static void SetCoordinates(this IGraphicComponent component, float x, float y, float width, float height)
        {
            var area = new Rectangle((int) x, (int) y, (int) width, (int) height);
            component.Area = area;
        }
    }
}
