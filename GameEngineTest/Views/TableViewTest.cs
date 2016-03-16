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

namespace GameEngineTest.Views
{
    [TestFixture]
    public class TableViewTest : IGraphicComponentTest
    {
        class CellData
        {
            public int row;
            public int column;

            public CellData(int row, int column)
            {
                this.row = row;
                this.column = column;
            }
        }

        public static List<TestCaseData> ModelSizes = new List<TestCaseData>
        {
            new TestCaseData(0, 0),
            new TestCaseData(10, 0),
            new TestCaseData(0, 10),
            new TestCaseData(5, 5)
        };

        public static List<TestCaseData> TableSizes = new List<TestCaseData>
        {
            new TestCaseData(1, 1),
            new TestCaseData(5, 1),
            new TestCaseData(1, 5),
            new TestCaseData(5, 5)
        };

        public static List<TestCaseData> IndicesData = new List<TestCaseData>
        {
            new TestCaseData(5, 5, null, new TableIndex(4, 4)),
            new TestCaseData(5, 5, new TableIndex(2, 2), null),
            new TestCaseData(5, 5, null, null),
            new TestCaseData(5, 5, new TableIndex(2, 2,), new TableIndex(4, 4))
        };

        private TableRendererMock<CellData> renderer;
        private Mock<ITableSelectionModel> selectionModelMock;
        private Mock<IItemModel<CellData>> modelMock;
        private TableView<CellData> table;


  
        [SetUp]
        public void Setup()
        {
            contentMock.SetupLoad();
            
            modelMock = new Mock<IItemModel<CellData>>();
            selectionModelMock = new Mock<ITableSelectionModel>();
            renderer = new TableRendererMock<CellData>();

            var componentTestModelMock = new Mock<IItemModel<CellData>>();
            SetDimension(componentTestModelMock, 5, 5);

            testObj = CreateTable(componentTestModelMock, renderer, selectionModelMock);
        }

        [TestCaseSource("RemoveTestData")]
        public void ResizeModelDataTest(int rows, int columns, int index, bool isColumnRemoved)
        {
            CellData[,] data = new CellData[rows, columns];
            FillTable(data);

            SetDimension(modelMock, rows, columns);
            table = CreateTable(modelMock, renderer, selectionModelMock);

            modelMock.Setup(o => o.DataAt(It.IsAny<int>(), It.IsAny<int>()))
                .Returns<int, int>((a, b) => data[a, b]);

            table.Draw(new SpriteBatchMock());
            CompareTables(rows, columns, data, renderer.entries);

            if (isColumnRemoved)
                columns--;
            else
                rows--;

            SetDimension(modelMock, rows, columns);

            if (isColumnRemoved)
            {
                for(int i = index; i < columns; i++)
                {
                    for (int j = 0; j < rows; j++)
                        data[j, i] = data[j, i + 1];
                }
            }
            else
            {
                for (int i = index; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                        data[i, j] = data[i + 1, j];
                }
            }

            modelMock.Raise(o => o.SizeChanged += null, modelMock.Object, new SizeChangedEventArgs(rows, columns));
            table.Draw(new SpriteBatchMock());

            CompareTables(rows, columns, data, renderer.entries);


        }

        private void CompareTables(int rows, int columns, CellData[,] data, CellData[,] entries)
        {
            for(int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                    Assert.AreEqual(data[i, j], entries[i, j]);
            }
        }

        private static void FillTable(CellData[,] data)
        {
            for (int i = 0; i < data.Rows(); i++)
            {
                for (int j = 0; j < data.Columns(); j++)
                    data[i, j] = new CellData(i, j);
            }
        }

        [TestCaseSource("ModelSizes")]
        public void RowsColumnsPropertyTest(int rows, int columns)
        {
            SetDimension(modelMock, rows, columns);

            table = CreateTable(modelMock, renderer, selectionModelMock);

            AssertTableSize(rows, columns);

            var newMock = new Mock<IItemModel<CellData>>();
            SetDimension(newMock, rows + 1, columns + 1);

            table.Model = newMock.Object;

            AssertTableSize(rows + 1, columns + 1);
        }

        [TestCaseSource("ModelSizes")]
        public void RowsColumnsOnModelChangedTest(int rows, int columns)
        {
            SetDimension(modelMock, rows, columns);

            table = CreateTable(modelMock, renderer, selectionModelMock);

            AssertTableSize(rows, columns);

            rows++;
            columns++;

            SetDimension(modelMock, rows, columns);
            modelMock.Raise(o => o.SizeChanged += null, modelMock.Object, new SizeChangedEventArgs(rows, columns));

            AssertTableSize(rows, columns);

            rows--;
            columns--;

            SetDimension(modelMock, rows, columns);
            modelMock.Raise(o => o.SizeChanged += null, modelMock.Object, new SizeChangedEventArgs(rows, columns));

            AssertTableSize(rows, columns);
        }

        [TestCaseSource("ModelSizes")]
        public void IndicesDefaultValues(int rows, int columns)
        {
            SetDimension(modelMock, rows, columns);
            table = CreateTable(modelMock, renderer, selectionModelMock);

            Assert.IsNull(table.StartIndex);
            Assert.IsNull(table.EndIndex);
        }

        [TestCaseSource("TableSizes")]
        public void SimpleDrawTest(int rows, int columns)
        {
            SetDimension(modelMock, rows, columns);
            table = CreateTable(modelMock, renderer, selectionModelMock);

            table.Draw(new SpriteBatchMock());

            CheckDrawnComponents(rows, columns);
        }

        private void CheckDrawnComponents(int rows, int columns)
        {
            CheckDrawnComponents(rows, columns, 0, 0, rows - 1, columns - 1);
        }

        private void CheckDrawnComponents(int rows, int columns, int startRow, int startColumn, int endRow, int endColumn)
        {
            var components = renderer.components;
            // Assert that the table view has requested enough components
            Assert.GreaterOrEqual(components.Columns(), rows);
            Assert.GreaterOrEqual(components.Columns(), columns);

            //Assert that the right components have been drawn
            for (int row = 0; row < components.Rows(); row++)
            {
                for (int column = 0; column < components.Columns(); column++)
                {
                    bool isInBound = startRow <= row && row <= endRow
                        && startColumn <= column && column <= endColumn;
                    // Only components between columns and rows should be drawn.
                    Assert.AreEqual(isInBound, components[row, column].WasDrawn);
                }
            }
        }

        [TestCaseSource("TableSizes")]
        public void SimpleDrawnAreaTest(int rows, int columns)
        {
            SetDimension(modelMock, rows, columns);
            table = CreateTable(modelMock, renderer, selectionModelMock);

            table.Draw(new SpriteBatchMock());

            CheckDrawnArea();

        }

        private void CheckDrawnArea()
        {
            var components = renderer.components;

            float totalHeigth = 0;
            float totalWidth = 0;
            // The cells should fill the whole area of the table view

            // Accumulate the height of all component for every column
            for (int column = 0; column < components.Columns(); column++)
            {
                for (int row = 0; row < components.Rows(); row++)
                {
                    totalHeigth += components[row, column].Height;
                }

                Assert.AreEqual(totalHeigth, table.Height);
            }

            // Accumulate the width of all component for every row
            for (int row = 0; row < components.Rows(); row++)
            {
                for (int column = 0; column < components.Columns(); column++)
                {
                    totalWidth += components[row, column].Width;
                }

                Assert.AreEqual(totalWidth, table.Width);
            }
        }

        [TestCaseSource("IndicesData")]
        public void SimpleStartEndIndicesTest(int rows, int columns, TableIndex? startIndex, TableIndex? endIndex)
        {
            SetDimension(modelMock, rows, columns);
            table = CreateTable(modelMock, renderer, selectionModelMock);

            table.StartIndex = startIndex;
            table.EndIndex = endIndex;

            int startRow = startIndex.HasValue ? startIndex.Value.Row : 0;
            int startColumn = startIndex.HasValue ? startIndex.Value.Column : 0;

            int endRow = endIndex.HasValue ? endIndex.Value.Row : rows - 1;
            int endColumn = endIndex.HasValue ? endIndex.Value.Column : columns - 1;

            table.Draw(new SpriteBatchMock());

            CheckDrawnComponents(rows, columns, startRow, startColumn, endRow, endColumn);

        }

        [TestCaseSource("IndicesData")]
        public void StartEndIndicesAreaTest(int rows, int columns, TableIndex? startIndex, TableIndex? endIndex)
        {
            SetDimension(modelMock, rows, columns);
            table = CreateTable(modelMock, renderer, selectionModelMock);

            table.StartIndex = startIndex;
            table.EndIndex = endIndex;

            int startRow = startIndex.HasValue ? startIndex.Value.Row : 0;
            int startColumn = startIndex.HasValue ? startIndex.Value.Column : 0;

            int endRow = endIndex.HasValue ? endIndex.Value.Row : rows - 1;
            int endColumn = endIndex.HasValue ? endIndex.Value.Column : columns - 1;

            table.Draw(new SpriteBatchMock());

            CheckDrawnArea();
        }

        private void AssertTableSize(int rows, int columns)
        {
            Assert.AreEqual(rows, table.Rows);
            Assert.AreEqual(columns, table.Columns);
        }

        private TableView<CellData> CreateTable(Mock<IItemModel<CellData>> modelMock, TableRendererMock<CellData> renderer, Mock<ITableSelectionModel> selectionModelMock)
        {
            var table = new TableView<CellData>(modelMock.Object, renderer, selectionModelMock.Object, gameMock.Object);
            table.SetCoordinates(0, 0, 500, 500);
            return table;
        }

        private static void SetDimension(Mock<IItemModel<CellData>> modelMock, int rows, int columns)
        {
            modelMock.Setup(o => o.Rows).Returns(rows);
            modelMock.Setup(o => o.Columns).Returns(columns);
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

        public void Load(Microsoft.Xna.Framework.Content.ContentManager content, string fontName)
        {
            spriteFont.Load(content, fontName);
        }

        public Microsoft.Xna.Framework.Vector2 MeasureString(StringBuilder text)
        {
            return spriteFont.MeasureString(text);
        }

        public Microsoft.Xna.Framework.Vector2 MeasureString(string text)
        {
            return spriteFont.MeasureString(text);
        }
    }
}