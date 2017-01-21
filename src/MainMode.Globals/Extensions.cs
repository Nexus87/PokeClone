using GameEngine.Globals;
using Microsoft.Xna.Framework;

namespace MainMode.Globals
{
    public static class Extensions
    {
        public static void Set<T>(this Table<T> table, Point p, T value)
        {
            table[p.Y, p.X] = value;
        }

        public static T Get<T>(this Table<T> table, Point p)
        {
            return table[p.Y, p.X];
        }
    }
}