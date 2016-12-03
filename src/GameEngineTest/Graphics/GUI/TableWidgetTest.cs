using GameEngine;
using GameEngine.Graphics;
using GameEngine.Graphics.GUI;
using GameEngineTest.TestUtils;
using NUnit.Framework;
using System;
using FakeItEasy;
using GameEngine.Graphics.TableView;

namespace GameEngineTest.Graphics.GUI
{
    [TestFixture]
    public class TableWidgetTest : IWidgetTest
    {
        [TestCase]
        public void CreateWidget_ZeroSizeTable_DoesNotThrow()
        {
            var tableViewStub = new TableViewMock();
            Assert.DoesNotThrow(() => CreateTableWidget(tableViewStub, 0, 0));
        }

        [TestCase(8, 8, 1, 3, 1, 3)]
        [TestCase(3, 5, 0, 0, 0, 0)]
        [TestCase(8, 8, 20, 1, 20, 1)]
        [TestCase(0, 0, 10, 32, 10, 32)]
        [TestCase(5, 5, 4, 32, 4, 32)]
        public void ResizeHandler_AutoResizeTableViewResize_EndIndexIsAdjusted(int rows, int columns, 
            int newRows, int newColumns, int endRow, int endColumn)
        {
            var endIndex = new TableIndex(endRow, endColumn);
            var view = new TableViewMock();
            CreateTableWidget(view, rows, columns);
            
            view.Rows = newRows;
            view.Columns = newColumns;
            view.RaiseTableResizeEvent(newRows, newColumns);

            AssertIndex(endIndex, view.EndIndex.Value);
        }

        [TestCase(8, 8,    2, 4,    4, 5,    9, 1,    5, 1)]
        [TestCase(8, 8,    1, 2,    4, 5,    9, 9,    5, 6)]
        [TestCase(8, 8,    1, 2,    4, 5,    1, 1,    1, 1)]
        [TestCase(2, 1,    2, 2,    0, 0,    5, 4,    2, 2)]
        [TestCase(3, 2,    3, 2,    0, 0,    1, 2,    1, 2)]
        public void Resizehandler_RestrictedVisibleArea_EndIndexIsAdjusted(int rows, int columns, 
            int visibleRows, int visibleColumns,
            int selectedRow, int selectedColumn,
            int newRows, int newColumns, int endRow, int endColumn)
        {
            var endIndex = new TableIndex(endRow, endColumn);
            var view = new TableViewMock();
            var table = CreateTableWidget(view, rows, columns, visibleRows, visibleColumns);

            table.SelectCell(selectedRow, selectedColumn);

            view.Rows = newRows;
            view.Columns = newColumns;
            view.RaiseTableResizeEvent(newRows, newColumns);

            AssertIndex(endIndex, view.EndIndex.Value);
        }

        [TestCase(3, 2,    3, 2,    0, 0,    1, 2,    0, 0)]
        [TestCase(8, 8,    1, 2,    6, 4,    1, 8,    0, 3)]
        [TestCase(8, 8,    1, 2,    6, 4,    8, 1,    6, 0)]
        public void Resizehandler_RestrictedVisibleArea_StartIndexIsAdjusted(int rows, int columns,
            int visibleRows, int visibleColumns,
            int selectedRow, int selectedColumn,
            int newRows, int newColumns, int startRow, int startColumn)
        {
            var startIndex = new TableIndex(startRow, startColumn);
            var view = new TableViewMock();
            var table = CreateTableWidget(view, rows, columns, visibleRows, visibleColumns);

            table.SelectCell(selectedRow, selectedColumn);

            view.Rows = newRows;
            view.Columns = newColumns;
            view.RaiseTableResizeEvent(newRows, newColumns);

            Assert.True(view.StartIndex.HasValue);
            AssertIndex(startIndex, view.StartIndex.Value);
        }
        [TestCase(0, 0, 5, 5, 0, 0)]
        [TestCase(8, 3, 2, 1, 2, 1)]
        [TestCase(1, 5, 5, 5, 1, 5)]
        public void CreateTable_WithVisibleColumnsAndRows_EndIndexHasExpectedValue(int rows, int column, int visibleRows, int visibleColumns, int endRow, int endColumn)
        {
            var view = new TableViewMock();
            CreateTableWidget(view, rows, column, visibleRows, visibleColumns);
            var endIndex = new TableIndex(endRow, endColumn);

            Assert.True(view.EndIndex.HasValue);
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
            var tableViewMock = A.Fake<TableViewMock>(option => option.CallsBaseMethods());
            var table = CreateTableWidget(tableViewMock, rows, columns);

            table.SelectCell(selectedRow, selectedColumn);

            A.CallTo(() => tableViewMock.SetCellSelection(selectedRow, selectedColumn, true)).MustHaveHappened();

        }

        [TestCase(2, 3, 0, 0, CommandKeys.Right, 0, 1)]
        [TestCase(3, 2, 0, 0, CommandKeys.Down, 1, 0)]
        [TestCase(10, 10, 9, 9, CommandKeys.Up, 8, 9)]
        [TestCase(10, 10, 9, 9, CommandKeys.Left, 9, 8)]
        public void HandleInput_StartingFromSomeCell_TableViewSelectCellIsCalled(int rows, int columns, int selectedRow, int selectedColumn, 
                                          CommandKeys key, int newRow, int newColumns)
        {
            var tableViewMock = A.Fake<TableViewMock>(option => option.CallsBaseMethods());
            var table = CreateTableWidget(tableViewMock, rows, columns);
            table.SelectCell(selectedRow, selectedColumn);


            table.HandleInput(key);

            A.CallTo(() => tableViewMock.SetCellSelection(newRow, newColumns, true)).MustHaveHappened();
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
        [TestCase(10, 4, 9, 3, 12, 1, 9, 0)]
        public void Cursor_OnTableResize_CursorIsAdjusted(int rows, int columns, int selectedRow, int selectedColumn, 
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

        private static TableWidget<TestType> CreateTableWidget(TableViewMock view, int? visibleRows = null, int? visibleColumns = null)
        {

            var widget = new TableWidget<TestType>(visibleRows, visibleColumns, view);
            widget.SetCoordinates(10, 10, 500, 500);
            return widget;
        }
        private static void SetupView(TableViewMock tableViewMock, int rows, int columns)
        {
            tableViewMock.Rows = rows;
            tableViewMock.Columns = columns;
        }

        protected override IWidget CreateWidget()
        {

            return new TableWidget<TestType>(2, 2, new TableViewMock());
        }
    }
}
