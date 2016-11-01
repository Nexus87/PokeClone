﻿using GameEngine.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine.Graphics.TableView;

namespace GameEngineTest.Utils
{
    [TestFixture]
    public class TableTest
    {
        [TestCase]
        public void RowsColumns_InitalSetup_Zero()
        {
            var table = new Table<object>();

            Assert.AreEqual(0, table.Rows);
            Assert.AreEqual(0, table.Columns);
        }

        [TestCase]
        public void Rows_SetData_ResizeAsNeeded()
        {
            var table = new Table<object> {[5, 0] = new object()};


            Assert.AreEqual(6, table.Rows);
        }

        [TestCase]
        public void Columns_SetData_ResizeAsNeeded()
        {
            var table = new Table<object> {[0, 5] = new object()};


            Assert.AreEqual(6, table.Columns);
        }

        [TestCase]
        public void RowsColumns_SetData_ResizeAsNeeded()
        {
            var table = new Table<object> {[3, 5] = new object()};

            Assert.AreEqual(6, table.Columns);
            Assert.AreEqual(4, table.Rows);
        }

        [TestCase]
        public void IndexerGet_DataNotSet_ReturnsDefault()
        {
            var table = new Table<object> {[5, 5] = new object()};

            Assert.AreEqual(default(object), table[0, 0]);
        }

        [TestCase]
        public void IndexerGet_TryGetAfterResize_ReturnsSameValue()
        {
            var testObject = new object();
            var table = new Table<object>
            {
                [4, 4] = testObject,
                [8, 6] = new object()
            };

            Assert.AreEqual(testObject, table[4, 4]);
        }

        [TestCase]
        public void IndexerGet_InsertSecondOnlyWithColumnResize_DoesNotShrink()
        {
            var table = new Table<object>
            {
                [3, 5] = new object(),
                [6, 3] = new object()
            };


            Assert.AreEqual(7, table.Rows);
            Assert.AreEqual(6, table.Columns);
        }

        [TestCase]
        public void IndexerGet_InsertSecondOnlyWithRowResize_DoesNotShrink()
        {
            var table = new Table<object>
            {
                [5, 3] = new object(),
                [3, 6] = new object()
            };

            Assert.AreEqual(6, table.Rows);
            Assert.AreEqual(7, table.Columns);
        }

        [TestCase]
        public void EnumerateColumns_OnEmptyTable_CountZero()
        {
            var table = new Table<object>();

            var enumerator = table.EnumerateColumns(0);

            Assert.AreEqual(0, enumerator.Count());
        }

        [TestCase]
        public void EnumerateRows_OnEmptyTable_CountZero()
        {
            var table = new Table<object>();

            var enumerator = table.EnumerateRows(0);

            Assert.AreEqual(0, enumerator.Count());
        }

        [TestCase]
        public void EnumerateColumns_SetLastEntry_ReturnsExpected()
        {
            var testObject = new object();
            var table = new Table<object> {[4, 4] = testObject};


            var enumerator = table.EnumerateColumns(4).ToList();

            Assert.AreEqual(4, enumerator.Count(o => o == null));
            Assert.AreEqual(testObject, enumerator.Last());
        }

        [TestCase]
        public void EnumerateRows_SetLastEntry_ReturnsExpected()
        {
            var testObject = new object();
            var table = new Table<object> {[4, 4] = testObject};

            var enumerator = table.EnumerateRows(4).ToList();

            Assert.AreEqual(4, enumerator.Count(o => o == null));
            Assert.AreEqual(testObject, enumerator.Last());
        }

        [TestCase]
        public void EnumerateColumns_ArgumentOutOfBound_CountZero()
        {
            var testObject = new object();
            var table = new Table<object> {[4, 4] = testObject};

            var enumerator = table.EnumerateColumns(5);

            Assert.AreEqual(0, enumerator.Count());
        }

        [TestCase]
        public void EnumerateRows_ArgumentOutOfBound_CountZero()
        {
            var testObject = new object();
            var table = new Table<object> {[4, 4] = testObject};

            var enumerator = table.EnumerateRows(5);

            Assert.AreEqual(0, enumerator.Count());
        }

        public static List<TestCaseData> EnumeratedData = new List<TestCaseData>()
        {
            new TestCaseData(new List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9})
        };

        [TestCaseSource(nameof(EnumeratedData))]
        public void EnumerateAlongColumns_BasicSetup_Order(List<int> result)
        {
            var table = new Table<int>();
            var start = 1;
            for (var column = 0; column < 3; column++, start+=3)
                SetColumn(table, column, start);

            var cnt = 0;
            foreach(var i in table.EnumerateAlongColumns()){
                Assert.AreEqual(result[cnt], i);
                cnt++;
            }
        }

        [TestCaseSource(nameof(EnumeratedData))]
        public void EnumerateAlongRows_BasicSetup_Order(List<int> result)
        {
            var table = new Table<int>();
            var start = 1;
            for (var row = 0; row < 3; row++, start += 3)
                SetRow(table, row, start);

            var cnt = 0;
            foreach (var i in table.EnumerateAlongRows())
            {
                Assert.AreEqual(result[cnt], i);
                cnt++;
            }
        }

        private static void SetRow(Table<int> table, int row, int start)
        {
            for (var i = 0; i < 3; i++, start++)
                table[row, i] = start;
        }

        private static void SetColumn(Table<int> table, int column, int start)
        {
            for (var i = 0; i < 3; i++, start++)
                table[i, column] = start;
        }

        [TestCase]
        public void CreateSubtable_WithValidIndexes_RightNumberOfRowsAndColumns()
        {
            var table = new Table<object> {[5, 5] = new object()};

            var subTable = table.CreateSubtable(new TableIndex(1, 1), new TableIndex(3, 3));

            Assert.AreEqual(2, subTable.Rows);
            Assert.AreEqual(2, subTable.Columns);
        }

        [TestCase]
        public void CreateSubtable_DataFilledTable_HasEntries()
        {
            var table = new Table<int>();
            for (var i = 0; i < 3; i++)
                for (var j = 0; j < 3; j++)
                    table[i, j] = i + j;

            var subTable = table.CreateSubtable(new TableIndex(1, 1), new TableIndex(3, 3));

            Assert.AreEqual(1 + 1, subTable[0, 0]);
            Assert.AreEqual(1 + 2, subTable[0, 1]);
            Assert.AreEqual(2 + 1, subTable[1, 0]);
            Assert.AreEqual(2 + 2, subTable[1, 1]);
        }

        [TestCase]
        public void IndexerSet_InvalidRow_ThrowsException()
        {
            var table = new Table<object>();

            Assert.Throws<ArgumentOutOfRangeException>(() => table[-1, 0] = new object());
        }

        [TestCase]
        public void IndexerSet_InvalidColumn_ThrowsException()
        {
            var table = new Table<object>();

            Assert.Throws<ArgumentOutOfRangeException>(() => table[0, -1] = new object());
        }
    }
}
