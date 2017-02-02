using System;
using System.Collections;
using System.Collections.Generic;

namespace GameEngine.Globals
{
    internal class SubTable<T> : ITable<T>
    {
        private TableIndex _startIndex;
        private TableIndex _endIndex;

        private readonly T[,] _table;

        public SubTable(T[,] table, TableIndex startIndex, TableIndex endIndex)
        {
            _startIndex = startIndex;
            _endIndex = endIndex;
            _table = table;
        }

        public int Columns => _endIndex.Column - _startIndex.Column;

        public int Rows => _endIndex.Row - _startIndex.Row;

        public T this[int row, int column]
        {
            get
            {
                if (row >= Rows || row < 0)
                    throw new ArgumentOutOfRangeException(nameof(row));
                if (column >= Columns || column < 0)
                    throw new ArgumentOutOfRangeException(nameof(row));

                return _table[_startIndex.Row + row, _startIndex.Column + column];
            }
        }

        public IEnumerable<T> EnumerateColumns(int row)
        {
            if (row >= Rows)
                yield break;

            for (var i = 0; i < Columns; i++)
                yield return this[row, i];
        }

        public IEnumerable<T> EnumerateRows(int column)
        {
            if (column >= Columns)
                yield break;

            for (var i = 0; i < Rows; i++)
                yield return this[i, column];
        }

        public IEnumerable<T> EnumerateAlongRows()
        {
            for (var row = 0; row < Rows; row++)
            {
                for (var column = 0; column < Columns; column++)
                    yield return this[row, column];
            }
        }

        public IEnumerable<T> EnumerateAlongColumns()
        {
            for (var column = 0; column < Columns; column++)
            {
                for (var row = 0; row < Rows; row++)
                    yield return this[row, column];
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return EnumerateAlongColumns().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return EnumerateAlongColumns().GetEnumerator();
        }
    }
}