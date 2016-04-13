using GameEngine.Graphics;
using GameEngine.Graphics.Views;
using GameEngineTest.Util;
using GameEngine.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Graphics
{
    [TestFixture]
    public class TableGridTest
    {
        static readonly int ROWS = 5;
        static readonly int COLUMNS = 5;
        static readonly float X = 10.0f;
        static readonly float Y = 5.0f;
        static readonly float WIDTH = 200.0f;
        static readonly float HEIGHT = 200.0f;

        [TestCase]
        public void Draw_BasicSetup_IsInContraints()
        {
            var grid = CreateDefaultGrid();
            var components = FillGrid(grid);

            grid.Draw(new SpriteBatchMock());

            foreach (var component in components)
                component.IsInConstraints(X, Y, WIDTH, HEIGHT);

        }

        [TestCase]
        public void Draw_BasicSetup_XCoordinateAscendWithColumns()
        {
            var grid = CreateDefaultGrid();
            var components = FillGrid(grid);

            grid.Draw(new SpriteBatchMock());

            for (int row = 0; row < grid.Rows; row++ )
                ColumnsXAscending(components, row);
        }

        private void ColumnsXAscending(SelectableGraphicComponentMock[,] components, int row)
        {
            for (int column = 0; column < components.Columns() - 1; column++)
                Assert.LessOrEqual(components[row, column].XPosition, components[row, column + 1].XPosition);
        }

        [TestCase]
        public void Draw_BasicSetup_YCoordinateAscendWithRows()
        {
            var grid = CreateDefaultGrid();
            var components = FillGrid(grid);

            grid.Draw(new SpriteBatchMock());

            for (int column = 0; column < grid.Columns; column++)
                RowsYAscending(components, column);
        }

        private void RowsYAscending(SelectableGraphicComponentMock[,] components, int column)
        {
            for (int row = 0; row < components.Rows() - 1; row++)
                Assert.LessOrEqual(components[row, column].YPosition, components[row + 1, column].YPosition);
        }

        [TestCase]
        public void Draw_SetStartIndex_DrawnComponentsLimitedByIndex()
        {
            var startIndex = new TableIndex(2, 2);
            var grid = CreateDefaultGrid();
            var components = FillGrid(grid);

            grid.StartIndex = startIndex;
            grid.Draw(new SpriteBatchMock());

            ValidateComponentsDrawn(components, grid.StartIndex, grid.EndIndex);
        }

        [TestCase]
        public void Draw_SetEndIndex_DrawnComponentsLimitedByIndex()
        {
            var endIndex = new TableIndex(2, 2);
            var grid = CreateDefaultGrid();
            var components = FillGrid(grid);

            grid.EndIndex = endIndex;
            grid.Draw(new SpriteBatchMock());

            ValidateComponentsDrawn(components, grid.StartIndex, grid.EndIndex);
        }

        private void ValidateComponentsDrawn(SelectableGraphicComponentMock[,] components, TableIndex startIndex, TableIndex endIndex)
        {
            for (int row = 0; row < components.Rows(); row++)
            {
                for (int column = 0; column < components.Columns(); column++)
                {
                    bool shouldBeDrawn = IsBetween(row, startIndex.Row, endIndex.Row) &&
                        IsBetween(column, startIndex.Column, endIndex.Column);

                    Assert.AreEqual(shouldBeDrawn, components[row, column].WasDrawn);
                }
            }
        }

        private bool IsBetween(int test, int lowerLimit, int upperLimit)
        {
            return lowerLimit <= test && test <= upperLimit;
        }

        
        [TestCase(new TableIndex(3, 3), 4, 4, new TableIndex(3, 3))]
        [TestCase(new TableIndex(1, 5), 4, 4, new TableIndex(1, 4))]
        [TestCase(new TableIndex(5, 1), 4, 4, new TableIndex(4, 1))]
        private void StartIndex_RowColumnResize_ShrinksIfNeeded(TableIndex startIndex, int newRows, int newColumns, TableIndex expectedStartIndex)
        {
            var grid = CreateDefaultGrid();
            grid.StartIndex = startIndex;

            grid.Rows = newRows;
            grid.Columns = newColumns;

            AssertIndicesAreEqual(expectedStartIndex, grid.StartIndex);
        }

        [TestCase(new TableIndex(3, 3), 4, 4, new TableIndex(3, 3))]
        [TestCase(new TableIndex(1, 5), 4, 4, new TableIndex(1, 4))]
        [TestCase(new TableIndex(5, 1), 4, 4, new TableIndex(4, 1))]
        private void EndIndexIndex_RowColumnResize_ShrinksIfNeeded(TableIndex endIndex, int newRows, int newColumns, TableIndex expectedEndIndex)
        {
            var grid = CreateDefaultGrid();
            grid.EndIndex = endIndex;

            grid.Rows = newRows;
            grid.Columns = newColumns;

            AssertIndicesAreEqual(expectedEndIndex, grid.EndIndex);
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

        private SelectableGraphicComponentMock[,] FillGrid(TableGrid grid)
        {
            var components = new SelectableGraphicComponentMock[grid.Rows, grid.Columns];

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
            grid.EndIndex = new TableIndex(rows - 1, columns - 1);

            return grid;
        }

       
    }
}
