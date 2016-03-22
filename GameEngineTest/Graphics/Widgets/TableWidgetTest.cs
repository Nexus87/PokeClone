using GameEngine;
using GameEngine.Graphics.Views;
using GameEngine.Graphics.Widgets;
using GameEngineTest.Views;
using GameEngineTest.Util;
using GameEngine.Graphics;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Graphics.Widgets
{
    [TestFixture]
    public class TableWidgetTest
    {
        Mock<TableViewMock> tableViewMock = new Mock<TableViewMock>();
        Mock<ITableModel<TestType>> modelMock = new Mock<ITableModel<TestType>>();
        Mock<ITableRenderer<TestType>> rendererMock = new Mock<ITableRenderer<TestType>>();

        TableWidget<TestType> table;

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
            new TestCaseData(15, 15, 2, 1, new TableIndex(3, 2), CommandKeys.Left, new TableIndex(3, 1), new TableIndex(4, 1)),
            new TestCaseData(15, 15, 2, 1, new TableIndex(3, 2), CommandKeys.Right, new TableIndex(3, 3), new TableIndex(4, 3)),
            new TestCaseData(15, 15, 2, 1, new TableIndex(3, 2), CommandKeys.Up, new TableIndex(2, 2), new TableIndex(3, 2)),
            new TestCaseData(15, 15, 2, 1, new TableIndex(3, 2), CommandKeys.Down, new TableIndex(3, 2), new TableIndex(4, 2)),
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

        [SetUp]
        public void Setup()
        {
            tableViewMock = new Mock<TableViewMock> { CallBase = true };
            modelMock = new Mock<ITableModel<TestType>>();
            rendererMock = new Mock<ITableRenderer<TestType>>();
        }

        [TestCase]
        public void ZeroSizedTable()
        {
            table = CreateTableWidget(tableViewMock, 0, 0);
        }

        [TestCaseSource("ResizeTableTestData")]
        public void ResizeTableTest(int rows, int columns, int visibleRows, int visibleColumns, TableIndex? selectedIndex, 
            int newRows, int newColumns, TableIndex startIdx)
        {
            var endIdx = new TableIndex();
            endIdx.Row = Math.Min(startIdx.Row + visibleRows - 1, newRows - 1);
            endIdx.Column = Math.Min(startIdx.Column + visibleColumns - 1, newColumns - 1);
            table = CreateTableWidget(tableViewMock, rows, columns, visibleRows, visibleColumns);
            
            if(selectedIndex != null)
                table.SelectCell(selectedIndex.Value.Row, selectedIndex.Value.Column);

            var view = tableViewMock.Object;
            view.Rows = newRows;
            view.Columns = newColumns;

            view.RaiseTableResizeEvent(newRows, newColumns);

            AssertIndex(startIdx, view.StartIndex.Value);
            AssertIndex(endIdx, view.EndIndex.Value);
        }
        [TestCase]
        public void ForwardingPropertiesTest()
        {
            table = CreateTableWidget(tableViewMock);
            tableViewMock.SetupSet(o => o.Model = modelMock.Object).Verifiable();

            table.Model = modelMock.Object;

            tableViewMock.Verify();

        }

        [TestCaseSource("InvalidSelectionData")]
        public void InvalidSelectionTest(int rows, int columns, int selectedRow, int selectedColumn)
        {
            table = CreateTableWidget(tableViewMock, rows, columns);

            Assert.Throws<ArgumentOutOfRangeException>(delegate { table.SelectCell(selectedRow, selectedColumn); });
        }

        [TestCase]
        public void ForwardingCoordinatesTest()
        {
            table = CreateTableWidget(tableViewMock);

            table.Draw(new SpriteBatchMock());

            var tableMock = tableViewMock.Object;

            Assert.AreEqual(table.XPosition, tableMock.XPosition);
            Assert.AreEqual(table.YPosition, tableMock.YPosition);
            Assert.AreEqual(table.Width, tableMock.Width);
            Assert.AreEqual(table.Height, tableMock.Height);
        }

        [TestCaseSource("SelectCellTestData")]
        public void SelectCellTest(int rows, int columns, int selectedRow, int selectedColumn)
        {
            table = CreateTableWidget(tableViewMock, rows, columns);
            tableViewMock.Setup(o => o.SetCellSelection(selectedRow, selectedColumn, true)).Verifiable();

            table.SelectCell(selectedRow, selectedColumn);

            tableViewMock.Verify();

        }

        [TestCaseSource("HandleInputTestData")]
        public void HandleInputTest(int rows, int columns, int selectedRow, int selectedColumn, 
                                          CommandKeys key, int newRow, int newColumns)
        {
            table = CreateTableWidget(tableViewMock, rows, columns);
            table.SelectCell(selectedRow, selectedColumn);

            tableViewMock.ResetCalls();
            tableViewMock.Setup(o => o.SetCellSelection(newRow, newColumns, true)).Verifiable();

            table.HandleInput(key);

            tableViewMock.Verify();
        }

        [TestCaseSource("ViewPortSelectionTestData")]
        public void ViewPortSetCellSelectionTest(int rows, int columns, int visibleRows, int visibleColumns, 
            TableIndex selectedCell, TableIndex start, TableIndex end)
        {

            table = CreateTableWidget(tableViewMock, rows, columns, visibleRows, visibleColumns);
            var view = tableViewMock.Object;   

            // Changes might only be applied in the Update method.
            // So we call Draw to make sure, that it is called.
            table.Draw(new SpriteBatchMock());
            // Start cell is still (0, 0)
            AssertIndex(view.StartIndex.Value, 0, 0);
            AssertIndex(view.EndIndex.Value, visibleRows -1 , visibleColumns - 1);


            table.SelectCell(selectedCell.Row, selectedCell.Column);
            table.Draw(new SpriteBatchMock());

            AssertIndex(start, view.StartIndex.Value);
            AssertIndex(end, view.EndIndex.Value);
        }

        [TestCaseSource("ViewPortInputTestData")]
        public void ViewPortInputTest(int rows, int columns, int visibleRows, int visibleColumns, TableIndex startSelection,
            CommandKeys key, TableIndex start, TableIndex end)
        {
            table = CreateTableWidget(tableViewMock, rows, columns, visibleRows, visibleColumns);
            var view = tableViewMock.Object;

            table.SelectCell(startSelection.Row, startSelection.Column);
            table.HandleInput(key);

            AssertIndex(start, view.StartIndex.Value);
            AssertIndex(end, view.EndIndex.Value);
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

        private TableWidget<TestType> CreateTableWidget(Mock<TableViewMock> view, int rows, int columns, int? visibleRows = null, int? visibleColumns = null)
        {
            SetupView(view, rows, columns);
            return CreateTableWidget(view, visibleRows, visibleColumns);
        }

        private TableWidget<TestType> CreateTableWidget(Mock<TableViewMock> view, int? visibleRows = null, int? visibleColumns = null)
        {
            
            var widget = new TableWidget<TestType>(visibleRows, visibleColumns, view.Object, new PokeEngine());
            widget.SetCoordinates(10, 10, 500, 500);
            return widget;
        }
        private void SetupView(Mock<TableViewMock> tableViewMock, int rows, int columns)
        {
            tableViewMock.Object.Rows = rows;
            tableViewMock.Object.Columns = columns;
        }
    }
}
