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
        public class CellData
        {
            public int row;
            public int column;
            public int custom;

            public CellData(int row, int column, int custom = 0)
            {
                this.row = row;
                this.column = column;
                this.custom = custom;
            }
        }

        public static List<TestCaseData> ResizeIndices = new List<TestCaseData>
        {
            new TestCaseData(1, 3, null, null, 
                             2, 1, null, null),

            new TestCaseData(2, 2, null, new TableIndex(1, 1), 
                             1, 2, null, new TableIndex(0, 1)),

            new TestCaseData(5, 5, new TableIndex(4, 4), new TableIndex(4, 4), 
                             4, 4, new TableIndex(3, 3), new TableIndex(3, 3)),

            new TestCaseData(10, 4, new TableIndex(9, 3), null,
                              9, 3, new TableIndex(8, 2), null)
        };
        public static List<TestCaseData> ResizeData = new List<TestCaseData>
        {
            new TestCaseData(2, 2, 3, 3),
            new TestCaseData(2, 1, 4, 5),
            new TestCaseData(1, 2, 5, 4),
            new TestCaseData(0, 0, 3, 5),
            new TestCaseData(5, 4, 1, 2),
            new TestCaseData(4, 5, 3, 1)
        };

        public static List<TestCaseData> InvalidSelectionIndices = new List<TestCaseData>
        {
            new TestCaseData(5, 5, -1, 0),
            new TestCaseData(5, 5, 0, -1),
            new TestCaseData(5, 5, -1, -1),
            new TestCaseData(5, 5, 5, 0),
            new TestCaseData(5, 5, 0, 5),
            new TestCaseData(5, 5, 5, 5),
        };

        public static List<TestCaseData> SelectionIndices = new List<TestCaseData>
        {
            new TestCaseData(5, 5, 1, 1),
            new TestCaseData(5, 5, 3, 4),
            new TestCaseData(3, 2, 1, 0)
        };

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
            new TestCaseData(5, 5, new TableIndex(2, 2), new TableIndex(4, 4)),
            new TestCaseData(5, 5, new TableIndex(1, 2), new TableIndex(4, 5))
        };

        public static List<TestCaseData> RemoveTestData = new List<TestCaseData>
        {
            new TestCaseData(5, 5, 0, true),
            new TestCaseData(5, 5, 0, false),
            new TestCaseData(5, 5, 4, true),
            new TestCaseData(5, 5, 4, false),
            new TestCaseData(5, 5, 3, true),
            new TestCaseData(5, 5, 3, false),
            new TestCaseData(1, 1, 0, true),
            new TestCaseData(1, 1, 0, false),
        };

        public static List<TestCaseData> InvalidIndices = new List<TestCaseData>
        {
            new TestCaseData(5, 5, new TableIndex(-1, -1), null),
            new TestCaseData(5, 5, null, new TableIndex(5, 5)),
            new TestCaseData(5, 5, new TableIndex(4, 4), new TableIndex(3, 3))
        };

        public static List<TestCaseData> ChangeIndexesTestData = new List<TestCaseData>
        {
            new TestCaseData(10, 12, null, null, new TableIndex(4, 2), new TableIndex(6, 3)),
            new TestCaseData(12, 8, null, new TableIndex(4, 2), null, new TableIndex(7, 5)),
            new TestCaseData(10, 1, new TableIndex(0, 0), new TableIndex(7, 0), new TableIndex(1, 0), new TableIndex(8, 0))
        };

        private TableRendererMock<CellData> renderer;
        private Mock<ITableSelectionModel> selectionModelMock;
        private Mock<ITableModel<CellData>> modelMock;
        private TableView<CellData> table;


  
        [SetUp]
        public void Setup()
        {
            modelMock = new Mock<ITableModel<CellData>>();
            selectionModelMock = new Mock<ITableSelectionModel>();
            renderer = new TableRendererMock<CellData>();

            var componentTestModelMock = new Mock<ITableModel<CellData>>();
            SetDimension(componentTestModelMock, 5, 5);

            
        }

        [TestCaseSource("ChangeIndexesTestData")]
        public void ChangeIndexesTest(int rows, int columns, TableIndex? startIndex, TableIndex? endIndex, TableIndex? newStart, TableIndex? newEnd)
        {
            SetDimension(modelMock, rows, columns);
            table = CreateTable(modelMock, renderer, selectionModelMock);

            table.StartIndex = startIndex;
            table.EndIndex = endIndex;

            table.Draw(new SpriteBatchMock());

            table.StartIndex = newStart;
            table.EndIndex = newEnd;

            foreach (var c in renderer.components)
                c.WasDrawn = false;

            table.Draw(new SpriteBatchMock());

            CheckDrawnComponents(rows, columns, newStart, newEnd);
        }
        [TestCaseSource("ResizeData")]
        public void SetModelTest(int rows, int columns, int newRows, int newColumns)
        {
            SetDimension(modelMock, rows, columns);
            table = CreateTable(modelMock, renderer, selectionModelMock);

            modelMock.Setup(o => o[It.IsAny<int>(), It.IsAny<int>()])
                .Returns(new CellData(-1, -1, -1));

            modelMock.Setup(o => o.DataAt(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new CellData(-1, -1, -1));

            table.Draw(new SpriteBatchMock());

            renderer.Reset();

            // Set the new model
            var newModel = new Mock<ITableModel<CellData>>();
            SetDimension(newModel, newRows, newColumns);

            newModel.Setup(o => o[It.IsAny<int>(), It.IsAny<int>()])
                .Returns(new CellData(1, 1, 1));

            newModel.Setup(o => o.DataAt(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new CellData(1, 1, 1));

            table.Model = newModel.Object;

            Assert.AreEqual(newRows, table.Rows);
            Assert.AreEqual(newColumns, table.Columns);

            table.Draw(new SpriteBatchMock());

            // Check if the table got his data from the new model
            Assert.AreEqual(newRows, renderer.entries.Rows());
            Assert.AreEqual(newColumns, renderer.entries.Columns());
            foreach (var entry in renderer.entries)
            {
                Assert.AreEqual(1, entry.custom);
            }
        }

        [TestCaseSource("ResizeData")]
        public void ResizeEventTest(int rows, int columns, int newRows, int newColumns)
        {
            SetDimension(modelMock, rows, columns);
            table = CreateTable(modelMock, renderer, selectionModelMock);
            TableResizeEventArgs args = null;

            table.OnTableResize += (obj, evArg) => args = evArg;

            modelMock.Raise(o => o.SizeChanged += null, modelMock.Object, new TableResizeEventArgs(newRows, newColumns));

            Assert.NotNull(args);
            Assert.AreEqual(newRows, args.Rows);
            Assert.AreEqual(newColumns, args.Columns);

        }

        [TestCaseSource("SelectionIndices")]
        public void SelectionChangedEventTest(int rows, int columns, int selectedRow, int selectedColumn)
        {
            SetDimension(modelMock, rows, columns);
            table = CreateTable(modelMock, renderer, selectionModelMock);
            SelectionChangedEventArgs arg = null;

            table.SelectionChanged += (obj, evArg) => arg = evArg;

            selectionModelMock.Raise(o => o.SelectionChanged += null, 
                selectionModelMock.Object, new SelectionChangedEventArgs(selectedRow, selectedColumn, true));

            Assert.NotNull(arg);
            Assert.AreEqual(selectedRow, arg.Row);
            Assert.AreEqual(selectedColumn, arg.Column);
            Assert.IsTrue(arg.IsSelected);

            arg = null;

            selectionModelMock.Raise(o => o.SelectionChanged += null,
                selectionModelMock.Object, new SelectionChangedEventArgs(selectedRow, selectedColumn, false));

            Assert.NotNull(arg);
            Assert.AreEqual(selectedRow, arg.Row);
            Assert.AreEqual(selectedColumn, arg.Column);
            Assert.IsFalse(arg.IsSelected);
        }

        [TestCaseSource("InvalidIndices")]
        public void InvalidIndicesTest(int rows, int column, TableIndex? startIndex, TableIndex? endIndex)
        {
            SetDimension(modelMock, rows, column);
            table = CreateTable(modelMock, renderer, selectionModelMock);

            Assert.Throws<ArgumentOutOfRangeException>(delegate
            {
                table.StartIndex = startIndex;
                table.EndIndex = endIndex;
            });
        }

        [TestCaseSource("SelectionIndices")]
        public void SelectionModelEventTest(int rows, int columns, int selectedRow, int selectedColumn)
        {
            SetDimension(modelMock, rows, columns);
            table = CreateTable(modelMock, renderer, selectionModelMock);

            table.Draw(new SpriteBatchMock());

            selectionModelMock.Setup(o => o.IsSelected(It.IsAny<int>(), It.IsAny<int>()))
                .Returns<int, int>((a, b) => a == selectedRow && b == selectedColumn);

            selectionModelMock.Raise(o => o.SelectionChanged += null, 
                selectionModelMock.Object, 
                new SelectionChangedEventArgs(selectedRow, selectedColumn, true)
                );

            table.Draw(new SpriteBatchMock());

            IterateComponents(0, 0, rows - 1, columns - 1, (data, isInBound) =>
            {
                int r = data.Row;
                int c = data.Column;
                bool isSelected = data.IsSelected;
                Assert.AreEqual(r == selectedRow && c == selectedColumn, isSelected);
            });
        }

        [TestCase]
        public void ForwardingSelectionModelReturnValue()
        {
            SetDimension(modelMock, 10, 10);
            table = CreateTable(modelMock, renderer, selectionModelMock);

            selectionModelMock.Setup(o => o.SelectIndex(It.IsAny<int>(), It.IsAny<int>()))
                .Returns<int, int>( (r,c) => r == 5 && c== 5);
            selectionModelMock.Setup(o => o.UnselectIndex(It.IsAny<int>(), It.IsAny<int>()))
                .Returns<int, int>( (r,c) => r == 5 && c== 5);

            Assert.True(table.SetCellSelection(5, 5, true));
            Assert.False(table.SetCellSelection(2, 2, true));

            Assert.True(table.SetCellSelection(5, 5, false));
            Assert.False(table.SetCellSelection(2, 2, false));
        }

        [TestCaseSource("SelectionIndices")]
        public void CellSelectionTest(int rows, int columns, int selectedRow, int selectedColumn)
        {
            SetDimension(modelMock, rows, columns);
            table = CreateTable(modelMock, renderer, selectionModelMock);

            selectionModelMock.Setup(o => o.IsSelected(It.IsAny<int>(), It.IsAny<int>()))
                .Returns<int, int>((a, b) => a == selectedRow && b == selectedColumn);

            table.Draw(new SpriteBatchMock());

            IterateComponents(0, 0, rows - 1, columns - 1, (data, isInBound) =>
            {
                int r = data.Row;
                int c = data.Column;
                bool isSelected = data.IsSelected;
                Assert.AreEqual(r == selectedRow && c == selectedColumn, isSelected);
            });
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
            modelMock.Setup(o => o[It.IsAny<int>(), It.IsAny<int>()])
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

            modelMock.Raise(o => o.SizeChanged += null, modelMock.Object, new TableResizeEventArgs(rows, columns));
            table.Draw(new SpriteBatchMock());

            CompareTables(rows, columns, data, renderer.entries);
        }

        [TestCaseSource("IndicesData")]
        public void IndicesDataTest(int rows, int columns, TableIndex? startIndex, TableIndex? endIndex) 
        {
            CellData[,] data = new CellData[rows, columns];
            FillTable(data);
            SetDimension(modelMock, rows, columns);

            modelMock.Setup(o => o.DataAt(It.IsAny<int>(), It.IsAny<int>()))
                .Returns<int, int>((row, column) => data[row, column]);
            modelMock.Setup(o => o[It.IsAny<int>(), It.IsAny<int>()])
                .Returns<int, int>((row, column) => data[row, column]);

            table = CreateTable(modelMock, renderer, selectionModelMock);
            table.StartIndex = startIndex;
            table.EndIndex = endIndex;

            table.Draw(new SpriteBatchMock());

            IterateComponents(rows, columns, startIndex, endIndex,
                (d, IsInBound) =>
                {
                    if (!IsInBound)
                        return;

                    Assert.AreEqual(data[d.Row, d.Column], d.Data);
                });

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

            var newMock = new Mock<ITableModel<CellData>>();
        }

        [TestCaseSource("SelectionIndices")]
        public void SimpleSelectionTest(int rows, int columns, int selectedRow, int selectedColumn)
        {
            SetDimension(modelMock, rows, columns);
            selectionModelMock = new Mock<ITableSelectionModel>();

            selectionModelMock.Setup(o => o.SelectIndex(selectedRow, selectedColumn)).Returns(true);
            table = CreateTable(modelMock, renderer, selectionModelMock); 
            
            table.SetCellSelection(selectedRow, selectedColumn, true);
            selectionModelMock.Verify(o => o.SelectIndex(selectedRow, selectedColumn), Times.Once);

            selectionModelMock.ResetCalls();
            selectionModelMock.Setup(o => o.UnselectIndex(selectedRow, selectedColumn)).Returns(true);

            table.SetCellSelection(selectedRow, selectedColumn, false);

            selectionModelMock.Verify(o => o.UnselectIndex(selectedRow, selectedColumn), Times.Once);
        }

        [TestCaseSource("InvalidSelectionIndices")]
        public void InvalidSelectionindicesTest(int rows, int columns, int selectedRow, int selectedColumn)
        {
            SetDimension(modelMock, rows, columns);
            table = CreateTable(modelMock, renderer, selectionModelMock);

            Assert.Throws<ArgumentOutOfRangeException>(delegate
            {
                table.SetCellSelection(selectedRow, selectedColumn, true);
            });

            Assert.Throws<ArgumentOutOfRangeException>(delegate
            {
                table.SetCellSelection(selectedRow, selectedColumn, false);
            });
        }

        [TestCaseSource("ResizeIndices")]
        public void IndicesNewModelBehavoirTest(int oldRows, int oldColumns, TableIndex? oldStart, TableIndex? oldEnd,
            int newRows, int newColumns, TableIndex? newStart, TableIndex? newEnd)
        {
            SetDimension(modelMock, oldRows, oldColumns);
            table = CreateTable(modelMock, renderer, selectionModelMock);
            table.StartIndex = oldStart;
            table.EndIndex = oldEnd;

            var newModel = new Mock<ITableModel<CellData>>();
            SetDimension(newModel, newRows, newColumns);
            table.Model = newModel.Object;

            AssertIndices(newStart, table.StartIndex);
            AssertIndices(newEnd, table.EndIndex);
        }

        [TestCaseSource("ResizeIndices")]
        public void IndicesResizeBehavoirTest(int oldRows, int oldColumns, TableIndex? oldStart, TableIndex? oldEnd,
            int newRows, int newColumns, TableIndex? newStart, TableIndex? newEnd)
        {
            SetDimension(modelMock, oldRows, oldColumns);
            table = CreateTable(modelMock, renderer, selectionModelMock);
            table.StartIndex = oldStart;
            table.EndIndex = oldEnd;

            SetDimension(modelMock, newRows, newColumns);
            modelMock.Raise(o => o.SizeChanged += null,
                modelMock.Object, new TableResizeEventArgs(newRows, newColumns));

            AssertIndices(newStart, table.StartIndex);
            AssertIndices(newEnd, table.EndIndex);
        }

        private static void AssertIndices(TableIndex? index1, TableIndex? index2)
        {
            Assert.AreEqual(index1.HasValue, index2.HasValue);
            
            // Both are null
            if (!index1.HasValue)
                return;

            var idx1 = index1.Value;
            var idx2 = index2.Value;

            Assert.AreEqual(idx1.Row, idx2.Row);
            Assert.AreEqual(idx1.Column, idx2.Column);
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
            modelMock.Raise(o => o.SizeChanged += null, modelMock.Object, new TableResizeEventArgs(rows, columns));

            AssertTableSize(rows, columns);

            rows--;
            columns--;

            SetDimension(modelMock, rows, columns);
            modelMock.Raise(o => o.SizeChanged += null, modelMock.Object, new TableResizeEventArgs(rows, columns));

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

            IterateComponents(0, 0, rows - 1, columns - 1,
                (data, isInBound) => Assert.AreEqual(isInBound, data.WasDrawn));
        }

        private void CheckDrawnComponents(int rows, int columns, TableIndex? startIndex, TableIndex? endIndex)
        {
            int startRow = startIndex.HasValue ? startIndex.Value.Row : 0;
            int startColumn = startIndex.HasValue ? startIndex.Value.Column : 0;

            int endRow = endIndex.HasValue ? endIndex.Value.Row : rows;
            int endColumn = endIndex.HasValue ? endIndex.Value.Column : columns;

            IterateComponents(startRow, startColumn, endRow, endColumn,
                (data, isInBound) => Assert.AreEqual(isInBound, data != null && data.WasDrawn));
        }

        private delegate void PredicateFunction(TableComponentMock<CellData> data, bool isInBound);
        private void IterateComponents(int rows, int columns, TableIndex? startIndex, TableIndex? endIndex, PredicateFunction predicate)
        {
            int startRow = startIndex.HasValue ? startIndex.Value.Row : 0;
            int startColumn = startIndex.HasValue ? startIndex.Value.Column : 0;

            int endRow = endIndex.HasValue ? endIndex.Value.Row : rows;
            int endColumn = endIndex.HasValue ? endIndex.Value.Column : columns;

            IterateComponents(startRow, startColumn, endRow, endColumn, predicate);
        }

        private void IterateComponents(int startRow, int startColumn, int endRow, int endColumn, PredicateFunction predicate)
        {
            var components = renderer.components;
            var rows = endRow - startRow ;
            var columns = endColumn - startColumn;
            // Assert that the table view has requested enough components
            Assert.GreaterOrEqual(components.Rows(), rows);
            Assert.GreaterOrEqual(components.Columns(), columns);

            //Assert that the right components have been drawn
            for (int row = 0; row < components.Rows(); row++)
            {
                for (int column = 0; column < components.Columns(); column++)
                {
                    bool isInBound = startRow <= row && row <= endRow
                        && startColumn <= column && column <= endColumn;
                    // Only components between columns and rows should be drawn.
                    predicate(components[row, column], isInBound);
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

        private void CheckDrawnArea(int startRow = 0, int startColumn = 0, int endRow = -1, int endColumn = -1)
        {
            var components = renderer.components;

            endRow = endRow == -1 ? components.Rows() : endRow;
            endColumn = endColumn == -1 ? components.Columns() : endColumn;

            float totalHeigth = 0;
            float totalWidth = 0;
            // The cells should fill the whole area of the table view

            // Accumulate the height of all component for every column
            for (int column = startColumn; column < endColumn; column++)
            {
                totalHeigth = 0;
                for (int row = startRow; row < endRow; row++)
                {
                    totalHeigth += components[row, column] != null ? components[row, column].Height : 0;
                }

                Assert.AreEqual(totalHeigth, table.Height);
            }

            // Accumulate the width of all component for every row
            for (int row = startRow; row < components.Rows(); row++)
            {
                totalWidth = 0;
                for (int column = startColumn; column < components.Columns(); column++)
                {
                    totalWidth += components[row, column] != null ? components[row, column].Width : 0;
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

            table.Draw(new SpriteBatchMock());

            CheckDrawnComponents(rows, columns, startIndex, endIndex);

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

            int endRow = endIndex.HasValue ? endIndex.Value.Row : rows;
            int endColumn = endIndex.HasValue ? endIndex.Value.Column : columns;

            table.Draw(new SpriteBatchMock());

            CheckDrawnArea(startRow, startColumn, endRow, endColumn);
        }

        private void AssertTableSize(int rows, int columns)
        {
            Assert.AreEqual(rows, table.Rows);
            Assert.AreEqual(columns, table.Columns);
        }

        private TableView<CellData> CreateTable(Mock<ITableModel<CellData>> modelMock, TableRendererMock<CellData> renderer, Mock<ITableSelectionModel> selectionModelMock)
        {
            var table = new TableView<CellData>(modelMock.Object, renderer, selectionModelMock.Object, gameMock.Object);
            table.SetCoordinates(0, 0, 500, 500);
            table.Setup();
            return table;
        }

        private static void SetDimension(Mock<ITableModel<CellData>> modelMock, int rows, int columns)
        {
            modelMock.Setup(o => o.Rows).Returns(rows);
            modelMock.Setup(o => o.Columns).Returns(columns);
        }

        protected override IGraphicComponent CreateComponent()
        {
            var table = new TableView<CellData>(modelMock.Object, renderer, selectionModelMock.Object, gameMock.Object);
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