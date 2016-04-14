using GameEngine.Graphics.Views;
using System;
using System.Collections.Generic;

namespace GameEngine.Utils
{
    public class Table<T> : ITable<T>
    {
        const int INITAL_ROWS = 8;
        const int INITAL_COLUMNS = 8;

        public int Rows { get; private set; }
        public int Columns { get; private set; }
        int rows;
        int columns;
        T[,] innerTable = new T[INITAL_ROWS, INITAL_COLUMNS];

        public T this[int row, int column]
        {
            get
            {
                if(!IsInBounds(row, column))
                    return default(T);

                return innerTable[row, column];
            }
            set
            {
                if (!IsInBounds(row, column))
                    ResizeInnerTable(row, column);

                innerTable[row, column] = value;
            }
        }
        private bool IsInBounds(int row, int column)
        {
            return row < Rows && column < Columns;
        }

        private void ResizeInnerTable(int row, int column)
        {
            var newColumns = GetNewColumn(column);
            var newRows = GetNewRow(row);

            var tmpTable = new T[newRows, newColumns];
            innerTable.Copy(tmpTable);
            innerTable = tmpTable;

            Columns = newColumns;
            Rows = newRows;
        }

        private int GetNewRow(int row)
        {
            return row < Rows ? Rows : row + 1;
        }

        private int GetNewColumn(int column)
        {
            return column < Columns ? Columns : column + 1;
        }

        public IEnumerable<T> EnumerateColumns(int row)
        {
            if (row >= Rows)
                yield break;

            for (int i = 0; i < Columns; i++)
                yield return this[row, i];
        }

        public IEnumerable<T> EnumerateRows(int column)
        {
            if (column >= Columns)
                yield break;

            for (int i = 0; i < Rows; i++)
                yield return this[i, column];
        }

        public IEnumerable<T> EnumerateAlongRows()
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                    yield return this[row, column];
            }
        }

        public IEnumerable<T> EnumerateAlongColumns()
        {
            for (int column = 0; column < Columns; column++)
            {
                for (int row = 0; row < Rows; row++)
                    yield return this[row, column];
            }
        }

        public ITable<T> CreateSubtable(TableIndex startIndex, TableIndex endIndex)
        {
            return new SubTable<T>(innerTable, startIndex, endIndex);
        }

        internal class SubTable<S> : ITable<S>
        {
            TableIndex startIndex;
            TableIndex endIndex;

            S[,] table;

            public SubTable(S[,] table, TableIndex startIndex, TableIndex endIndex)
            {
                this.startIndex = startIndex;
                this.endIndex = endIndex;
                this.table = table;
            }

            public int Columns
            {
                get { return endIndex.Column - startIndex.Column + 1; }
            }

            public int Rows
            {
                get { return endIndex.Row - startIndex.Row + 1; }
            }

            public S this[int row, int column]
            {
                get { return table[startIndex.Row + row, startIndex.Column + column]; }
            }

            public IEnumerable<S> EnumerateColumns(int row)
            {
                if (row >= Rows)
                    yield break;

                for (int i = startIndex.Column; i <= endIndex.Column; i++)
                    yield return this[row, i];
            }

            public IEnumerable<S> EnumerateRows(int column)
            {
                if (column >= Columns)
                    yield break;

                for (int i = startIndex.Row; i <= endIndex.Row; i++)
                    yield return this[i, column];
            }

            public IEnumerable<S> EnumerateAlongRows()
            {
                for (int row = startIndex.Row; row <= endIndex.Row; row++)
                {
                    for (int column = startIndex.Column; column <= endIndex.Column; column++)
                        yield return this[row, column];
                }
            }

            public IEnumerable<S> EnumerateAlongColumns()
            {
                for (int column = startIndex.Column; column <= endIndex.Column; column++)
                {
                    for (int row = startIndex.Row; row <= endIndex.Row; row++)
                        yield return this[row, column];
                }
            }
        }
    }
}
