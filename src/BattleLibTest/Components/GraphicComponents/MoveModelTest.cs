using Base;
using Base.Data;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using GameEngine.GUI.Graphics.TableView;

namespace BattleLibTest.GUI
{
    [TestFixture]
    public class MoveModelTest
    {
        [TestCase]
        public void ColumnsRows_NoDataSet_1And4()
        {
            var model = MoveModelTestFactory.CreateEmptyModel();

            Assert.AreEqual(1, model.Columns);
            Assert.AreEqual(4, model.Rows);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void DataAt_SetSomeData_ReturnsSetData(int moves)
        {
            var model = MoveModelTestFactory.CreateModel(moves);

            for (int i = 0; i < moves; i++)
                Assert.AreEqual(MoveModelTestFactory.GetMoveName(i), model.DataAt(i, 0).Name);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void DataAt_SetSomeData_ReturnsNullForUnsetData(int moves)
        {
            var model = MoveModelTestFactory.CreateModel(moves);

            for (int i = moves; i < 4; i++)
                Assert.Null(model.DataAt(i, 0));
        }

        [TestCase(1, 3)]
        [TestCase(3, 1)]
        [TestCase(4, 4)]
        [TestCase(2, 1)]
        public void DataAt_ResetPokemon_MovesHaveChanged(int oldMoves, int newMoves)
        {
            var wrapper = MoveModelTestFactory.CreateWrapper();
            wrapper.Pokemon = MoveModelTestFactory.CreatePokemon(oldMoves);
            var model = MoveModelTestFactory.CreateModel(wrapper);

            wrapper.Pokemon = MoveModelTestFactory.CreatePokemon(newMoves);

            int i = 0;
            for (; i < newMoves; i++)
                Assert.AreEqual(MoveModelTestFactory.GetMoveName(i), model.DataAt(i, 0).Name);

            for (; i < 4; i++)
                Assert.Null(model.DataAt(i, 0));
        }

        [TestCase(1, 3)]
        [TestCase(3, 1)]
        [TestCase(4, 4)]
        [TestCase(2, 1)]
        public void SizeChangedEvent_ResetPokemon_IsNotRaised(int oldMoves, int newMoves)
        {
            bool sizeChanged = false;
            var wrapper = MoveModelTestFactory.CreateWrapper();
            wrapper.Pokemon = MoveModelTestFactory.CreatePokemon(oldMoves);
            var model = MoveModelTestFactory.CreateModel(wrapper);

            model.SizeChanged += delegate { sizeChanged = true; };

            wrapper.Pokemon = MoveModelTestFactory.CreatePokemon(newMoves);

            Assert.False(sizeChanged);
        }

        [TestCase(1, 3)]
        [TestCase(3, 1)]
        [TestCase(4, 4)]
        [TestCase(2, 1)]
        public void DataChangedEvent_ResetPokemon_IsRaised(int oldMoves, int newMoves)
        {
            var dataChanged = false;
            var wrapper = MoveModelTestFactory.CreateWrapper();
            wrapper.Pokemon = MoveModelTestFactory.CreatePokemon(oldMoves);

            new List<DataChangedEventArgs<Move>>();
            var model = MoveModelTestFactory.CreateModel(wrapper);

            model.DataChanged += (a, b) => dataChanged = true;

            wrapper.Pokemon = MoveModelTestFactory.CreatePokemon(newMoves);

            Assert.True(dataChanged);
        }

        [TestCase]
        public void SetDataAt_SomeValue_ThrowsExecption()
        {
            var model = MoveModelTestFactory.CreateModel(3);
            Assert.Throws<InvalidOperationException>(delegate { model.SetDataAt(new Move(new MoveData()), 2, 0); });
        }

        [TestCase]
        public void SetIndexer_SomeValue_ThrowsExecption()
        {
            var model = MoveModelTestFactory.CreateModel(3);
            Assert.Throws<InvalidOperationException>(delegate { model[2, 0] = new Move(new MoveData()); });
        }

        [TestCase(-1)]
        [TestCase(-10)]
        [TestCase(4)]
        [TestCase(10)]
        public void DataAt_IndexOutOfBound_ThrowsExecption(int idx)
        {
            var model = MoveModelTestFactory.CreateModel(3);
            Assert.Throws<ArgumentOutOfRangeException>(delegate { model.DataAt(idx, 0); });
        }

        [TestCase(-1)]
        [TestCase(-10)]
        [TestCase(4)]
        [TestCase(10)]
        public void GetIndexer_IndexOutOfBound_ThrowsExecption(int idx)
        {
            var model = MoveModelTestFactory.CreateModel(3);
            Assert.Throws<ArgumentOutOfRangeException>(delegate { var d = model[idx, 0]; });
        }
    }
}
