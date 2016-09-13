using GameEngine;
using GameEngine.Graphics;
using GameEngine.Utils;
using GameEngineTest.TestUtils;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngineTest.Graphics
{
    [TestFixture]
    public class TableGridTest
    {

        const int ROWS = 5;
        const int COLUMNS = 5;
        const float X = 10.0f;
        const float Y = 5.0f;
        const float WIDTH = 200.0f;
        const float HEIGHT = 200.0f;

        [TestCase]
        public void Draw_BasicSetup_IsInContraints()
        {
            var grid = CreateDefaultGrid();
            var components = FillGrid(grid);

            grid.Draw();

            foreach (var component in components)
                component.IsInConstraints(X, Y, WIDTH, HEIGHT);

        }

        [TestCase]
        public void Draw_BasicSetup_XCoordinateAscendWithColumns()
        {
            var grid = CreateDefaultGrid();
            var components = FillGrid(grid);

            grid.Draw();

            for (int row = 0; row < grid.Rows; row++ )
                ColumnsXAscending(components, row);
        }

        private void ColumnsXAscending(Table<SelectableGraphicComponentMock> components, int row)
        {
            for (int column = 0; column < components.Columns - 1; column++)
                Assert.LessOrEqual(components[row, column].XPosition, components[row, column + 1].XPosition);
        }

        [TestCase]
        public void Draw_BasicSetup_YCoordinateAscendWithRows()
        {
            var grid = CreateDefaultGrid();
            var components = FillGrid(grid);

            grid.Draw();

            for (int column = 0; column < grid.Columns; column++)
                RowsYAscending(components, column);
        }

        private void RowsYAscending(Table<SelectableGraphicComponentMock> components, int column)
        {
            for (int row = 0; row < components.Rows - 1; row++)
                Assert.LessOrEqual(components[row, column].YPosition, components[row + 1, column].YPosition);
        }

        [TestCase]
        public void Draw_SetStartIndex_DrawnComponentsLimitedByIndex()
        {
            var startIndex = new TableIndex(2, 2);
            var grid = CreateDefaultGrid();
            var components = FillGrid(grid);

            grid.StartIndex = startIndex;
            grid.Draw();

            Assert.IsTrue(grid.StartIndex.HasValue);
            Assert.IsTrue(grid.EndIndex.HasValue);

            ValidateComponentsDrawn(components, grid.StartIndex.Value, grid.EndIndex.Value);
        }

        [TestCase]
        public void Draw_SetEndIndex_DrawnComponentsLimitedByIndex()
        {
            var endIndex = new TableIndex(2, 2);
            var grid = CreateDefaultGrid();
            var components = FillGrid(grid);

            grid.EndIndex = endIndex;
            grid.Draw();

            Assert.IsTrue(grid.StartIndex.HasValue);
            Assert.IsTrue(grid.EndIndex.HasValue);

            ValidateComponentsDrawn(components, grid.StartIndex.Value, grid.EndIndex.Value);
        }

        [TestCase]
        public void SetStartIndex_OnEmptyTable_DoesNotThrowAnException()
        {
            var grid = CreateGrid(0, 0);
            var startIndex = new TableIndex(0, 0);

            grid.StartIndex = startIndex;
        }

        private void ValidateComponentsDrawn(Table<SelectableGraphicComponentMock> components, TableIndex startIndex, TableIndex endIndex)
        {
            var drawnArea = components.CreateSubtable(startIndex, endIndex);
            foreach (var c in components)
                Assert.AreEqual(drawnArea.Contains(c), c.WasDrawn);
        }

        private bool IsBetween(int test, int lowerLimit, int upperLimit)
        {
            return lowerLimit <= test && test < upperLimit;
        }

        public static List<TestCaseData> StartIndexTestData = new List<TestCaseData>
        {
            new TestCaseData(new TableIndex(3, 3), 4, 4, new TableIndex(3, 3)),
            new TestCaseData(new TableIndex(1, 4), 4, 4, new TableIndex(1, 3)),
            new TestCaseData(new TableIndex(4, 1), 4, 4, new TableIndex(3, 1))
        };

        [TestCaseSource("StartIndexTestData")]
        public void StartIndex_RowColumnResize_ShrinksIfNeeded(TableIndex startIndex, int newRows, int newColumns, TableIndex expectedStartIndex)
        {
            var grid = CreateDefaultGrid();
            grid.StartIndex = startIndex;

            grid.Rows = newRows;
            grid.Columns = newColumns;

            Assert.IsTrue(grid.StartIndex.HasValue);
            AssertIndicesAreEqual(expectedStartIndex, grid.StartIndex.Value);
        }

        public static List<TestCaseData> EndIndexTestData = new List<TestCaseData>
        {
            new TestCaseData(new TableIndex(3, 3), 4, 4, new TableIndex(3, 3)),
            new TestCaseData(new TableIndex(1, 5), 4, 4, new TableIndex(1, 4)),
            new TestCaseData(new TableIndex(5, 1), 4, 4, new TableIndex(4, 1))
        };

        [TestCaseSource("EndIndexTestData")]
        public void EndIndexIndex_RowColumnResize_ShrinksIfNeeded(TableIndex endIndex, int newRows, int newColumns, TableIndex expectedEndIndex)
        {
            var grid = CreateDefaultGrid();
            grid.EndIndex = endIndex;

            grid.Rows = newRows;
            grid.Columns = newColumns;

            Assert.IsTrue(grid.EndIndex.HasValue);
            AssertIndicesAreEqual(expectedEndIndex, grid.EndIndex.Value);
        }

        [TestCase]
        public void StartIndex_Default_0_0()
        {
            TableIndex defaultStartIndex = new TableIndex(0, 0);
            var grid = new TableGrid(ROWS, COLUMNS);

            Assert.IsTrue(grid.StartIndex.HasValue);
            AssertIndicesAreEqual(defaultStartIndex, grid.StartIndex.Value);
        }

        [TestCase]
        public void EndIndex_Default_0_0()
        {
            TableIndex defaultEndIndex = new TableIndex(0, 0);
            var grid = new TableGrid(ROWS, COLUMNS);

            Assert.IsTrue(grid.EndIndex.HasValue);
            AssertIndicesAreEqual(defaultEndIndex, grid.EndIndex.Value);
        }

        [TestCase]
        public void Draw_EndIndexNull_DrawAll()
        {
            var grid = new TableGrid(ROWS, COLUMNS);
            var components = FillGrid(grid);

            grid.EndIndex = null;
            grid.Draw();

            foreach (var component in components)
                Assert.IsTrue(component.WasDrawn);
            
        }

        [TestCase]
        public void Draw_ResetStartToNull_DrawAll()
        {
            var grid = CreateDefaultGrid();
            var components = FillGrid(grid);

            grid.StartIndex = new TableIndex(ROWS - 1, COLUMNS - 1);
            grid.StartIndex = null;

            grid.Draw();

            foreach (var component in components)
                Assert.IsTrue(component.WasDrawn);
        }

        [TestCase]
        public void Draw_ResetEndToNotNull_DrawNothing()
        {
            var grid = new TableGrid(ROWS, COLUMNS);
            var components = FillGrid(grid);

            grid.EndIndex = null;
            grid.EndIndex = new TableIndex(0, 0);
            grid.Draw();

            foreach (var component in components)
                Assert.IsFalse(component.WasDrawn);

        }

        [TestCase]
        public void Draw_BasicSetup_DrawnComponentsHeigthFillsContraints()
        {
            var grid = CreateDefaultGrid();
            var components = FillGrid(grid);

            grid.Draw();

            for (int i = 0; i < grid.Columns; i++)
                Assert.AreEqual(HEIGHT, components.EnumerateRows(i).Sum(o => o.Height));
            
        }

        [TestCase]
        public void Draw_BasicSetup_DrawnComponentsWidthFillsContraints()
        {
            var grid = CreateDefaultGrid();
            var components = FillGrid(grid);

            grid.Draw();

            for (int i = 0; i < grid.Rows; i++)
                Assert.AreEqual(WIDTH, components.EnumerateColumns(i).Sum(o => o.Width));

        }

        public static List<TestCaseData> InvalidIndexes = new List<TestCaseData>
        {
            new TestCaseData(new TableIndex(-1, -1), null),
            new TestCaseData(null, new TableIndex(6, 6)),
            new TestCaseData(new TableIndex(4, 4), new TableIndex(3, 3))
        };

        [TestCaseSource("InvalidIndexes")]
        public void SetIndexes_InvalidData_ThrowsException(TableIndex? startIndex, TableIndex? endIndex)
        {
            var grid = CreateDefaultGrid();
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                grid.StartIndex = startIndex;
                grid.EndIndex = endIndex;
            });
        }

        private void AssertIndicesAreEqual(TableIndex expectedIndex, TableIndex tableIndex)
        {
            Assert.AreEqual(expectedIndex.Row, tableIndex.Row);
            Assert.AreEqual(expectedIndex.Column, tableIndex.Column);
        }

        private TableGrid CreateDefaultGrid()
        {
            var grid = CreateGrid();
            grid.SetCoordinates(X, Y, WIDTH, HEIGHT);
            return grid;
        }

        private Table<SelectableGraphicComponentMock> FillGrid(TableGrid grid)
        {
            var components = new Table<SelectableGraphicComponentMock>();

            for (int row = 0; row < grid.Rows; row++)
            {
                for (int column = 0; column < grid.Columns; column++)
                {
                    var component = new SelectableGraphicComponentMock();
                    components[row, column] = component;
                    grid.SetComponentAt(row, column, component);
                }
            }

            return components;
        }

        private TableGrid CreateGrid()
        {
            return CreateGrid(ROWS, COLUMNS);
        }

        private TableGrid CreateGrid(int rows, int columns)
        {
            var grid = new TableGrid();
            grid.Rows = rows;
            grid.Columns = columns;

            grid.StartIndex = new TableIndex(0, 0);
            grid.EndIndex = new TableIndex(rows, columns);

            return grid;
        }

       
    }
}
