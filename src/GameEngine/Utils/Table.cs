﻿using GameEngine.Graphics;
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

        private T[,] innerTable;

        public Table(int rows, int columns)
        {
            innerTable = new T[rows, columns];
            Rows = rows;
            Columns = columns;
        }

        public Table() 
        {
            innerTable = new T[InitalRows, InitalColumns];
        }

        private Table(T[,] array)
        {
            innerTable = array;
            Rows = array.Rows();
            Columns = array.Columns();
        }

        public T this[int row, int column]
        {
            get
            {
                CheckIndexIsPositive(row, column);

                if(!IsInBounds(row, column))
                    return default(T);

                return innerTable[row, column];
            }
            set
            {
                CheckIndexIsPositive(row, column);

                if (!IsInBounds(row, column))
                    ResizeTable(row, column);

                innerTable[row, column] = value;
            }
        }

        private void CheckIndexIsPositive(int row, int column){
            if (row < 0)
                throw new ArgumentOutOfRangeException("row", "was: " + row);
            if (column < 0)
                throw new ArgumentOutOfRangeException("column", "was: " + column);
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
            if (innerTable.Rows() >= newRows && innerTable.Columns() >= newColumns)
                return;

            newRows = Math.Max(newRows, 2 * innerTable.Rows());
            newColumns = Math.Max(newColumns, 2 * innerTable.Columns());

            var tmpTable = new T[newRows, newColumns];
            innerTable.Copy(tmpTable);
            innerTable = tmpTable;
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
            return new SubTable<T>(innerTable, startIndex, endIndex);
        }

        internal class SubTable<TS> : ITable<TS>
        {
            private TableIndex startIndex;
            private TableIndex endIndex;

            private readonly TS[,] table;

            public SubTable(TS[,] table, TableIndex startIndex, TableIndex endIndex)
            {
                this.startIndex = startIndex;
                this.endIndex = endIndex;
                this.table = table;
            }

            public int Columns
            {
                get { return endIndex.Column - startIndex.Column; }
            }

            public int Rows
            {
                get { return endIndex.Row - startIndex.Row; }
            }

            public TS this[int row, int column]
            {
                get 
                {
                    if (row >= Rows || row < 0)
                        throw new ArgumentOutOfRangeException("row");
                    if(column >= Columns || column < 0)
                        throw new ArgumentOutOfRangeException("row");

                    return table[startIndex.Row + row, startIndex.Column + column]; 
                }
            }

            public IEnumerable<TS> EnumerateColumns(int row)
            {
                if (row >= Rows)
                    yield break;

                for (int i = 0; i < Columns; i++)
                    yield return this[row, i];
            }

            public IEnumerable<TS> EnumerateRows(int column)
            {
                if (column >= Columns)
                    yield break;

                for (int i = 0; i < Rows; i++)
                    yield return this[i, column];
            }

            public IEnumerable<TS> EnumerateAlongRows()
            {
                for (int row = 0; row < Rows; row++)
                {
                    for (int column = 0; column < Columns; column++)
                        yield return this[row, column];
                }
            }

            public IEnumerable<TS> EnumerateAlongColumns()
            {
                for (int column = 0; column < Columns; column++)
                {
                    for (int row = 0; row < Rows; row++)
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
