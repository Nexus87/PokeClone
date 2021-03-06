﻿using System;

namespace GameEngine.Globals
{
    public static class Extensions
    {
        public delegate T Creator<out T>();
        public delegate T CellCreator<out T>(int row, int column);

        public static bool AlmostEqual(this float f1, float f2)
        {
            return AlmostEqual(f1, f2, 0.0001f);
        }

        public static bool AlmostEqual(this double f1, double f2)
        {
            return AlmostEqual(f1, f2, 0.0001d);
        }

        public static bool AlmostEqual(this float f1, float f2, float epsilon)
        {
            return Math.Abs(f1 - f2) < epsilon;
        }

        public static bool AlmostEqual(this double f1, double f2, double epsilon)
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
            var rows = Math.Min(source.GetLength(0), target.GetLength(0));
            var columns = Math.Min(source.GetLength(1), target.GetLength(1));

            LoopOverTable(rows, columns, (i, j) => target[i,j] = source[i,j] );
        }

        public static void LoopOverTable(int rows, int columns, Action<int, int> action)
        {
            for(var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    action(i, j);
                }
            }
        }

        public static void LoopOverTable<T>(this ITable<T> table, Action<int, int> action)
        {
            LoopOverTable(table.Rows, table.Columns, action);
        }

        public static void CheckNull(this object obj, string variableName)
        {
            if (obj == null)
                throw new ArgumentNullException(variableName + " must not be null");
        }

    }
}
