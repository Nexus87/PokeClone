using Base;
using Base.Data;
using BattleLib.GraphicComponents;
using GameEngineTest.TestUtils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLibTest.Components.GraphicComponents
{
    [TestFixture]
    class AttackTableSelectionModelTest
    {
        [Test]
        public void SelectIndex_ModelHasNoValidValue_ReturnsFalse()
        {
            var model = new TableModelStub<Move> { Rows = 5, Columns = 1 };
            var selectionModel = new AttackTableSelectionModel(model);

            bool wasSelected = selectionModel.SelectIndex(1, 0);

            Assert.False(wasSelected);
        }

        [TestCase(20, 0)]
        [TestCase(0, 4)]
        public void SelectIndex_IndexOutOfBound_ReturnFalse(int row, int column)
        {
            var model = new TableModelStub<Move> { Rows = 5, Columns = 1, ReturnValue = new Move(new MoveData()) };
            var selectionModel = new AttackTableSelectionModel(model);

            bool wasSelected = selectionModel.SelectIndex(row, column);

            Assert.False(wasSelected);
        }
    }
}
