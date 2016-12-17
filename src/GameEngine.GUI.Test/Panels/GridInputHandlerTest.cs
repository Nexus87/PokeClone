using System.Collections.Generic;
using FakeItEasy;
using GameEngine.Globals;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Panels;
using GameEngine.Utils;
using NUnit.Framework;
using Extensions = GameEngine.Utils.Extensions;

namespace GameEngine.GUI.Test.Panels
{
    [TestFixture]
    public class GridInputHandlerTest
    {
        public static List<TestCaseData> InputHandlerData = new List<TestCaseData>
        {
            new TestCaseData(
                new[,]
                {
                    {true, true, true},
                    {true, true, true},
                    {true, true, true}
                },
                CommandKeys.Right, 0, 1
            ),
            new TestCaseData(
                new[,]
                {
                    {true, true, true},
                    {true, true, true},
                    {true, true, true}
                },
                CommandKeys.Down, 1, 0
            ),
            new TestCaseData(
                new[,]
                {
                    {true, false, true},
                    {false, false, false},
                    {true, true, true}
                },
                CommandKeys.Right, 0, 2
            ),
            new TestCaseData(
                new[,]
                {
                    {true, false, false},
                    {false, false, false},
                    {true, true, true}
                },
                CommandKeys.Right, 0, 0
            ),
            new TestCaseData(
                new[,]
                {
                    {true, true, true},
                    {true, true, true},
                    {true, true, true}
                },
                CommandKeys.Up, 0, 0
            ),
            new TestCaseData(
                new[,]
                {
                    {true, false, true},
                    {false, false, false},
                    {true, true, true}
                },
                CommandKeys.Down, 2, 0
            ),
            new TestCaseData(
                new[,]
                {
                    {true, false, false},
                    {false, false, false},
                    {false, true, true}
                },
                CommandKeys.Down, 0, 0
            ),
            new TestCaseData(
                new[,]
                {
                    {true, true, true},
                    {true, true, true},
                    {true, true, true}
                },
                CommandKeys.Left, 0, 0
            ),
        };

        [TestCaseSource(nameof(InputHandlerData))]
        public void HandleKeyInput_DirectionKey_ExpectedComponentIsSelected(
            bool[,] selectableComponents, CommandKeys key, int expectedRow, int expectedColumn)
        {
            var componentTable = CreateComponentTable(selectableComponents);
            var gridInputHandler = CreateGridInputHandler(componentTable);

            gridInputHandler.HandleKeyInput(key);

            Assert.IsTrue(componentTable[expectedRow, expectedColumn].IsSelected);
        }

        private static GridInputHandler CreateGridInputHandler(Table<GridCell> componentTable)
        {
            return new GridInputHandler(componentTable);
        }

        private static Table<GridCell> CreateComponentTable(bool[,] selectableComponents)
        {
            var table = new Table<GridCell>();
            Extensions.LoopOverTable(selectableComponents.Rows(), selectableComponents.Columns(), (row, column) =>
            {
                var component = A.Fake<IGraphicComponent>();
                A.CallTo(() => component.IsSelectable).Returns(selectableComponents[row, column]);
                var gridCell = new GridCell() {GuiComponent = component};
                table[row, column] = gridCell;
            });

            return table;
        }
    }
}