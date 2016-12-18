using GameEngineTest.TestUtils;
using NUnit.Framework;
using System;
using FakeItEasy;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.TableView;
using GameEngine.Registry;

namespace GameEngineTest.Graphics
{
    [TestFixture]
    public class TableViewTest : IGraphicComponentTest
    {
        

        [TestCase]
        public void RowsColumns_ResizeModelBeforeSetup_ReturnsNewSize()
        {
            var modelStub = A.Fake<ITableModel<object>>();
            var table = CreateTable(modelStub);
            const int rows = 10;
            const int columns = 20;

            SetDimension(modelStub, rows, columns);
            modelStub.SizeChanged += Raise.With(new TableResizeEventArgs(rows, columns));

            Assert.AreEqual(rows, table.Rows);
            Assert.AreEqual(columns, table.Columns);
        }

        [TestCase]
        public void SetStartIndex_ResizeModelBeforeSetup_GetReturnsStartIndex()
        {
            var modelStub = A.Fake<ITableModel<object>>();
            var table = CreateTable(modelStub);
            const int rows = 10;
            const int columns = 20;
            var expectedStartIndex = new TableIndex(rows - 1, columns - 1);

            SetDimension(modelStub, rows, columns);
            modelStub.SizeChanged += Raise.With(new TableResizeEventArgs(rows, columns));

            table.StartIndex = expectedStartIndex;

            AssertIndexesAreEqual(expectedStartIndex, table.StartIndex);
        }

        [TestCase]
        public void SetEndIndex_ResizeModelBeforeSetup_GetReturnsEndIndex()
        {
            var modelStub = A.Fake<ITableModel<object>>();
            var table = CreateTable(modelStub);
            const int rows = 10;
            const int columns = 20;
            var expectedEndIndex = new TableIndex(rows, columns);

            SetDimension(modelStub, rows, columns);
            modelStub.SizeChanged += Raise.With(new TableResizeEventArgs(rows, columns));

            table.EndIndex = expectedEndIndex;

            AssertIndexesAreEqual(expectedEndIndex, table.EndIndex);
        }

        [TestCase]
        public void SelectionChanged_SelectionModelRaisesEventBeforeSetup_IsRaised()
        {
            var selectionModelStub = A.Fake<ITableSelectionModel>();
            var table = CreateTable(10, 20, selectionModelStub);
            var eventWasRaised = false;
            var eventArgs = new SelectionChangedEventArgs(5, 4, true);
            table.SelectionChanged += (obj, args) => eventWasRaised = true;

            selectionModelStub.SelectionChanged += Raise.With(selectionModelStub, eventArgs);

            Assert.True(eventWasRaised);
        }

        [TestCase]
        public void SelectionChanged_SelectionModelRaisesEventBeforeSetup_RightEventArgs()
        {
            var selectionModelStub = A.Fake<ITableSelectionModel>();
            var table = CreateTable(10, 20, selectionModelStub);
            var eventArgs = new SelectionChangedEventArgs(5, 4, true);
            SelectionChangedEventArgs returnedArgs = null;
            table.SelectionChanged += (obj, args) => returnedArgs = args;

            selectionModelStub.SelectionChanged += Raise.With(selectionModelStub, eventArgs);

            Assert.NotNull(returnedArgs);
            Assert.AreEqual(eventArgs.Row, returnedArgs.Row);
            Assert.AreEqual(eventArgs.Column, returnedArgs.Column);
            Assert.AreEqual(eventArgs.IsSelected, returnedArgs.IsSelected);
        }

        [TestCase]
        public void Draw_SetStartIndex_IndexIsHandedToTableGrid()
        {
            var grid = A.Fake<ITableGrid>();
            var table = CreateTable(10, 20, grid);
            var startIndex = new TableIndex(5, 10);
            table.StartIndex = startIndex;
            table.Setup();

            table.Draw();

            A.CallToSet(() => grid.StartIndex).To(startIndex).MustHaveHappened(Repeated.AtLeast.Once);
        }

        [TestCase]
        public void Draw_SetEndIndex_IndexIsHandedToTableGrid()
        {
            var grid = A.Fake<ITableGrid>();
            var table = CreateTable(10, 20, grid);
            var endIndex = new TableIndex(5, 10);
            table.EndIndex = endIndex;
            table.Setup();

            table.Draw();

            A.CallToSet(() => grid.EndIndex).To(endIndex).MustHaveHappened(Repeated.AtLeast.Once);
        }

        [TestCase(5, 4, 5, 0)]
        [TestCase(5, 4, 0, 4)]
        [TestCase(5, 4, 6, 0)]
        [TestCase(5, 4, 0, 5)]
        [TestCase(5, 4, -1, 0)]
        [TestCase(5, 4, 0, -1)]
        public void SetCellSelection_InvalidIndex_ThrowsOutOfBoundException(int rows, int columns, int selectedRow, int selectedColumn)
        {
            var table = CreateTable(rows, columns);

            Assert.Throws<ArgumentOutOfRangeException>(() => table.SetCellSelection(rows, columns, true));
        }

        [TestCase]
        public void Draw_ModelDataChangedRaised_RenderedComponentHasNewData()
        {
            var modelStub = A.Fake<ITableModel<object>>();
            var tableRendererMock = new TableRendererMock<object>();
            SetDimension(modelStub, 10, 20);
            var table = CreateTable(modelStub, tableRendererMock, A.Fake<ITableSelectionModel>());
            var newData = new object();
            const int row = 4;
            const int column = 5;
            var eventArgs = new DataChangedEventArgs<object>(row, column, newData);

            table.Setup();
            A.CallTo(() => modelStub.DataAt(row, column)).Returns(newData);
            modelStub.DataChanged += Raise.With(modelStub, eventArgs);
            table.Draw();

            var changedComponent = tableRendererMock.Components[row, column];
            Assert.AreEqual(newData, changedComponent.Data);
        }

        [TestCase]
        public void Draw_SelectionChangedRaised_RenderedComponentHasRightSelection()
        {
            var modelStub = A.Fake<ITableModel<object>>();
            var tableRendererMock = new TableRendererMock<object>();
            var selectionModelStub = A.Fake<ITableSelectionModel>();
            SetDimension(modelStub, 10, 20);
            var table = CreateTable(modelStub, tableRendererMock, selectionModelStub);
            const int row = 4;
            const int column = 5;
            var eventArgs = new SelectionChangedEventArgs(row, column, true);

            table.Setup();
            A.CallTo(() => selectionModelStub.IsSelected(row, column)).Returns(true);

            selectionModelStub.SelectionChanged += Raise.With(selectionModelStub, eventArgs);
            table.Draw();

            var changedComponent = tableRendererMock.Components[row, column];
            Assert.True(changedComponent.IsSelected);
        }

        [TestCase]
        public void DrawCalledTwice_ModelDataChangedRaised_RenderedComponentHasNewData()
        {
            var modelStub = A.Fake<ITableModel<object>>();
            var tableRendererMock = new TableRendererMock<object>();
            SetDimension(modelStub, 10, 20);
            var table = CreateTable(modelStub, tableRendererMock, A.Fake<ITableSelectionModel>());
            var newData = new object();
            const int row = 4;
            const int column = 5;
            var eventArgs = new DataChangedEventArgs<object>(row, column, newData);

            table.Setup();
            table.Draw();

            A.CallTo(() => modelStub.DataAt(row, column)).Returns(newData);
            modelStub.DataChanged += Raise.With(modelStub, eventArgs);
            table.Draw();

            var changedComponent = tableRendererMock.Components[row, column];
            Assert.AreEqual(newData, changedComponent.Data);
        }

        [TestCase]
        public void DrawCalledTwice_SelectionChangedRaised_RenderedComponentHasRightSelection()
        {
            var modelStub = A.Fake<ITableModel<object>>();
            var tableRendererMock = new TableRendererMock<object>();
            var selectionModelStub = A.Fake<ITableSelectionModel>();
            SetDimension(modelStub, 10, 20);
            var table = CreateTable(modelStub, tableRendererMock, selectionModelStub);
            const int row = 4;
            const int column = 5;
            var eventArgs = new SelectionChangedEventArgs(row, column, true);

            table.Setup();
            table.Draw(new SpriteBatchMock());

            A.CallTo(() => selectionModelStub.IsSelected(row, column)).Returns(true);
            selectionModelStub.SelectionChanged += Raise.With(selectionModelStub, eventArgs);
            table.Draw(new SpriteBatchMock());

            var changedComponent = tableRendererMock.Components[row, column];
            Assert.True(changedComponent.IsSelected);
        }

        [TestCase]
        public void SetCellSelection_ValidIndex_IsHandedToSelectionModel()
        {
            var selectionModelMock = A.Fake<ITableSelectionModel>();
            var table = CreateTable(10, 20, selectionModelMock);

            table.Setup();
            table.SetCellSelection(4, 6, true);

            A.CallTo(() => selectionModelMock.SelectIndex(4, 6)).MustHaveHappened(Repeated.AtLeast.Once);
        }

        private static void AssertIndexesAreEqual(TableIndex expectedStartIndex, TableIndex? testIndex)
        {
            Assert.NotNull(testIndex);
            Assert.AreEqual(expectedStartIndex.Column, testIndex.Value.Column);
            Assert.AreEqual(expectedStartIndex.Row, testIndex.Value.Row);
        }

        private static TableView<object> CreateTable(int rows, int columns, ITableGrid grid = null)
        {
            var selectionModelStub = A.Fake<ITableSelectionModel>();
            return CreateTable(rows, columns, selectionModelStub, grid);
        }
        private static TableView<object> CreateTable(int rows, int columns, ITableSelectionModel selectionModel, ITableGrid grid = null)
        {
            var modelStub = A.Fake<ITableModel<object>>();
            SetDimension(modelStub, rows, columns);
            return CreateTable(modelStub, new TableRendererMock<object>(), selectionModel, grid);
        }

        private static TableView<object> CreateTable(ITableModel<object> modelMock, ITableGrid grid = null)
        {
            return CreateTable(modelMock, new TableRendererMock<object>(), A.Fake<ITableSelectionModel>(), grid);
        }

        private static TableView<object> CreateTable(ITableModel<object> modelMock, TableRendererMock<object> renderer, ITableSelectionModel selectionModelMock, ITableGrid grid = null)
        {
            if (grid == null)
                grid = new TableGrid();
            var gameTypeRegistry = A.Fake<IGameTypeRegistry>();
            var table = new TableView<object>(modelMock, selectionModelMock, gameTypeRegistry, grid)
            {
                Factory = renderer.GetComponent
            };
            table.SetCoordinates(0, 0, 500, 500);
            return table;
        }

        private static void SetDimension(ITableModel<object> modelMock, int rows, int columns)
        {
            A.CallTo(() => modelMock.Rows).Returns(rows);
            A.CallTo(()=> modelMock.Columns).Returns(columns);
        }

        protected override IGraphicComponent CreateComponent()
        {
            var modelMock = A.Fake<ITableModel<object>>();
            var selectionModelMock = A.Fake<ITableSelectionModel>();
            var renderer = new TableRendererMock<object>();
            var gameTypeRegistry = A.Fake<IGameTypeRegistry>();
            var table = new TableView<object>(modelMock, selectionModelMock, gameTypeRegistry){Factory = renderer.GetComponent};
            table.Setup();
            return table;
        }
    }

}