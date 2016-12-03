using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using GameEngine.GUI.Panels;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace GameEngine.GUI.Test.Panels
{
    [TestFixture]
    public class GridTest
    {
        public static List<TestCaseData> PercentPropertiesData = new List<TestCaseData>()
        {
            new TestCaseData(new Rectangle(0, 0, 100, 100),
                new List<RowProperty>{new RowProperty{Share = 1, Type = ValueType.Percent}, new RowProperty{Share = 1, Type = ValueType.Percent}},
                new List<ColumnProperty>{new ColumnProperty{Share = 1, Type = ValueType.Percent}, new ColumnProperty{Share = 1, Type = ValueType.Percent}},
                new Table<Rectangle>(new[,]
                {
                    {new Rectangle(0, 0, 50, 50), new Rectangle(50, 0, 50, 50)},
                    {new Rectangle(0, 50, 50, 50), new Rectangle(50, 50, 50, 50)}
                })
            ),

            new TestCaseData(new Rectangle(50, 20, 100, 200),
                new List<RowProperty>{new RowProperty{Share = 1, Type = ValueType.Percent}, new RowProperty{Share = 1, Type = ValueType.Percent}},
                new List<ColumnProperty>{new ColumnProperty{Share = 1, Type = ValueType.Percent}, new ColumnProperty{Share = 1, Type = ValueType.Percent}},
                new Table<Rectangle>(new[,]
                {
                    {new Rectangle(50, 20, 50, 100), new Rectangle(100, 20, 50, 100)},
                    {new Rectangle(50, 120, 50, 100), new Rectangle(100, 120, 50, 100)}
                })),
            new TestCaseData(new Rectangle(0, 0, 90, 300),
                new List<RowProperty>{new RowProperty{Share = 1, Type = ValueType.Percent}, new RowProperty{Share = 2, Type = ValueType.Percent}},
                new List<ColumnProperty>{new ColumnProperty{Share = 2, Type = ValueType.Percent}, new ColumnProperty{Share = 1, Type = ValueType.Percent}},
                new Table<Rectangle>(new[,]
                {
                    {new Rectangle(0, 0, 60, 100), new Rectangle(60, 0, 30, 100)},
                    {new Rectangle(0, 100, 60, 200), new Rectangle(60, 100, 30, 200)}
                }))
        };

        [TestCaseSource(nameof(PercentPropertiesData))]
        public void Update_RowsAndColumnsWithPercentType_ContentComponentsAreOrdered(
            Rectangle gridPosition,
            List<RowProperty> rows, List<ColumnProperty> columns,
            Table<Rectangle> expectedPositions)
        {
            var grid = CreateGrid(gridPosition, rows, columns);
            var components = FillGrid(grid, rows.Count, columns.Count);

            grid.Update(new GameTime());

            VerifyComponentsHaveExpectedPosition(components, expectedPositions);

        }

        public static List<TestCaseData> FixedPropertiesTestData = new List<TestCaseData>()
        {
            new TestCaseData(new Rectangle(0, 0, 100, 100),
                new List<RowProperty>{new RowProperty{Height = 20, Type = ValueType.Absolute}, new RowProperty{Height = 10, Type = ValueType.Absolute}},
                new List<ColumnProperty>{new ColumnProperty{Width = 30, Type = ValueType.Absolute}, new ColumnProperty{Width = 70, Type = ValueType.Absolute}},
                new Table<Rectangle>(new[,]
                {
                    {new Rectangle(0, 0, 30, 20), new Rectangle(30, 0, 70, 20)},
                    {new Rectangle(0, 20, 30, 10), new Rectangle(30, 20, 70, 10)}
                })
            ),

            new TestCaseData(new Rectangle(50, 20, 100, 200),
                new List<RowProperty>{new RowProperty{Height = 20, Type = ValueType.Absolute}, new RowProperty{Height = 10, Type = ValueType.Absolute}},
                new List<ColumnProperty>{new ColumnProperty{Width = 30, Type = ValueType.Absolute}, new ColumnProperty{Width = 70, Type = ValueType.Absolute}},
                new Table<Rectangle>(new[,]
                {
                    {new Rectangle(50, 20, 30, 20), new Rectangle(80, 20, 70, 20)},
                    {new Rectangle(50, 40, 30, 10), new Rectangle(80, 40, 70, 10)}
                })
            ),
            new TestCaseData(new Rectangle(0, 0, 90, 300),
                new List<RowProperty>{new RowProperty{Height = 200, Type = ValueType.Absolute}, new RowProperty{Height = 200, Type = ValueType.Absolute}},
                new List<ColumnProperty>{new ColumnProperty{Width = 100, Type = ValueType.Absolute}, new ColumnProperty{Width = 80, Type = ValueType.Absolute}},
                new Table<Rectangle>(new[,]
                {
                    {new Rectangle(0, 0, 100, 200), new Rectangle(100, 0, 80, 200)},
                    {new Rectangle(0, 200, 100, 200), new Rectangle(100, 200, 80, 200)}
                })
            )

        };

        [TestCaseSource(nameof(FixedPropertiesTestData))]
        public void Update_RowsAndColumnsWithFixedType_ContentComponentsAreOrdered(
            Rectangle gridPosition,
            List<RowProperty> rows, List<ColumnProperty> columns,
            Table<Rectangle> expectedPositions)
        {
            var grid = CreateGrid(gridPosition, rows, columns);
            var components = FillGrid(grid, rows.Count, columns.Count);

            grid.Update(new GameTime());

            VerifyComponentsHaveExpectedPosition(components, expectedPositions);

        }

        public static List<TestCaseData> AutoPropertiesTestData = new List<TestCaseData>()
        {
            new TestCaseData(new Rectangle(0, 0, 100, 100),
                new List<RowProperty>{new RowProperty{Type = ValueType.Auto}, new RowProperty{Type = ValueType.Auto}},
                new List<ColumnProperty>{new ColumnProperty{Type = ValueType.Auto}, new ColumnProperty{Type = ValueType.Auto}},
                new Table<Rectangle>(new[,]
                {
                    {new Rectangle(0, 0, 30, 20), new Rectangle(0, 0, 30, 20)},
                    {new Rectangle(0, 0, 30, 20), new Rectangle(0, 0, 20, 20)}
                }),
                new Table<Rectangle>(new[,]
                {
                    {new Rectangle(0, 0, 30, 20), new Rectangle(30, 0, 30, 20)},
                    {new Rectangle(0, 20, 30, 20), new Rectangle(30, 20, 30, 20)}
                })
            ),

            new TestCaseData(new Rectangle(0, 0, 100, 100),
                new List<RowProperty>{new RowProperty{Type = ValueType.Auto}, new RowProperty{Type = ValueType.Auto}},
                new List<ColumnProperty>{new ColumnProperty{Type = ValueType.Auto}, new ColumnProperty{Type = ValueType.Auto}},
                new Table<Rectangle>(new[,]
                {
                    {new Rectangle(0, 0, 10, 10), new Rectangle(0, 0, 10, 20)},
                    {new Rectangle(0, 0, 30, 20), new Rectangle(0, 0, 20, 10)}
                }),
                new Table<Rectangle>(new[,]
                {
                    {new Rectangle(0, 0, 30, 20), new Rectangle(30, 0, 20, 20)},
                    {new Rectangle(0, 20, 30, 20), new Rectangle(30, 20, 20, 20)}
                })
            ),
            new TestCaseData(new Rectangle(0, 0, 100, 100),
                new List<RowProperty>{new RowProperty{Type = ValueType.Auto}, new RowProperty{Type = ValueType.Auto}},
                new List<ColumnProperty>{new ColumnProperty{Type = ValueType.Auto}, new ColumnProperty{Type = ValueType.Auto}},
                new Table<Rectangle>(new[,]
                {
                    {new Rectangle(0, 0, 90, 80), new Rectangle(0, 0, 30, 80)},
                    {new Rectangle(0, 0, 30, 30), new Rectangle(0, 0, 20, 10)}
                }),
                new Table<Rectangle>(new[,]
                {
                    {new Rectangle(0, 0, 90, 80), new Rectangle(90, 0, 30, 80)},
                    {new Rectangle(0, 80, 90, 30), new Rectangle(90, 80, 30, 30)}
                })
            ),

        };

        [TestCaseSource(nameof(AutoPropertiesTestData))]
        public void Update_RowsAndColumnsWithAutoType_ContentComponentsAreOrdered(
            Rectangle gridPosition,
            List<RowProperty> rows, List<ColumnProperty> columns,
            Table<Rectangle> preferredSizes,
            Table<Rectangle> expectedPositions)
        {
            var grid = CreateGrid(gridPosition, rows, columns);
            var components = FillGridWithPreferedSizes(grid, preferredSizes, rows.Count, columns.Count);

            grid.Update(new GameTime());

            VerifyComponentsHaveExpectedPosition(components, expectedPositions);

        }

        public static List<TestCaseData> MixedPropertiesTestData = new List<TestCaseData>()
        {
            new TestCaseData(new Rectangle(0, 0, 100, 100),
                new List<RowProperty>{new RowProperty{Type = ValueType.Auto}, new RowProperty{Type = ValueType.Percent, Share = 1}},
                new List<ColumnProperty>{new ColumnProperty{Type = ValueType.Absolute, Width = 110}, new ColumnProperty{Type = ValueType.Percent, Share = 1}},
                new Table<Rectangle>(new[,]
                {
                    {new Rectangle(0, 0, 30, 20), new Rectangle(0, 0, 30, 20)},
                    {new Rectangle(0, 0, 30, 20), new Rectangle(0, 0, 20, 20)}
                }),
                new Table<Rectangle>(new[,]
                {
                    {new Rectangle(0, 0, 110, 20), new Rectangle(110, 0, 0, 20)},
                    {new Rectangle(0, 20, 110, 80), new Rectangle(110, 20, 0, 80)}
                })
            ),
        };

        [TestCaseSource(nameof(MixedPropertiesTestData))]
        public void Update_RowsAndColumnsWithMixedTypes_ContentComponentsAreOrdered(
            Rectangle gridPosition,
            List<RowProperty> rows, List<ColumnProperty> columns,
            Table<Rectangle> preferredSizes,
            Table<Rectangle> expectedPositions)
        {
            var grid = CreateGrid(gridPosition, rows, columns);
            var components = FillGridWithPreferedSizes(grid, preferredSizes, rows.Count, columns.Count);

            grid.Update(new GameTime());

            VerifyComponentsHaveExpectedPosition(components, expectedPositions);

        }
        private static ITable<IGraphicComponent> FillGridWithPreferedSizes(Grid grid, ITable<Rectangle> preferredSizes,
            int rowsCount, int columnsCount)
        {
            var table = new Table<IGraphicComponent>(rowsCount, columnsCount);
            Extensions.LoopOverTable(rowsCount, columnsCount, (i, j) =>
            {
                var componentMock = A.Fake<IGraphicComponent>();
                A.CallTo(() => componentMock.PreferedHeight).Returns(preferredSizes[i, j].Height);
                A.CallTo(() => componentMock.PreferedWidth).Returns(preferredSizes[i, j].Width);
                table[i, j] = componentMock;
                grid.SetComponent(componentMock, i, j);
            });

            return table;
        }


        [TestCase]
        public void Rows_AfterAddRowAndColumn_AsExpected()
        {
            const int expected = 1;
            var grid = new Grid();

            grid.AddRow(new RowProperty());
            grid.AddColumn(new ColumnProperty());

            Assert.AreEqual(expected, grid.Rows);
        }

        [TestCase]
        public void Columns_AfterAddRowAndColumn_AsExpected()
        {
            const int expected = 1;
            var grid = new Grid();

            grid.AddRow(new RowProperty());
            grid.AddColumn(new ColumnProperty());

            Assert.AreEqual(expected, grid.Columns);
        }

        public static List<TestCaseData> RemoveColumnData = new List<TestCaseData>
        {
            new TestCaseData(new Rectangle(100, 200, 100, 300), 10, 8, 1)
        };

        [TestCaseSource(nameof(RemoveColumnData))]
        public void RemoveColumn_ValidColumn_PreviousAddedComponentsAreNotUpdated(Rectangle position, int rows, int columns, int columnToBeRemoved)
        {
            var grid = CreateGrid(position, rows, columns);
            var components = FillGrid(grid, rows, columns);

            grid.RemoveColumn(columnToBeRemoved);
            grid.Update(new GameTime());

            foreach (var component in components.EnumerateRows(columnToBeRemoved))
            {
                A.CallToSet(() => component.Constraints).MustNotHaveHappened();
            }
        }

        public static List<TestCaseData> RemoveRowData = new List<TestCaseData>
        {
            new TestCaseData(new Rectangle(100, 200, 100, 300), 10, 8, 1)
        };

        [TestCaseSource(nameof(RemoveRowData))]
        public void RemoveRow_ValidColumn_PreviousAddedComponentsAreNotUpdated(Rectangle position, int rows, int columns, int rowToBeRemoved)
        {
            var grid = CreateGrid(position, rows, columns);
            var components = FillGrid(grid, rows, columns);

            grid.RemoveRow(rowToBeRemoved);
            grid.Update(new GameTime());

            foreach (var component in components.EnumerateColumns(rowToBeRemoved))
            {
                A.CallToSet(() => component.Constraints).MustNotHaveHappened();
            }
        }
        private static void VerifyComponentsHaveExpectedPosition(ITable<IGraphicComponent> components, ITable<Rectangle> expectedPositions)
        {
            Extensions.LoopOverTable(components.Rows, components.Columns, (i, j) =>
            {
                var expectedPosition = expectedPositions[i, j];
                A.CallToSet(() => components[i, j].Constraints).To(expectedPosition).MustHaveHappened(Repeated.AtLeast.Once);
            });
        }

        private static Table<IGraphicComponent> FillGrid(Grid grid, int rows, int columns)
        {
            var table = new Table<IGraphicComponent>(rows, columns);
            Extensions.LoopOverTable(rows, columns, (i, j) =>
            {
                var componentMock = A.Fake<IGraphicComponent>();
                table[i, j] = componentMock;
                grid.SetComponent(componentMock, i, j);
            });

            return table;
        }

        private static Grid CreateGrid(Rectangle gridPosition, IEnumerable<RowProperty> rows, IEnumerable<ColumnProperty> columns)
        {
            var grid = new Grid {Constraints = gridPosition};
            grid.AddAllColumns(columns);
            grid.AddAllRows(rows);

            return grid;
        }

        private static Grid CreateGrid(Rectangle gridPosition, int rows, int column)
        {
            var rowProperties = Enumerable
                .Range(0, rows)
                .Select(i => new RowProperty {Type = ValueType.Auto, Share = 1});
            var columnProperties = Enumerable
                .Range(0, column)
                .Select(i => new ColumnProperty {Type = ValueType.Auto, Share = 1});

            return CreateGrid(gridPosition, rowProperties, columnProperties);
        }
    }
}