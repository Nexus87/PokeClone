
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

        public static void SetPosition(this IGraphicComponent component, IGraphicComponent constraints)
        {
            component.SetPosition(constraints.Area.Location);
        }

        public static void SetPosition(this IGraphicComponent component, Point location)
        {
            component.Area = new Rectangle(location, component.Area.Size);
        }

        public static void SetPosition(this IGraphicComponent component, int x, int y)
        {
            component.SetPosition(new Point(x, y));
        }
    }
}
