using System.Collections.Generic;

namespace GameEngine.Globals
{
    public interface ITable<out T> : IEnumerable<T>
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
