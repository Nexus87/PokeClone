using System;
using System.Collections.Generic;
namespace GameEngine.Utils
{
    public interface ITable<T>
    {
        int Columns { get; }
        int Rows { get; }
        T this[int row, int column] { get; }

        IEnumerable<T> EnumerateColumns(int row);
        IEnumerable<T> EnumerateRows(int column);

        IEnumerable<T> EnumerateAlongRows();
        IEnumerable<T> EnumerateAlongColumns();
    }
}
