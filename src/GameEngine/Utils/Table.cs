using System;
using System.Collections.Generic;
using GameEngine.Graphics.TableView;

namespace GameEngine.Utils
{
    public class Table<T> : ITable<T>
    {
        private const int InitalRows = 8;
        private const int InitalColumns = 8;

        public int Rows { get; private set; }
        public int Columns { get; private set; }

        private T[,] _innerTable;

        public Table(int rows, int columns)
        {
            _innerTable = new T[rows, columns];
            Rows = rows;
            Columns = columns;
        }

        public Table()
        {
            _innerTable = new T[InitalRows, InitalColumns];
        }

        public Table(T[,] array)
        {
            _innerTable = array;
            Rows = array.Rows();
            Columns = array.Columns();
        }

        public T this[int row, int column]
        {
            get
            {
                CheckIndexIsPositive(row, column);

                return IsInBounds(row, column) ? _innerTable[row, column] : default(T);
            }
            set
            {
                CheckIndexIsPositive(row, column);

                if (!IsInBounds(row, column))
                    ResizeTable(row, column);

                _innerTable[row, column] = value;
            }
        }

        private static void CheckIndexIsPositive(int row, int column)
        {
            if (row < 0)
                throw new ArgumentOutOfRangeException(nameof(row), "was: " + row);
            if (column < 0)
                throw new ArgumentOutOfRangeException(nameof(column), "was: " + column);
        }

        private bool IsInBounds(int row, int column)
        {
            return row < Rows && column < Columns;
        }

        private void ResizeTable(int row, int column)
        {
            var newColumns = CalculateColumnsNumber(column);
            var newRows = CalculateRowsNumber(row);

            ResizeInnerTable(newColumns, newRows);

            Columns = newColumns;
            Rows = newRows;
        }

        private int CalculateRowsNumber(int row)
        {
            return row < Rows ? Rows : row + 1;
        }

        private int CalculateColumnsNumber(int column)
        {
            return column < Columns ? Columns : column + 1;
        }

        private void ResizeInnerTable(int newColumns, int newRows)
        {
            if (_innerTable.Rows() >= newRows && _innerTable.Columns() >= newColumns)
                return;

            newRows = Math.Max(newRows, 2 * _innerTable.Rows());
            newColumns = Math.Max(newColumns, 2 * _innerTable.Columns());

            var tmpTable = new T[newRows, newColumns];
            _innerTable.Copy(tmpTable);
            _innerTable = tmpTable;
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

        public ITable<T> CreateSubtable(TableIndex startIndex, TableIndex endIndex)
        {
            return new SubTable<T>(_innerTable, startIndex, endIndex);
        }

        internal class SubTable<TS> : ITable<TS>
        {
            private TableIndex _startIndex;
            private TableIndex _endIndex;

            private readonly TS[,] _table;

            public SubTable(TS[,] table, TableIndex startIndex, TableIndex endIndex)
            {
                _startIndex = startIndex;
                _endIndex = endIndex;
                _table = table;
            }

            public int Columns => _endIndex.Column - _startIndex.Column;

            public int Rows => _endIndex.Row - _startIndex.Row;

            public TS this[int row, int column]
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

            public IEnumerable<TS> EnumerateColumns(int row)
            {
                if (row >= Rows)
                    yield break;

                for (var i = 0; i < Columns; i++)
                    yield return this[row, i];
            }

            public IEnumerable<TS> EnumerateRows(int column)
            {
                if (column >= Columns)
                    yield break;

                for (var i = 0; i < Rows; i++)
                    yield return this[i, column];
            }

            public IEnumerable<TS> EnumerateAlongRows()
            {
                for (var row = 0; row < Rows; row++)
                {
                    for (var column = 0; column < Columns; column++)
                        yield return this[row, column];
                }
            }

            public IEnumerable<TS> EnumerateAlongColumns()
            {
                for (var column = 0; column < Columns; column++)
                {
                    for (var row = 0; row < Rows; row++)
                        yield return this[row, column];
                }
            }

            public IEnumerator<TS> GetEnumerator()
            {
                return EnumerateAlongColumns().GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return EnumerateAlongColumns().GetEnumerator();
            }
        }

        public static ITable<T> FromArray(T[,] array)
        {
            return new Table<T>(array);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return EnumerateAlongColumns().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return EnumerateAlongColumns().GetEnumerator();
        }
    }
}