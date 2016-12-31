using System;
using System.Collections.Generic;

namespace GameEngine.Globals
{
    public class Table<T> : ITable<T>
    {
        private const int InitalRows = 0;
        private const int InitalColumns = 0;

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

        public void RemoveRow(int removedRow)
        {
            if(removedRow >= Rows || removedRow < 0)
                return;

            var newTable = new T[Rows - 1, Columns];
            CopyRows(0, removedRow, newTable, 0);
            CopyRows(removedRow + 1, Rows, newTable, removedRow);

            _innerTable = newTable;
            Rows--;
        }

        private void CopyRows(int startRow, int endRow, T[,] newTable, int offset)
        {
            var realOffset = offset * Columns;
            var entriesToCopy = (endRow - startRow) * Columns;
            Array.Copy(_innerTable, startRow * Columns, newTable, realOffset, entriesToCopy);
        }

        public void RemoveColumn(int removedColumn)
        {
            if(removedColumn >= Columns || removedColumn < 0)
                return;

            var newTable = new T[Rows, Columns - 1];
            CopyColumns(0, removedColumn, newTable, 0);
            CopyColumns(removedColumn + 1, Columns, newTable, removedColumn);

            _innerTable = newTable;
            Columns--;
        }

        private void CopyColumns(int startColumn, int endColumn, T[,] target, int offset)
        {
            var columnsToCopy = endColumn - startColumn;
            for (var row = 0; row < Rows; row++)
            {
                var sourceOffset = row * Columns + startColumn;
                var targetOffset = row * (Columns-1) + offset;
                Array.Copy(_innerTable, sourceOffset, target, targetOffset, columnsToCopy);
            }
        }
    }
}