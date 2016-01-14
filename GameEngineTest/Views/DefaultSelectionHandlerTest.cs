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

        [SetUp]
        public void Setup()
        {
            testObj = new DefaultSelectionHandler();

            viewMock = new Mock<IItemView>();
            
            viewMock.SetupGet(o => o.Columns).Returns(10);
            viewMock.SetupGet(o => o.Rows).Returns(10);
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

            testObj.Init(viewMock.Object);
        }

        [TestCase]
        public void InitialSelectionTest()
        {
            Assert.AreEqual(0, testObj.SelectedColumn);
            Assert.AreEqual(0, testObj.SelectedRow);
        }

        [TestCase]
        public void HandleInputTest()
        {
            Assert.AreEqual(0, testObj.SelectedColumn);
            Assert.AreEqual(0, testObj.SelectedRow);

            testObj.HandleInput(Keys.Right);

            Assert.AreEqual(1, testObj.SelectedColumn);
            Assert.AreEqual(0, testObj.SelectedRow);

            testObj.HandleInput(Keys.Down);

            Assert.AreEqual(1, testObj.SelectedColumn);
            Assert.AreEqual(1, testObj.SelectedRow);

            testObj.HandleInput(Keys.Left);
            testObj.HandleInput(Keys.Up);

            Assert.AreEqual(0, testObj.SelectedColumn);
            Assert.AreEqual(0, testObj.SelectedRow);

            testObj.HandleInput(Keys.Left);
            testObj.HandleInput(Keys.Up);

            Assert.AreEqual(0, testObj.SelectedColumn);
            Assert.AreEqual(0, testObj.SelectedRow);

            for (int i = 1; i < viewMock.Object.Rows; i++)
            {
                testObj.HandleInput(Keys.Down);
                Assert.AreEqual(0, testObj.SelectedColumn);
                Assert.AreEqual(i, testObj.SelectedRow);
            }

            for (int i = 1; i < viewMock.Object.Columns; i++)
            {
                testObj.HandleInput(Keys.Right);
                Assert.AreEqual(i, testObj.SelectedColumn);
                Assert.AreEqual(viewMock.Object.Rows - 1, testObj.SelectedRow);
            }

            testObj.HandleInput(Keys.Down);
            Assert.AreEqual(viewMock.Object.Columns - 1, testObj.SelectedColumn);
            Assert.AreEqual(viewMock.Object.Rows - 1, testObj.SelectedRow);

            testObj.HandleInput(Keys.Right);
            Assert.AreEqual(viewMock.Object.Columns - 1, testObj.SelectedColumn);
            Assert.AreEqual(viewMock.Object.Rows - 1, testObj.SelectedRow);
        }

        [TestCase]
        public void EventTest()
        {
            bool itemSelectedCalled = false;
            bool closeCalled = false;
            bool selectionChangedCalled = false;

            testObj.ItemSelected += delegate { itemSelectedCalled = true;};
            testObj.CloseRequested += delegate { closeCalled = true; };
            testObj.SelectionChanged += delegate { selectionChangedCalled = true; };

            Assert.AreEqual(0, testObj.SelectedColumn);
            Assert.AreEqual(0, testObj.SelectedRow);

            testObj.HandleInput(Keys.Left);
            Assert.AreEqual(0, testObj.SelectedColumn);
            Assert.AreEqual(0, testObj.SelectedRow);
            Assert.False(selectionChangedCalled);

            testObj.HandleInput(Keys.Up);
            Assert.AreEqual(0, testObj.SelectedColumn);
            Assert.AreEqual(0, testObj.SelectedRow);
            Assert.False(selectionChangedCalled);

            Assert.False(itemSelectedCalled);
            Assert.False(closeCalled);

            testObj.HandleInput(Keys.Right);
            Assert.True(selectionChangedCalled);

            selectionChangedCalled = false;

            testObj.HandleInput(Keys.Down);
            Assert.True(selectionChangedCalled);
            selectionChangedCalled = false;

            for (int i = 2; i < viewMock.Object.Rows; i++)
            {
                testObj.HandleInput(Keys.Down);
                Assert.True(selectionChangedCalled, "Error on step " + i);
                selectionChangedCalled = false;
            }

            for (int i = 2; i < viewMock.Object.Columns; i++)
            {
                testObj.HandleInput(Keys.Right);
                Assert.True(selectionChangedCalled, "Error on step " + i);
                selectionChangedCalled = false;
            }

            Assert.AreEqual(viewMock.Object.Columns - 1, testObj.SelectedColumn);
            Assert.AreEqual(viewMock.Object.Rows - 1, testObj.SelectedRow);
            Assert.False(selectionChangedCalled);

            testObj.HandleInput(Keys.Down);
            Assert.False(selectionChangedCalled);

            testObj.HandleInput(Keys.Right);
            Assert.False(selectionChangedCalled);

            testObj.HandleInput(Keys.Enter);
            Assert.True(itemSelectedCalled);
            Assert.False(closeCalled);

            itemSelectedCalled = false;

            testObj.HandleInput(Keys.Escape);
            Assert.True(closeCalled);
            Assert.False(itemSelectedCalled);
        }

        [TestCase]
        public void ChangeViewportTest()
        {
            while (testObj.SelectedColumn < viewMock.Object.ViewportColumns - 1)
            {
                testObj.HandleInput(Keys.Right);
                Assert.AreEqual(0, startColumn);
            }

            testObj.HandleInput(Keys.Right);
            Assert.AreEqual(viewMock.Object.ViewportColumns, testObj.SelectedColumn);
            Assert.AreEqual(1, startColumn);

            testObj.HandleInput(Keys.Right);
            Assert.AreEqual(viewMock.Object.ViewportColumns + 1, testObj.SelectedColumn);
            Assert.AreEqual(2, startColumn);

            while (testObj.SelectedColumn > startColumn)
            {
                testObj.HandleInput(Keys.Left);
                Assert.AreEqual(2, startColumn);
            }

            testObj.HandleInput(Keys.Left);
            Assert.AreEqual(1, startColumn);

            testObj.HandleInput(Keys.Left);
            Assert.AreEqual(0, startColumn);

            // Same for rows
            while (testObj.SelectedRow < viewMock.Object.ViewportRows - 1)
            {
                testObj.HandleInput(Keys.Down);
                Assert.AreEqual(0, startRow);
            }

            testObj.HandleInput(Keys.Down);
            Assert.AreEqual(viewMock.Object.ViewportRows, testObj.SelectedRow);
            Assert.AreEqual(1, startRow);

            testObj.HandleInput(Keys.Down);
            Assert.AreEqual(viewMock.Object.ViewportRows + 1, testObj.SelectedRow);
            Assert.AreEqual(2, startRow);

            while (testObj.SelectedRow > startRow)
            {
                testObj.HandleInput(Keys.Up);
                Assert.AreEqual(2, startRow);
            }

            testObj.HandleInput(Keys.Up);
            Assert.AreEqual(1, startRow);

            testObj.HandleInput(Keys.Up);
            Assert.AreEqual(0, startRow);
        }

        public static List<TestCaseData> TableSize = new List<TestCaseData>
        {
            new TestCaseData(3, 3, 3, 3),
            new TestCaseData(10, 3, 5, 3),
            new TestCaseData(3, 10, 3, 5),
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

            viewMock.SetupGet(o => o.Columns).Returns(column);
            viewMock.SetupGet(o => o.Rows).Returns(row);
            viewMock.Raise(o => o.OnTableResize += null, viewMock.Object, new TableResizeEventArgs(row, column));

            Assert.AreEqual(selectedColumn, testObj.SelectedColumn);
            Assert.AreEqual(selectedRow, testObj.SelectedRow);


        }
    }
}
