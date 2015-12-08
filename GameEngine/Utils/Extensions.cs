using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Utils
{
    static class Extensions
    {
        public static void Copy<T>(this T[,] source, T[,] target)
        {
            int rows = Math.Min(source.GetLength(0), target.GetLength(0));
            int columns = Math.Min(source.GetLength(1), target.GetLength(1));
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    target[i, j] = source[i, j];
                }
            }
        }

        public static void Resize<T>(this List<T> list, int newSize)
        {
            if (list.Count == newSize)
                return;
            else if (list.Count > newSize)
            {
                list.RemoveRange(newSize, list.Count - newSize);
            }
            else
            {
                if (list.Capacity < newSize)
                    list.Capacity = newSize;

                list.AddRange(Enumerable.Repeat(default(T), newSize - list.Count));
            }
        }
    }
}
