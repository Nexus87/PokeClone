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
        Mock<ITableView<TestType>> tableViewMock = new Mock<ITableView<TestType>>();
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
        };

        public static List<TestCaseData> ViewPortInputTestData = new List<TestCaseData>
        {
            new TestCaseData(15, 15, 2, 1, new TableIndex(3, 2), CommandKeys.Left, new TableIndex(3, 1), new TableIndex(4, 1)),
            new TestCaseData(15, 15, 2, 1, new TableIndex(3, 2), CommandKeys.Right, new TableIndex(3, 3), new TableIndex(4, 3)),
            new TestCaseData(15, 15, 2, 1, new TableIndex(3, 2), CommandKeys.Up, new TableIndex(2, 2), new TableIndex(2, 2)),
            new TestCaseData(15, 15, 2, 1, new TableIndex(3, 2), CommandKeys.Down, new TableIndex(3, 2), new TableIndex(4, 2)),
        };

        [SetUp]
        public void Setup()
        {
            table = new TableWidget<TestType>(new PokeEngine());
            table.SetCoordinates(10, 10, 500, 500);
        }

        [TestCase]
        public void ForwardingPropertiesTest()
        {
            tableViewMock.SetupSet(o => o.Model = modelMock.Object).Verifiable();

            table.TableView = tableViewMock.Object;
            table.Model = modelMock.Object;

            tableViewMock.VerifyAll();

        }

        [TestCase]
        public void ForwardingCoordinatesTest()
        {
            float x = 0, y = 0, width = 0, height = 0;

            tableViewMock.SetupSet(o => o.XPosition = It.IsAny<float>()).Callback<float>(X => x = X);
            tableViewMock.SetupSet(o => o.YPosition = It.IsAny<float>()).Callback<float>(Y => y = Y);
            tableViewMock.SetupSet(o => o.Width = It.IsAny<float>()).Callback<float>(Width => width = Width);
            tableViewMock.SetupSet(o => o.Height = It.IsAny<float>()).Callback<float>(Height => height = Height);

            table.TableView = tableViewMock.Object;

            table.Draw(new SpriteBatchMock());

            Assert.AreEqual(table.XPosition, x);
            Assert.AreEqual(table.YPosition, y);
            Assert.AreEqual(table.Width, width);
            Assert.AreEqual(table.Height, height);
        }

        [TestCaseSource("SelectCellTestData")]
        public void SelectCellTest(int rows, int columns, int selectedRow, int selectedColumn)
        {
            tableViewMock.Setup(o => o.SetCellSelection(selectedRow, selectedColumn, true)).Verifiable();
            SetupView(tableViewMock, rows, columns);

            table.TableView = tableViewMock.Object;

            table.SelectCell(selectedRow, selectedColumn);

            tableViewMock.VerifyAll();

        }

        [TestCaseSource("HandleInputTestData")]
        public void HandleInputTest(int rows, int columns, int selectedRow, int selectedColumn, 
                                          CommandKeys key, int newRow, int newColumns)
        {
            SetupTable(table, rows, columns);
            table.SelectCell(selectedRow, selectedColumn);

            tableViewMock.ResetCalls();
            tableViewMock.Setup(o => o.SetCellSelection(newRow, newColumns, true)).Verifiable();

            table.HandleInput(key);

            tableViewMock.VerifyAll();
        }

        [TestCaseSource("ViewPortSelectionTestData")]
        public void ViewPortSetCellSelectionTest(int rows, int columns, int visibleRows, int visibleColumns, 
            TableIndex selectedCell, TableIndex start, TableIndex end)
        {
            SetupTable(table, rows, columns);
            
            TableIndex mockStart = new TableIndex(-1, -1);
            TableIndex mockEnd = new TableIndex();

            tableViewMock.Setup(o => o.StartIndex)
                .Callback<TableIndex>(idx => mockStart = idx);
            tableViewMock.Setup(o => o.EndIndex)
                .Callback<TableIndex>(idx => mockEnd = idx);

            table.VisibleColumns = visibleColumns;
            table.VisibleRows = visibleRows;

            // Start cell is still (0, 0)
            AssertIndex(mockStart, 0, 0);
            AssertIndex(mockEnd, visibleRows, visibleColumns);


            table.SelectCell(selectedCell.Row, selectedCell.Column);

            AssertIndex(start, mockStart);
            AssertIndex(end, mockEnd);
        }

        [TestCaseSource("ViewPortInputTestData")]
        public void ViewPortInputTest(int rows, int columns, int visibleRows, int visibleColumns, TableIndex startSelection,
            CommandKeys key, TableIndex start, TableIndex end)
        {
            SetupTable(table, rows, columns);

            TableIndex mockStart = new TableIndex(-1, -1);
            TableIndex mockEnd = new TableIndex();

            tableViewMock.Setup(o => o.StartIndex)
                .Callback<TableIndex>(idx => mockStart = idx);
            tableViewMock.Setup(o => o.EndIndex)
                .Callback<TableIndex>(idx => mockEnd = idx);

            table.SelectCell(startSelection.Row, startSelection.Column);
            table.HandleInput(key);

            AssertIndex(start, mockStart);
            AssertIndex(end, mockEnd);
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
        private void SetupTable(TableWidget<TestType> table, int rows, int columns)
        {
            SetupView(tableViewMock, rows, columns);
            table.TableView = tableViewMock.Object;
        }

        private void SetupView(Mock<ITableView<TestType>> tableViewMock, int rows, int columns)
        {
            tableViewMock.Setup(o => o.Rows).Returns(rows);
            tableViewMock.Setup(o => o.Columns).Returns(columns);
        }
    }
}
