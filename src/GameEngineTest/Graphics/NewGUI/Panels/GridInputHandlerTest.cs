using System.Collections.Generic;
using GameEngine;
using GameEngine.Graphics.NewGUI;
using GameEngine.Graphics.NewGUI.Panels;
using GameEngine.Utils;
using Moq;
using NUnit.Framework;

namespace GameEngineTest.Graphics.NewGUI.Panels
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
        public void HandleInput_DirectionKey_ExpectedComponentIsSelected(
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
                var component = new Mock<IGraphicComponent>();
                component.SetupAllProperties();
                component.SetupGet(c => c.IsSelectable).Returns(selectableComponents[row, column]);
                var gridCell = new GridCell() {GraphicComponent = component.Object};
                table[row, column] = gridCell;
            });

            return table;
        }
    }
}