using GameEngine.Graphics;
using GameEngine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Utils
{
    static class Extensions
    {
        public delegate T Creator<T>();

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

        public static void Copy<T>(this T[,] source, T[,] target, Creator<T> defaultValue)
        {
            int rows = Math.Min(source.GetLength(0), target.GetLength(0));
            int columns = Math.Min(source.GetLength(1), target.GetLength(1));

            int sourceRows = source.GetLength(0);
            int sourceColumns = source.GetLength(1);
            int targetRows = target.GetLength(0);
            int targetColumns = target.GetLength(1);

            for (int row = 0; row < targetRows; row++)
            {
                for (int column = 0; column < targetColumns; column++)
                {
                    target[row, column] = row >= sourceRows || column >= sourceColumns ? defaultValue() :  source[row, column];
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

        public static void SetCoordinates(this IGraphicComponent component, IGraphicComponent constraints)
        {
            component.SetCoordinates(constraints.X, constraints.Y, constraints.Width, constraints.Height);
        }

        public static void SetCoordinates(this IGraphicComponent component, float X, float Y, float width, float height)
        {
            component.X = X;
            component.Y = Y;
            component.Width = width;
            component.Height = height;
        }
    }

    public static class PublicExtensions
    {
        public static void CheckNull(this Object obj, string variableName)
        {
            if (obj == null)
                throw new ArgumentNullException(variableName + " must not be null");
        }
    }
}
