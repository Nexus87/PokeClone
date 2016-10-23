﻿using System.Collections.Generic;
using GameEngine.Graphics.NewGUI;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;

namespace GameEngineTest.Graphics.NewGUI
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

        [TestCaseSource("PercentPropertiesData")]
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

//            new TestCaseData(new Rectangle(50, 20, 100, 200),
//                new List<RowProperty>{new RowProperty{Height = 20, Type = ValueType.Absolute}, new RowProperty{Height = 10, Type = ValueType.Absolute}},
//                new List<ColumnProperty>{new ColumnProperty{Width = 30, Type = ValueType.Absolute}, new ColumnProperty{Width = 70, Type = ValueType.Absolute}},
//                new Table<Rectangle>(new[,]
//                {
//                    {new Rectangle(50, 20, 30, 20), new Rectangle(80, 20, 70, 20)},
//                    {new Rectangle(50, 40, 30, 10), new Rectangle(80, 40, 70, 10)}
//                })
//            ),
//            new TestCaseData(new Rectangle(0, 0, 90, 300),
//                new List<RowProperty>{new RowProperty{Height = 200, Type = ValueType.Absolute}, new RowProperty{Height = 200, Type = ValueType.Absolute}},
//                new List<ColumnProperty>{new ColumnProperty{Width = 100, Type = ValueType.Absolute}, new ColumnProperty{Width = 80, Type = ValueType.Absolute}},
//                new Table<Rectangle>(new[,]
//                {
//                    {new Rectangle(0, 0, 100, 200), new Rectangle(100, 0, 80, 200)},
//                    {new Rectangle(0, 200, 100, 200), new Rectangle(100, 200, 80, 200)}
//                })
//            )

        };

        [TestCaseSource("FixedPropertiesTestData")]
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

        private static void VerifyComponentsHaveExpectedPosition(ITable<Mock<IGraphicComponent>> components, ITable<Rectangle> expectedPositions)
        {
            Extensions.LoopOverTable(components.Rows, components.Columns, (i, j) =>
            {
                var expectedPosition = expectedPositions[i, j];
                components[i, j].VerifySet(c => c.Constraints = expectedPosition);
            });
        }

        private static Table<Mock<IGraphicComponent>> FillGrid(Grid grid, int rows, int columns)
        {
            var table = new Table<Mock<IGraphicComponent>>(rows, columns);
            Extensions.LoopOverTable(rows, columns, (i, j) =>
            {
                var componentMock = new Mock<IGraphicComponent>();
                componentMock.SetupAllProperties();
                table[i, j] = componentMock;
                grid.SetComponent(componentMock.Object, i, j);
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
    }
}