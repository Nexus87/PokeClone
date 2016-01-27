using GameEngine.Graphics.Views;
using Microsoft.Xna.Framework.Input;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Views
{
    [TestFixture]
    public class DefaultSelectionHandlerTest
    {
        private DefaultSelectionHandler testObj;
        private Mock<IItemView> viewMock;
        private int startColumn = 0;
        private int startRow = 0;

        private const int Rows = 10;
        private const int Columns = 10;

        [SetUp]
        public void Setup()
        {
            testObj = new DefaultSelectionHandler();

            viewMock = new Mock<IItemView>();
            
            viewMock.SetupGet(o => o.Columns).Returns(Columns);
            viewMock.SetupGet(o => o.Rows).Returns(Rows);
            viewMock.SetupGet(o => o.ViewportColumns).Returns(2);
            viewMock.SetupGet(o => o.ViewportRows).Returns(2);
            viewMock.SetupGet(o => o.ViewportStartColumn).Returns(startColumn);
            viewMock.SetupSet(o => o.ViewportStartColumn = It.IsAny<int>()).Callback<int>(i =>
            {
                startColumn = i;
                viewMock.SetupGet(o => o.ViewportStartColumn).Returns(startColumn);
            });
            viewMock.SetupGet(o => o.ViewportStartRow).Returns(startRow);
            viewMock.SetupSet(o => o.ViewportStartRow = It.IsAny<int>()).Callback<int>(i => 
            {
                startRow = i;
                viewMock.SetupGet(o => o.ViewportStartRow).Returns(startRow);
            });
            viewMock.Setup(o => o.SetCellSelection(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>())).Returns(true);
            testObj.Init(viewMock.Object);
        }

        [TestCase]
        public void InitialSelectionTest()
        {
            Assert.AreEqual(0, testObj.SelectedColumn);
            Assert.AreEqual(0, testObj.SelectedRow);
            testObj.Update();

            viewMock.Verify(o => o.SetCellSelection(0, 0, true), Times.Once);
        }

        [TestCase]
        public void EmptyTableTest()
        {
            viewMock.Setup(o => o.Rows).Returns(0);
            viewMock.Setup(o => o.Columns).Returns(0);
            // Verify, that row and columns are always >= 0
            viewMock.Setup(o => o.SetCellSelection(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>()))
                .Callback<int, int, bool>((a, b, c) => { Assert.GreaterOrEqual(a, 0); Assert.GreaterOrEqual(b, 0); });
            viewMock.ResetCalls();


            testObj.Init(viewMock.Object);
            testObj.Update();
            viewMock.Verify(o => o.SetCellSelection(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>()), Times.Never);

            Assert.AreEqual(-1, testObj.SelectedRow);
            Assert.AreEqual(-1, testObj.SelectedColumn);

            viewMock.Setup(o => o.Rows).Returns(1);
            viewMock.Setup(o => o.Columns).Returns(1);

            viewMock.Raise(o => o.OnTableResize += null, viewMock.Object, new TableResizeEventArgs(1, 1));

            Assert.AreEqual(0, testObj.SelectedRow);
            Assert.AreEqual(0, testObj.SelectedColumn);
            testObj.Update();

            viewMock.Verify(o => o.SetCellSelection(0, 0, true), Times.AtLeastOnce);
            viewMock.ResetCalls();

            viewMock.Setup(o => o.Rows).Returns(0);
            viewMock.Setup(o => o.Columns).Returns(0);
            viewMock.Raise(o => o.OnTableResize += null, viewMock.Object, new TableResizeEventArgs(0, 0));

            Assert.AreEqual(-1, testObj.SelectedRow);
            Assert.AreEqual(-1, testObj.SelectedColumn);
            
            testObj.Update();
            viewMock.Verify(o => o.SetCellSelection(It.IsAny<int>(), It.IsAny<int>(), true), Times.Never);
        }

        public static List<TestCaseData> HandleInputTestData = new List<TestCaseData>
        {
            new TestCaseData(0, 0, Keys.Right, 0, 1),
            new TestCaseData(0, 0, Keys.Down, 1, 0),
            new TestCaseData(1, 1, Keys.Left, 1, 0),
            new TestCaseData(1, 1, Keys.Up, 0, 1),
            new TestCaseData(Rows - 1, 0, Keys.Down, Rows - 1, 0),
            new TestCaseData(0, Columns - 1, Keys.Right, 0, Columns - 1),
            new TestCaseData(0, 0, Keys.Up, 0, 0),
            new TestCaseData(0, 0, Keys.Left, 0, 0)
        };

        [TestCaseSource("HandleInputTestData")]
        public void HandleInputTest(int row, int column, Keys key, int newRow, int newColumn)
        {
            testObj.SelectedRow = row;
            testObj.SelectedColumn = column;
            testObj.Update();

            viewMock.Verify(o => o.SetCellSelection(row, column, true), Times.Once);
            Assert.AreEqual(column, testObj.SelectedColumn);
            Assert.AreEqual(row, testObj.SelectedRow);

            viewMock.ResetCalls();
            testObj.HandleInput(key);
            testObj.Update();

            Assert.AreEqual(newColumn, testObj.SelectedColumn);
            Assert.AreEqual(newRow, testObj.SelectedRow);

            if (newColumn != column || newRow != row)
            {
                viewMock.Verify(o => o.SetCellSelection(row, column, false), Times.Once);
                viewMock.Verify(o => o.SetCellSelection(newRow, newColumn, true), Times.Once);
            }
        }

        [TestCase]
        public void EventTest()
        {
            bool itemSelectedCalled = false;
            bool closeCalled = false;
            bool selectionChangedCalled = false;

            testObj.Update();

            testObj.ItemSelected += delegate { itemSelectedCalled = true;};
            testObj.CloseRequested += delegate { closeCalled = true; };
            testObj.SelectionChanged += delegate { selectionChangedCalled = true; };
            
            Assert.AreEqual(0, testObj.SelectedColumn);
            Assert.AreEqual(0, testObj.SelectedRow);

            testObj.HandleInput(Keys.Left);
            testObj.Update();
            Assert.AreEqual(0, testObj.SelectedColumn);
            Assert.AreEqual(0, testObj.SelectedRow);
            Assert.False(selectionChangedCalled);

            testObj.HandleInput(Keys.Up);
            testObj.Update();
            Assert.AreEqual(0, testObj.SelectedColumn);
            Assert.AreEqual(0, testObj.SelectedRow);
            Assert.False(selectionChangedCalled);

            Assert.False(itemSelectedCalled);
            Assert.False(closeCalled);

            testObj.HandleInput(Keys.Right);
            testObj.Update();
            Assert.True(selectionChangedCalled);

            selectionChangedCalled = false;

            testObj.HandleInput(Keys.Down);
            testObj.Update();
            Assert.True(selectionChangedCalled);
            selectionChangedCalled = false;

            for (int i = 2; i < viewMock.Object.Rows; i++)
            {
                testObj.HandleInput(Keys.Down);
                testObj.Update();
                Assert.True(selectionChangedCalled, "Error on step " + i);
                selectionChangedCalled = false;
            }

            for (int i = 2; i < viewMock.Object.Columns; i++)
            {
                testObj.HandleInput(Keys.Right);
                testObj.Update();
                Assert.True(selectionChangedCalled, "Error on step " + i);
                selectionChangedCalled = false;
            }

            Assert.AreEqual(viewMock.Object.Columns - 1, testObj.SelectedColumn);
            Assert.AreEqual(viewMock.Object.Rows - 1, testObj.SelectedRow);
            Assert.False(selectionChangedCalled);

            testObj.HandleInput(Keys.Down);
            testObj.Update();
            Assert.False(selectionChangedCalled);

            testObj.HandleInput(Keys.Right);
            testObj.Update();
            Assert.False(selectionChangedCalled);

            testObj.HandleInput(Keys.Enter);
            testObj.Update();
            Assert.True(itemSelectedCalled);
            Assert.False(closeCalled);

            itemSelectedCalled = false;

            testObj.HandleInput(Keys.Escape);
            testObj.Update();
            Assert.True(closeCalled);
            Assert.False(itemSelectedCalled);
        }

        [TestCase]
        public void ChangeViewportTest()
        {
            while (testObj.SelectedColumn < viewMock.Object.ViewportColumns - 1)
            {
                testObj.HandleInput(Keys.Right);
                testObj.Update();
                Assert.AreEqual(0, startColumn);
            }

            testObj.HandleInput(Keys.Right);
            testObj.Update();
            Assert.AreEqual(viewMock.Object.ViewportColumns, testObj.SelectedColumn);
            Assert.AreEqual(1, startColumn);

            testObj.HandleInput(Keys.Right);
            testObj.Update();
            Assert.AreEqual(viewMock.Object.ViewportColumns + 1, testObj.SelectedColumn);
            Assert.AreEqual(2, startColumn);

            while (testObj.SelectedColumn > startColumn)
            {
                testObj.HandleInput(Keys.Left);
                testObj.Update();
                Assert.AreEqual(2, startColumn);
            }

            testObj.HandleInput(Keys.Left);
            testObj.Update();
            Assert.AreEqual(1, startColumn);

            testObj.HandleInput(Keys.Left);
            testObj.Update();
            Assert.AreEqual(0, startColumn);

            // Same for rows
            while (testObj.SelectedRow < viewMock.Object.ViewportRows - 1)
            {
                testObj.HandleInput(Keys.Down);
                testObj.Update();
                Assert.AreEqual(0, startRow);
            }

            testObj.HandleInput(Keys.Down);
            testObj.Update();
            Assert.AreEqual(viewMock.Object.ViewportRows, testObj.SelectedRow);
            Assert.AreEqual(1, startRow);

            testObj.HandleInput(Keys.Down);
            testObj.Update();
            Assert.AreEqual(viewMock.Object.ViewportRows + 1, testObj.SelectedRow);
            Assert.AreEqual(2, startRow);

            while (testObj.SelectedRow > startRow)
            {
                testObj.HandleInput(Keys.Up);
                testObj.Update();
                Assert.AreEqual(2, startRow);
            }

            testObj.HandleInput(Keys.Up);
            testObj.Update();
            Assert.AreEqual(1, startRow);

            testObj.HandleInput(Keys.Up);
            testObj.Update();
            Assert.AreEqual(0, startRow);
        }

        public static List<TestCaseData> TableSize = new List<TestCaseData>
        {
            new TestCaseData(3, 3, 2, 2),
            new TestCaseData(10, 3, 5, 2),
            new TestCaseData(3, 10, 2, 5),
            new TestCaseData(20, 20, 5, 5),
            new TestCaseData(6, 6, 5, 5),
        };

        [TestCaseSource("TableSize")]
        public void ResizeTest(int row, int column, int selectedRow, int selectedColumn)
        {
            Assert.AreEqual(0, testObj.SelectedColumn);
            Assert.AreEqual(0, testObj.SelectedRow);

            testObj.SelectedRow = 5;
            testObj.SelectedColumn = 5;

            Assert.AreEqual(5, testObj.SelectedColumn);
            Assert.AreEqual(5, testObj.SelectedRow);
            testObj.Update();

            viewMock.SetupGet(o => o.Columns).Returns(column);
            viewMock.SetupGet(o => o.Rows).Returns(row);

            viewMock.Raise(o => o.OnTableResize += null, viewMock.Object, new TableResizeEventArgs(row, column));
            testObj.Update();

            Assert.AreEqual(selectedColumn, testObj.SelectedColumn);
            Assert.AreEqual(selectedRow, testObj.SelectedRow);

            if (selectedRow != 5 || selectedColumn != 5)
            {
                viewMock.Verify(o => o.SetCellSelection(5, 5, false), Times.Once);
                viewMock.Verify(o => o.SetCellSelection(selectedRow, selectedColumn, true), Times.Once);
            }



        }
    }
}
