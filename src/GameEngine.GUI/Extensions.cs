using Microsoft.Xna.Framework;

namespace GameEngine.GUI
{
    public static class Extensions
    {

        public static void SetCoordinates(this IGuiComponent component, IGuiComponent constraints)
        {
            component.Area = constraints.Area;
        }

        public static void SetCoordinates(this IGuiComponent component, float x, float y, float width, float height)
        {
            var area = new Rectangle((int) x, (int) y, (int) width, (int) height);
            component.Area = area;
        }

        public static void SetPosition(this IGuiComponent component, IGuiComponent constraints)
        {
            component.SetPosition(constraints.Area.Location);
        }

        public static void SetPosition(this IGuiComponent component, Point location)
        {
            component.Area = new Rectangle(location, component.Area.Size);
        }

        public static void SetPosition(this IGuiComponent component, int x, int y)
        {
            component.SetPosition(new Point(x, y));
        }

        public static void SetSize(this IGuiComponent component, int width, int height)
        {
            component.Area = new Rectangle(component.Area.X, component.Area.Y, width, height);
        }
    }
}
