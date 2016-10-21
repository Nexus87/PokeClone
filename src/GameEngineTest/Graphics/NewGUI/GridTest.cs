using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public static List<TestCaseData> TestCaseDatas = new List<TestCaseData>()
        {
            new TestCaseData(new Rectangle(0, 0, 100, 100),
                new List<RowProperty>{new RowProperty{Height = 0.5f, Type = ValueType.Percent}, new RowProperty{Height = 0.5f, Type = ValueType.Percent}},
                new List<ColumnProperty>{new ColumnProperty{Width = 0.5f, Type = ValueType.Percent}, new ColumnProperty{Width = 0.5f, Type = ValueType.Percent}},
                new Table<Rectangle>(new Rectangle[,]{{new Rectangle(0, 0, 50, 50), new Rectangle(50, 0, 50, 50)},{new Rectangle(0, 50, 50, 50), new Rectangle(50, 50, 50, 50), }})
            )
        };

        public void Update_SetUpGridWithContent_ContentComponentsAreOrdered(
            Rectangle gridPosition,
            List<RowProperty> rows, List<ColumnProperty> columns,
            Table<Rectangle> expectedPositions)
        {
            var grid = CreateGrid(rows, columns);
            var components = FillGrid(grid, rows.Count, columns.Count);

            grid.Update(new GameTime());

            VerifyComponentsHaveExpectedPosition(components, expectedPositions);

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
                table[i, j] = componentMock;
                grid.SetComponent(componentMock.Object, i, j);
            });

            return table;
        }

        private static Grid CreateGrid(IEnumerable<RowProperty> rows, IEnumerable<ColumnProperty> columns)
        {
            var grid = new Grid();
            grid.AddAllColumns(columns);
            grid.AddAllRows(rows);

            return grid;
        }
    }
}