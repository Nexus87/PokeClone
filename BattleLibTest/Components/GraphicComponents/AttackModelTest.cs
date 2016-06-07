﻿using Base;
using Base.Data;
using BattleLib;
using BattleLib.Components.BattleState;
using BattleLib.GraphicComponents;
using GameEngine.Graphics;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BattleLibTest.GUI
{
    internal class AttackModelTestFactory
    {
        public static Pokemon CreatePokemon(int numMoves)
        {
            var pkmn = new Pokemon(new PokemonData(), new Stats());
            for (int i = 0; i < numMoves; i++)
                pkmn.SetMove(i, new Move(new MoveData { Name = GetMoveName(i) }));

            return pkmn;
        }

        public static AttackModel CreateModel(int moves)
        {
            return CreateModel(CreatePokemon(moves));

        }
        public static AttackModel CreateEmptyModel()
        {
            return CreateModel(pkmn: null);
        }

        public static AttackModel CreateModel(Pokemon pkmn)
        {
            var wrapper = new PokemonWrapper(new ClientIdentifier());
            if(pkmn != null)
                wrapper.Pokemon = pkmn;

            return CreateModel(wrapper);
        }

        public static AttackModel CreateModel(PokemonWrapper wrapper)
        {
            return new AttackModel(wrapper);
        }

        public static PokemonWrapper CreateWrapper()
        {
            return new PokemonWrapper(new ClientIdentifier());
        }

        public static string GetMoveName(int number)
        {
            return "Move " + number;
        }
    }

    [TestFixture]
    public class AttackModelTest
    {
        [TestCase]
        public void ColumnsRows_NoDataSet_1And4()
        {
            var model = AttackModelTestFactory.CreateEmptyModel();

            Assert.AreEqual(1, model.Columns);
            Assert.AreEqual(4, model.Rows);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void DataAt_SetSomeData_ReturnsSetData(int moves)
        {
            var model = AttackModelTestFactory.CreateModel(moves);

            for (int i = 0; i < moves; i++)
                Assert.AreEqual(AttackModelTestFactory.GetMoveName(i), model.DataAt(i, 0).Name);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void DataAt_SetSomeData_ReturnsNullForUnsetData(int moves)
        {
            var model = AttackModelTestFactory.CreateModel(moves);

            for (int i = moves; i < 4; i++)
                Assert.Null(model.DataAt(i, 0));
        }

        [TestCase(1, 3)]
        [TestCase(3, 1)]
        [TestCase(4, 4)]
        [TestCase(2, 1)]
        public void DataAt_ResetPokemon_MovesHaveChanged(int oldMoves, int newMoves)
        {
            var wrapper = AttackModelTestFactory.CreateWrapper();
            wrapper.Pokemon = AttackModelTestFactory.CreatePokemon(oldMoves);
            var model = AttackModelTestFactory.CreateModel(wrapper);

            wrapper.Pokemon = AttackModelTestFactory.CreatePokemon(newMoves);

            int i = 0;
            for (; i < newMoves; i++)
                Assert.AreEqual(AttackModelTestFactory.GetMoveName(i), model.DataAt(i, 0).Name);

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
            var wrapper = AttackModelTestFactory.CreateWrapper();
            wrapper.Pokemon = AttackModelTestFactory.CreatePokemon(oldMoves);
            var model = AttackModelTestFactory.CreateModel(wrapper);

            model.SizeChanged += delegate { sizeChanged = true; };

            wrapper.Pokemon = AttackModelTestFactory.CreatePokemon(newMoves);

            Assert.False(sizeChanged);
        }

        [TestCase(1, 3)]
        [TestCase(3, 1)]
        [TestCase(4, 4)]
        [TestCase(2, 1)]
        public void DataChangedEvent_ResetPokemon_IsRaised(int oldMoves, int newMoves)
        {
            bool dataChanged = false;
            var wrapper = AttackModelTestFactory.CreateWrapper();
            wrapper.Pokemon = AttackModelTestFactory.CreatePokemon(oldMoves);

            var eventArgs = new List<DataChangedEventArgs<Move>>();
            var model = AttackModelTestFactory.CreateModel(wrapper);

            model.DataChanged += (a, b) => dataChanged = true;

            wrapper.Pokemon = AttackModelTestFactory.CreatePokemon(newMoves);

            Assert.True(dataChanged);
        }

        [TestCase]
        public void SetDataAt_SomeValue_ThrowsExecption()
        {
            var model = AttackModelTestFactory.CreateModel(3);
            Assert.Throws<InvalidOperationException>(delegate { model.SetDataAt(new Move(new MoveData()), 2, 0); });
        }

        [TestCase]
        public void SetIndexer_SomeValue_ThrowsExecption()
        {
            var model = AttackModelTestFactory.CreateModel(3);
            Assert.Throws<InvalidOperationException>(delegate { model[2, 0] = new Move(new MoveData()); });
        }

        [TestCase(-1)]
        [TestCase(-10)]
        [TestCase(4)]
        [TestCase(10)]
        public void DataAt_IndexOutOfBound_ThrowsExecption(int idx)
        {
            var model = AttackModelTestFactory.CreateModel(3);
            Assert.Throws<ArgumentOutOfRangeException>(delegate { model.DataAt(idx, 0); });
        }

        [TestCase(-1)]
        [TestCase(-10)]
        [TestCase(4)]
        [TestCase(10)]
        public void GetIndexer_IndexOutOfBound_ThrowsExecption(int idx)
        {
            var model = AttackModelTestFactory.CreateModel(3);
            Assert.Throws<ArgumentOutOfRangeException>(delegate { var d = model[idx, 0]; });
        }
    }
}