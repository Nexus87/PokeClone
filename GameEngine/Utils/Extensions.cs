using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine.Utils
{
    public static class Extensions
    {
        public delegate T Creator<T>();
        public delegate T CellCreator<T>(int row, int column);

        public static bool AlmostEqual(this float f1, float f2)
        {
            return AlmostEqual(f1, f2, 0.0001f);
        }

        public static bool AlmostEqual(this float f1, float f2, float epsilon)
        {
            return Math.Abs(f1 - f2) < epsilon;
        }
        public static int Rows<T>(this T[,] table)
        {
            return table.GetLength(0);
        }

        public static int Columns<T>(this T[,] table)
        {
            return table.GetLength(1);
        }

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

        public static void Copy<T>(this T[,] source, T[,] target, CellCreator<T> defaultValue)
        {
            int sourceRows = source.GetLength(0);
            int sourceColumns = source.GetLength(1);
            int targetRows = target.GetLength(0);
            int targetColumns = target.GetLength(1);

            for (int row = 0; row < targetRows; row++)
            {
                for (int column = 0; column < targetColumns; column++)
                {
                    target[row, column] = row >= sourceRows || column >= sourceColumns ? defaultValue(row, column) :  source[row, column];
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

    public static class PublicExtensions
    {
        public static void CheckNull(this Object obj, string variableName)
        {
            if (obj == null)
                throw new ArgumentNullException(variableName + " must not be null");
        }
    }
}
