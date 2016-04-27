﻿using GameEngine;
using GameEngine.Graphics.Views;
using GameEngine.Graphics.Widgets;
using GameEngineTest.Views;
using GameEngineTest.Util;
using GameEngine.Graphics;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace GameEngineTest.Graphics.Widgets
{
    [TestFixture]
    public class TableWidgetTest
    {
        public static List<TestCaseData> HandleInputTestData = new List<TestCaseData>
        {
            new TestCaseData(2, 3, 0, 0, CommandKeys.Right, 0, 1),
            new TestCaseData(3, 2, 0, 0, CommandKeys.Down, 1, 0),
            new TestCaseData(10, 10, 9, 9, CommandKeys.Up, 8, 9),
            new TestCaseData(10, 10, 9, 9, CommandKeys.Left, 9, 8)
        };

        public static List<TestCaseData> SelectCellTestData = new List<TestCaseData>
        {
            new TestCaseData(2, 2, 1, 0),
            new TestCaseData(1, 4, 0, 3),
            new TestCaseData(10, 4, 9, 3)
        };

        public static List<TestCaseData> ViewPortSelectionTestData = new List<TestCaseData>
        {
            new TestCaseData(15, 15, 2, 1, new TableIndex(3, 2), new TableIndex(3, 2), new TableIndex(4, 2)),
            new TestCaseData(10, 10, 4, 4, new TableIndex(3, 3), new TableIndex(0, 0), new TableIndex(3, 3)),
            new TestCaseData(10, 10, 3, 3, new TableIndex(8, 8), new TableIndex(7, 7), new TableIndex(9, 9))
        };

        public static List<TestCaseData> ViewPortInputTestData = new List<TestCaseData>
        {
            new TestCaseData(15, 15, 2, 1, new TableIndex(3, 2), CommandKeys.Left, new TableIndex(3, 1), new TableIndex(5, 2)),
            new TestCaseData(15, 15, 2, 1, new TableIndex(3, 2), CommandKeys.Right, new TableIndex(3, 3), new TableIndex(5, 4)),
            new TestCaseData(15, 15, 2, 1, new TableIndex(3, 2), CommandKeys.Up, new TableIndex(2, 2), new TableIndex(4, 3)),
            new TestCaseData(15, 15, 2, 1, new TableIndex(3, 2), CommandKeys.Down, new TableIndex(3, 2), new TableIndex(5, 3)),
        };

        public static List<TestCaseData> InvalidSelectionData = new List<TestCaseData>
        {
            new TestCaseData(10, 10, -1, 0),
            new TestCaseData(10, 10, 0, -1),
            new TestCaseData(10, 10, 10, 0),
            new TestCaseData(10, 10, 0, 10),
            new TestCaseData(10, 10, -1, -5),
            new TestCaseData(10, 10, 13, -1),
        };

        public static List<TestCaseData> ResizeTableTestData = new List<TestCaseData>
        {
            new TestCaseData(10, 10, 4, 4, new TableIndex(4, 4), 5, 5, new TableIndex(1, 1)),
            new TestCaseData(3, 3, 5, 5, new TableIndex(0, 0), 12, 7, new TableIndex(0, 0)),
            new TestCaseData(12, 6, 5, 3, new TableIndex(0, 0), 3, 2, new TableIndex(0, 0)),
            new TestCaseData(7, 8, 5, 5, new TableIndex(4, 6), 3, 1, new TableIndex(0, 0)),
            new TestCaseData(8, 8, 4, 4, new TableIndex(4, 4), 8, 2, new TableIndex(4, 0)),
            new TestCaseData(8, 8, 4, 4, new TableIndex(4, 4), 2, 8, new TableIndex(0, 4)),
            new TestCaseData(2, 2, 4, 4, new TableIndex(0, 0), 3, 3, new TableIndex(0, 0)),
            new TestCaseData(0, 0, 4, 4, null, 4, 4, new TableIndex(0, 0))
        };

        public static List<TestCaseData> InputKeysData = new List<TestCaseData>
        {
            new TestCaseData(10, 6, 3, 4, CommandKeys.Left),
            new TestCaseData(10, 6, 3, 4, CommandKeys.Right),
            new TestCaseData(10, 6, 3, 4, CommandKeys.Up),
            new TestCaseData(10, 6, 3, 4, CommandKeys.Down)
        };

        [TestCase]
        public void CreateWidget_ZeroSizeTable_DoesNotThrow()
        {
            var tableViewStub = new TableViewMock();
            Assert.DoesNotThrow(() => CreateTableWidget(tableViewStub, 0, 0));
        }

        [TestCase(8, 8, 1, 3, 1, 3)]
        [TestCase(3, 5, 0, 0, 0, 0)]
        public void ResizeHandler_AutoResizeShrinkTableView_EndIndexIsAdjusted(int rows, int columns, 
            int newRows, int newColumns, int endRow, int endColumn)
        {
            var endIndex = new TableIndex(endRow, endColumn);
            var view = new TableViewMock();
            var table = CreateTableWidget(view, rows, columns);
            
            view.Rows = newRows;
            view.Columns = newColumns;
            view.RaiseTableResizeEvent(newRows, newColumns);

            AssertIndex(endIndex, view.EndIndex.Value);
        }

        [TestCase(8, 8, 20, 1, 20, 1)]
        [TestCase(0, 0, 10, 32, 10, 32)]
        public void ResizeHandler_AutoResizeGrowingTableView_EndIndexIsAdjusted(int rows, int columns,
            int newRows, int newColumns, int endRow, int endColumn)
        {
            var endIndex = new TableIndex(endRow, endColumn);
            var view = new TableViewMock();
            var table = CreateTableWidget(view, rows, columns);

            view.Rows = newRows;
            view.Columns = newColumns;
            view.RaiseTableResizeEvent(newRows, newColumns);

            AssertIndex(endIndex, view.EndIndex.Value);
        }

        [TestCase(0, 0, 5, 5, 0, 0)]
        [TestCase(8, 3, 2, 1, 2, 1)]
        [TestCase(1, 5, 5, 5, 1, 5)]
        public void CreateTable_WithVisibleColumnsAndRows_EndIndexHasExpectedValue(int rows, int column, int visibleRows, int visibleColumns, int endRow, int endColumn)
        {
            var view = new TableViewMock();
            var table = CreateTableWidget(view, rows, column, visibleRows, visibleColumns);
            var endIndex = new TableIndex(endRow, endColumn);

            AssertIndex(endIndex, view.EndIndex.Value);
            
        }

        [TestCase(10, 10, -1, 0)]
        [TestCase(10, 10, 0, -1)]
        [TestCase(10, 10, 10, 0)]
        [TestCase(10, 10, 0, 10)]
        [TestCase(10, 10, -1, -5)]
        [TestCase(10, 10, 13, -1)]
        public void SelectCell_InvalidCell_ThrowsException(int rows, int columns, int selectedRow, int selectedColumn)
        {
            var tableViewMock = new TableViewMock();
            var table = CreateTableWidget(tableViewMock, rows, columns);

            Assert.Throws<ArgumentOutOfRangeException>(delegate { table.SelectCell(selectedRow, selectedColumn); });
        }

        [TestCase(10.0f, 12.0f, 400.0f, 500.0f)]
        public void Draw_SetCoordiantes_TableViewFillsWholeSpace(float x, float y, float width, float height)
        {
            var tableViewMock = new TableViewMock();
            var table = CreateTableWidget(tableViewMock);
            table.SetCoordinates(x, y, width, height);

            table.Draw();

            Assert.AreEqual(x, tableViewMock.XPosition);
            Assert.AreEqual(y, tableViewMock.YPosition);
            Assert.AreEqual(width, tableViewMock.Width);
            Assert.AreEqual(height, tableViewMock.Height);
        }

        [TestCase(2, 2, 1, 0)]
        [TestCase(1, 4, 0, 3)]
        [TestCase(10, 4, 9, 3)]
        public void SelectCell_TableViewReturnsFalse_CursorHasNotChanged(int rows, int columns, int selectedRow, int selectedColumn)
        {
            var tableViewMock = new TableViewMock { SelectionReturnValue = false };
            var table = CreateTableWidget(tableViewMock, rows, columns);

            int startCursorRow = table.cursorRow;
            int startCursorColumn = table.cursorColumn;

            table.SelectCell(selectedRow, selectedColumn);

            Assert.AreEqual(startCursorRow, table.cursorRow);
            Assert.AreEqual(startCursorColumn, table.cursorColumn);
        }

        [TestCase(10, 6, 3, 4, CommandKeys.Left)]
        [TestCase(10, 6, 3, 4, CommandKeys.Right)]
        [TestCase(10, 6, 3, 4, CommandKeys.Up)]
        [TestCase(10, 6, 3, 4, CommandKeys.Down)]
        public void HandleInput_TableViewSelectionFails_CursorNotChanged(int rows, int columns, int initialSelectedRow, int initialSelectedColumn,
            CommandKeys key)
        {
            var tableViewMock = new TableViewMock();
            var table = CreateTableWidget(tableViewMock, rows, columns);

            table.SelectCell(initialSelectedRow, initialSelectedColumn);
            tableViewMock.SelectionReturnValue = false;

            table.HandleInput(key);

            Assert.AreEqual(initialSelectedRow, table.cursorRow);
            Assert.AreEqual(initialSelectedColumn, table.cursorColumn);
        }

        [TestCase(2, 2, 1, 0)]
        [TestCase(1, 4, 0, 3)]
        [TestCase(10, 4, 9, 3)]
        public void SelectCell_WithValidData_TableViewSelectCellIsCalled(int rows, int columns, int selectedRow, int selectedColumn)
        {
            var tableViewMock = new Mock<TableViewMock> { CallBase = true };
            var table = CreateTableWidget(tableViewMock.Object, rows, columns);
            tableViewMock.Setup(o => o.SetCellSelection(selectedRow, selectedColumn, true)).Verifiable();

            table.SelectCell(selectedRow, selectedColumn);

            tableViewMock.Verify();

        }

        [TestCase(2, 3, 0, 0, CommandKeys.Right, 0, 1)]
        [TestCase(3, 2, 0, 0, CommandKeys.Down, 1, 0)]
        [TestCase(10, 10, 9, 9, CommandKeys.Up, 8, 9)]
        [TestCase(10, 10, 9, 9, CommandKeys.Left, 9, 8)]
        public void HandleInput_StartingFromSomeCell_TableViewSelectCellIsCalled(int rows, int columns, int selectedRow, int selectedColumn, 
                                          CommandKeys key, int newRow, int newColumns)
        {
            var tableViewMock = new Mock<TableViewMock> { CallBase = true };
            var table = CreateTableWidget(tableViewMock.Object, rows, columns);
            table.SelectCell(selectedRow, selectedColumn);

            tableViewMock.ResetCalls();
            tableViewMock.Setup(o => o.SetCellSelection(newRow, newColumns, true)).Verifiable();

            table.HandleInput(key);

            tableViewMock.Verify();
        }

        [TestCase(20, 32, 5, 3, 10, 21, 11, 22)]
        public void SetSelection_WithVisibleRowsColumnsSet_EndIndexAdjusts(int rows, int columns, int visibleRows, int visibleColumns, 
            int selectedRow, int selectedColumn, int endRow, int endColumn)
        {
            var tableViewMock = new TableViewMock();
            var table = CreateTableWidget(tableViewMock, rows, columns, visibleRows, visibleColumns);
            var endIndex = new TableIndex(endRow, endColumn);

            table.SelectCell(selectedRow, selectedColumn);

            AssertIndex(endIndex, tableViewMock.EndIndex.Value);
            
        }

        [TestCase(20, 32, 5, 3, 10, 21, 6, 19)]
        public void SetSelection_WithVisibleRowsColumnsSet_StartIndexAdjusts(int rows, int columns, int visibleRows, int visibleColumns, 
            int selectedRow, int selectedColumn, int startRow, int startColumn)
        {
            var tableViewMock = new TableViewMock();
            var table = CreateTableWidget(tableViewMock, rows, columns, visibleRows, visibleColumns);
            var startIndex = new TableIndex(startRow, startColumn);

            table.SelectCell(selectedRow, selectedColumn);

            AssertIndex(startIndex, tableViewMock.StartIndex.Value);

        }

        [TestCase(20, 32, 5, 3, 10, 21, 1, 2, 1, 2)]
        public void SetSelection_CalledTwiceWithDifferentIndexes_StartIndexAdjusts(int rows, int columns, int visibleRows, int visibleColumns, 
            int selectedRow, int selectedColumn, int selectedRow2, int selectedColumn2, 
            int startRow, int startColumn)
        {
            var tableViewMock = new TableViewMock();
            var table = CreateTableWidget(tableViewMock, rows, columns, visibleRows, visibleColumns);
            var startIndex = new TableIndex(startRow, startColumn);

            table.SelectCell(selectedRow, selectedColumn);
            table.SelectCell(selectedRow2, selectedColumn2);

            AssertIndex(startIndex, tableViewMock.StartIndex.Value);

        }

        [TestCase(20, 32, 5, 3, 10, 21, 1, 2, 6, 5)]
        public void SetSelection_CalledTwiceWithDifferentIndexes_EndIndexAdjusts(int rows, int columns, int visibleRows, int visibleColumns,
            int selectedRow, int selectedColumn, int selectedRow2, int selectedColumn2, 
            int endRow, int endColumn)
        {
            var tableViewMock = new TableViewMock();
            var table = CreateTableWidget(tableViewMock, rows, columns, visibleRows, visibleColumns);
            var endIndex = new TableIndex(endRow, endColumn);

            table.SelectCell(selectedRow, selectedColumn);
            table.SelectCell(selectedRow2, selectedColumn2);

            AssertIndex(endIndex, tableViewMock.EndIndex.Value);

        }

        [TestCase(20, 32, 5, 3, 10, 21, CommandKeys.Right, 11, 23)]
        public void HandleInput_WithVisibleRowsColumnsSet_EndIndexAdjusts(int rows, int columns, int visibleRows, int visibleColumns,
            int selectedRow, int selectedColumn,
            CommandKeys key,
            int endRow, int endColumn)
        {
            var tableViewMock = new TableViewMock();
            var table = CreateTableWidget(tableViewMock, rows, columns, visibleRows, visibleColumns);
            var endIndex = new TableIndex(endRow, endColumn);

            table.SelectCell(selectedRow, selectedColumn);
            table.HandleInput(key);

            AssertIndex(endIndex, tableViewMock.EndIndex.Value);

        }

        [TestCase(20, 32, 5, 3, 10, 21, CommandKeys.Right, 6, 20)]
        public void HandleInput_WithVisibleRowsColumnsSet_StartIndexAdjusts(int rows, int columns, int visibleRows, int visibleColumns,
            int selectedRow, int selectedColumn,
            CommandKeys key,
            int startRow, int startColumn)
        {
            var tableViewMock = new TableViewMock();
            var table = CreateTableWidget(tableViewMock, rows, columns, visibleRows, visibleColumns);
            var startIndex = new TableIndex(startRow, startColumn);

            table.SelectCell(selectedRow, selectedColumn);
            table.HandleInput(key);

            AssertIndex(startIndex, tableViewMock.StartIndex.Value);

        }

        [TestCase(10, 4, 9, 3, 1, 1, 0, 0)]
        [TestCase(10, 4, 9, 3, 0, 0, 0, 0)]
        public void Cursor_OnTableResize_IsAdjusted(int rows, int columns, int selectedRow, int selectedColumn, 
            int newRows, int newColumns,
            int expectedRow, int expectedColumn)
        {
            var tableViewMock = new TableViewMock();
            var table = CreateTableWidget(tableViewMock, rows, columns);
            table.SelectCell(selectedRow, selectedColumn);

            tableViewMock.RaiseTableResizeEvent(newRows, newColumns);

            Assert.AreEqual(expectedRow, table.cursorRow);
            Assert.AreEqual(expectedColumn, table.cursorColumn);
        }

        private static void AssertIndex(TableIndex idx, TableIndex idx2)
        {
            AssertIndex(idx2, idx.Row, idx.Column);
        }

        private static void AssertIndex(TableIndex idx, int row, int column)
        {
            Assert.AreEqual(column, idx.Column);
            Assert.AreEqual(row, idx.Row);
        }

        private TableWidget<TestType> CreateTableWidget(TableViewMock view, int rows, int columns, int? visibleRows = null, int? visibleColumns = null)
        {
            SetupView(view, rows, columns);
            return CreateTableWidget(view, visibleRows, visibleColumns);
        }

        private TableWidget<TestType> CreateTableWidget(TableViewMock view, int? visibleRows = null, int? visibleColumns = null)
        {
            
            var widget = new TableWidget<TestType>(visibleRows, visibleColumns, view, new PokeEngine());
            widget.SetCoordinates(10, 10, 500, 500);
            return widget;
        }
        private void SetupView(TableViewMock tableViewMock, int rows, int columns)
        {
            tableViewMock.Rows = rows;
            tableViewMock.Columns = columns;
        }
    }
}
