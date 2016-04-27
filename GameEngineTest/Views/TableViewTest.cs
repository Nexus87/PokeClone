using GameEngine.Graphics.Views;
using GameEngine.Graphics;
using GameEngine.Wrapper;
using GameEngine.Utils;
using GameEngineTest.Util;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System;
using Microsoft.Xna.Framework.Content;

namespace GameEngineTest.Views
{
    [TestFixture]
    public class TableViewTest : IGraphicComponentTest
    {
        

        [TestCase]
        public void RowsColumns_ResizeModelBeforeSetup_ReturnsNewSize()
        {
            var modelStub = new Mock<ITableModel<Object>>();
            var table = CreateTable(modelStub);
            int rows = 10;
            int columns = 20;

            SetDimension(modelStub, rows, columns);
            modelStub.Raise(o => o.SizeChanged += null, new TableResizeEventArgs(rows, columns));

            Assert.AreEqual(rows, table.Rows);
            Assert.AreEqual(columns, table.Columns);
        }

        [TestCase]
        public void SetStartIndex_ResizeModelBeforeSetup_GetReturnsStartIndex()
        {
            var modelStub = new Mock<ITableModel<Object>>();
            var table = CreateTable(modelStub);
            int rows = 10;
            int columns = 20;
            var expectedStartIndex = new TableIndex(rows - 1, columns - 1);

            SetDimension(modelStub, rows, columns);
            modelStub.Raise(o => o.SizeChanged += null, new TableResizeEventArgs(rows, columns));

            table.StartIndex = expectedStartIndex;

            AssertIndexesAreEqual(expectedStartIndex, table.StartIndex);
        }

        [TestCase]
        public void SetEndIndex_ResizeModelBeforeSetup_GetReturnsEndIndex()
        {
            var modelStub = new Mock<ITableModel<Object>>();
            var table = CreateTable(modelStub);
            int rows = 10;
            int columns = 20;
            var expectedEndIndex = new TableIndex(rows, columns);

            SetDimension(modelStub, rows, columns);
            modelStub.Raise(o => o.SizeChanged += null, new TableResizeEventArgs(rows, columns));

            table.EndIndex = expectedEndIndex;

            AssertIndexesAreEqual(expectedEndIndex, table.EndIndex);
        }

        [TestCase]
        public void SelectionChanged_SelectionModelRaisesEventBeforeSetup_IsRaised()
        {
            var selectionModelStub = new Mock<ITableSelectionModel>();
            var table = CreateTable(10, 20, selectionModelStub);
            bool eventWasRaised = false;
            SelectionChangedEventArgs eventArgs = new SelectionChangedEventArgs(5, 4, true);
            table.SelectionChanged += (obj, args) => eventWasRaised = true;

            selectionModelStub.Raise(o => o.SelectionChanged += null, selectionModelStub.Object, eventArgs);

            Assert.True(eventWasRaised);
        }

        [TestCase]
        public void SelectionChanged_SelectionModelRaisesEventBeforeSetup_RightEventArgs()
        {
            var selectionModelStub = new Mock<ITableSelectionModel>();
            var table = CreateTable(10, 20, selectionModelStub);
            SelectionChangedEventArgs eventArgs = new SelectionChangedEventArgs(5, 4, true);
            SelectionChangedEventArgs returnedArgs = null;
            table.SelectionChanged += (obj, args) => returnedArgs = args;

            selectionModelStub.Raise(o => o.SelectionChanged += null, selectionModelStub.Object, eventArgs);

            Assert.NotNull(returnedArgs);
            Assert.AreEqual(eventArgs.Row, returnedArgs.Row);
            Assert.AreEqual(eventArgs.Column, returnedArgs.Column);
            Assert.AreEqual(eventArgs.IsSelected, returnedArgs.IsSelected);
        }

        [TestCase]
        public void Draw_SetStartIndex_IndexIsHandedToTableGrid()
        {
            var grid = new Mock<ITableGrid>();
            var table = CreateTable(10, 20, grid.Object);
            var startIndex = new TableIndex(5, 10);
            table.StartIndex = startIndex;
            table.Setup();

            table.Draw(new SpriteBatchMock());

            grid.VerifySet(o => o.StartIndex = startIndex, Times.AtLeastOnce);
        }

        [TestCase]
        public void Draw_SetEndIndex_IndexIsHandedToTableGrid()
        {
            var grid = new Mock<ITableGrid>();
            var table = CreateTable(10, 20, grid.Object);
            var endIndex = new TableIndex(5, 10);
            table.EndIndex = endIndex;
            table.Setup();

            table.Draw(new SpriteBatchMock());

            grid.VerifySet(o => o.EndIndex = endIndex, Times.AtLeastOnce);
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
            var modelStub = new Mock<ITableModel<Object>>();
            var tableRendererMock = new TableRendererMock<Object>();
            SetDimension(modelStub, 10, 20);
            var table = CreateTable(modelStub, tableRendererMock, new Mock<ITableSelectionModel>());
            var newData = new Object();
            int row = 4;
            int column = 5;
            var eventArgs = new DataChangedEventArgs<Object>(row, column, newData);

            table.Setup();
            modelStub.SetupGet(o => o[row, column] ).Returns(newData);
            modelStub.Setup(o => o.DataAt(row, column) ).Returns(newData);
            modelStub.Raise( o => o.DataChanged += null, modelStub.Object, eventArgs);
            table.Draw(new SpriteBatchMock());

            var changedComponent = tableRendererMock.components[row, column];
            Assert.AreEqual(newData, changedComponent.Data);
        }

        [TestCase]
        public void Draw_SelectionChangedRaised_RenderedComponentHasRightSelection()
        {
            var modelStub = new Mock<ITableModel<Object>>();
            var tableRendererMock = new TableRendererMock<Object>();
            var selectionModelStub = new Mock<ITableSelectionModel>();
            SetDimension(modelStub, 10, 20);
            var table = CreateTable(modelStub, tableRendererMock, selectionModelStub);
            int row = 4;
            int column = 5;
            var eventArgs = new SelectionChangedEventArgs(row, column, true);

            table.Setup();
            selectionModelStub.Setup(o => o.IsSelected(row, column)).Returns(true);
            selectionModelStub.Raise(o => o.SelectionChanged += null, selectionModelStub.Object, eventArgs);
            table.Draw(new SpriteBatchMock());

            var changedComponent = tableRendererMock.components[row, column];
            Assert.True(changedComponent.IsSelected);
        }

        [TestCase]
        public void DrawCalledTwice_ModelDataChangedRaised_RenderedComponentHasNewData()
        {
            var modelStub = new Mock<ITableModel<Object>>();
            var tableRendererMock = new TableRendererMock<Object>();
            SetDimension(modelStub, 10, 20);
            var table = CreateTable(modelStub, tableRendererMock, new Mock<ITableSelectionModel>());
            var newData = new Object();
            int row = 4;
            int column = 5;
            var eventArgs = new DataChangedEventArgs<Object>(row, column, newData);

            table.Setup();
            table.Draw(new SpriteBatchMock());

            modelStub.SetupGet(o => o[row, column]).Returns(newData);
            modelStub.Setup(o => o.DataAt(row, column)).Returns(newData);
            modelStub.Raise(o => o.DataChanged += null, modelStub.Object, eventArgs);
            table.Draw(new SpriteBatchMock());

            var changedComponent = tableRendererMock.components[row, column];
            Assert.AreEqual(newData, changedComponent.Data);
        }

        [TestCase]
        public void DrawCalledTwice_SelectionChangedRaised_RenderedComponentHasRightSelection()
        {
            var modelStub = new Mock<ITableModel<Object>>();
            var tableRendererMock = new TableRendererMock<Object>();
            var selectionModelStub = new Mock<ITableSelectionModel>();
            SetDimension(modelStub, 10, 20);
            var table = CreateTable(modelStub, tableRendererMock, selectionModelStub);
            int row = 4;
            int column = 5;
            var eventArgs = new SelectionChangedEventArgs(row, column, true);

            table.Setup();
            table.Draw(new SpriteBatchMock());

            selectionModelStub.Setup(o => o.IsSelected(row, column)).Returns(true);
            selectionModelStub.Raise(o => o.SelectionChanged += null, selectionModelStub.Object, eventArgs);
            table.Draw(new SpriteBatchMock());

            var changedComponent = tableRendererMock.components[row, column];
            Assert.True(changedComponent.IsSelected);
        }

        [TestCase]
        public void SetCellSelection_ValidIndex_IsHandedToSelectionModel()
        {
            var selectionModelMock = new Mock<ITableSelectionModel>();
            var table = CreateTable(10, 20, selectionModelMock);

            table.Setup();
            table.SetCellSelection(4, 6, true);

            selectionModelMock.Verify(o => o.SelectIndex(4, 6), Times.AtLeastOnce);
        }

        private void AssertIndexesAreEqual(TableIndex expectedStartIndex, TableIndex? testIndex)
        {
            Assert.NotNull(testIndex);
            Assert.AreEqual(expectedStartIndex.Column, testIndex.Value.Column);
            Assert.AreEqual(expectedStartIndex.Row, testIndex.Value.Row);
        }

        private TableView<Object> CreateTable(int rows, int columns, ITableGrid grid = null)
        {
            var selectionModelStub = new Mock<ITableSelectionModel>();
            return CreateTable(rows, columns, selectionModelStub, grid);
        }
        private TableView<Object> CreateTable(int rows, int columns, Mock<ITableSelectionModel> selectionModel, ITableGrid grid = null)
        {
            var modelStub = new Mock<ITableModel<Object>>();
            SetDimension(modelStub, rows, columns);
            return CreateTable(modelStub, new TableRendererMock<Object>(), selectionModel, grid);
        }

        private TableView<Object> CreateTable(Mock<ITableModel<Object>> modelMock, ITableGrid grid = null)
        {
            return CreateTable(modelMock, new TableRendererMock<Object>(), new Mock<ITableSelectionModel>(), grid);
        }

        private TableView<Object> CreateTable(Mock<ITableModel<Object>> modelMock, TableRendererMock<Object> renderer, Mock<ITableSelectionModel> selectionModelMock, ITableGrid grid = null)
        {
            if (grid == null)
                grid = new TableGrid(gameStub);

            var table = new TableView<Object>(modelMock.Object, renderer, selectionModelMock.Object, grid, gameStub);
            table.SetCoordinates(0, 0, 500, 500);
            return table;
        }

        private static void SetDimension(Mock<ITableModel<Object>> modelMock, int rows, int columns)
        {
            modelMock.Setup(o => o.Rows).Returns(rows);
            modelMock.Setup(o => o.Columns).Returns(columns);
        }

        protected override IGraphicComponent CreateComponent()
        {
            var modelMock = new Mock<ITableModel<Object>>();
            var selectionModelMock = new Mock<ITableSelectionModel>();
            var renderer = new TableRendererMock<Object>();
            var table = new TableView<Object>(modelMock.Object, renderer, selectionModelMock.Object, gameStub);
            table.Setup();
            return table;
        }
    }

    internal class SpriteFontMock : ISpriteFont
    {
        public ISpriteFont spriteFont;
        public Mock<ISpriteFont> spriteMock = new Mock<ISpriteFont>();

        public SpriteFontMock()
        {
            spriteMock.SetupMeasureString();
            spriteFont = spriteMock.Object;
        }

        public System.Collections.ObjectModel.ReadOnlyCollection<char> Characters { get { return spriteFont.Characters; } }
        public char? DefaultCharacter { get { return spriteFont.DefaultCharacter; } set { spriteFont.DefaultCharacter = value; } }
        public int LineSpacing { get { return spriteFont.LineSpacing; } set { spriteFont.LineSpacing = value; } }
        public float Spacing { get { return spriteFont.Spacing; } set { spriteFont.Spacing = value; } }
        public Microsoft.Xna.Framework.Graphics.SpriteFont SpriteFont { get { return spriteFont.SpriteFont; } }

        public Microsoft.Xna.Framework.Graphics.Texture2D Texture
        {
            get { return spriteFont.Texture; }
        }

        public void Load(ContentManager content, string fontName)
        {
            spriteFont.LoadContent();
        }

        public Microsoft.Xna.Framework.Vector2 MeasureString(StringBuilder text)
        {
            return spriteFont.MeasureString(text);
        }

        public Microsoft.Xna.Framework.Vector2 MeasureString(string text)
        {
            return spriteFont.MeasureString(text);
        }


        public void LoadContent()
        {
        }
    }
}