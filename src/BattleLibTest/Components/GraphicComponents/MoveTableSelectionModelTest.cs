using Base;
using Base.Data;
using BattleMode.Core.Components.GraphicComponents;
using BattleMode.Gui;
using GameEngineTest.TestUtils;
using NUnit.Framework;

namespace BattleModeTest.Components.GraphicComponents
{
    [TestFixture]
    internal class MoveTableSelectionModelTest
    {
        [Test]
        public void SelectIndex_ModelHasNoValidValue_ReturnsFalse()
        {
            var model = new TableModelStub<Move> { Rows = 5, Columns = 1 };
            var selectionModel = new MoveTableSelectionModel(model);

            bool wasSelected = selectionModel.SelectIndex(1, 0);

            Assert.False(wasSelected);
        }

        [TestCase(20, 0)]
        [TestCase(0, 4)]
        public void SelectIndex_IndexOutOfBound_ReturnFalse(int row, int column)
        {
            var model = new TableModelStub<Move> { Rows = 5, Columns = 1, ReturnValue = new Move(new MoveData()) };
            var selectionModel = new MoveTableSelectionModel(model);

            bool wasSelected = selectionModel.SelectIndex(row, column);

            Assert.False(wasSelected);
        }
    }
}
