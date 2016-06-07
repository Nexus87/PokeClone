using Base;
using Base.Data;
using BattleLib;
using BattleLib.Components.BattleState;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLibTest.Components.BattleState
{
    [TestFixture]
    public class WaitForCharStateTest
    {
        BattleData battleData;
        ClientIdentifier player;
        ClientIdentifier ai;

        [SetUp]
        public void Setup()
        {
            player = new ClientIdentifier();
            ai = new ClientIdentifier();
            battleData = new BattleData(player, ai);
        }

        [TestCase]
        public void Init_SomePKMNAreKO_IsDoneReturnsFalse()
        {
            battleData.GetPokemon(player).Pokemon = CreatePokemon(StatusCondition.KO);
            battleData.GetPokemon(ai).Pokemon = CreatePokemon(StatusCondition.Normal);
            var state = CreateWaitForCharState();

            state.Init(battleData);

            Assert.IsFalse(state.IsDone);
        }

        [TestCase]
        public void Init_NoPKMNAreKO_IsDoneReturnsTrue()
        {
            battleData.GetPokemon(player).Pokemon = CreatePokemon(StatusCondition.Normal);
            battleData.GetPokemon(ai).Pokemon = CreatePokemon(StatusCondition.Normal);
            var state = CreateWaitForCharState();

            state.Init(battleData);

            Assert.IsTrue(state.IsDone);
        }

        [TestCase]
        public void SetMove_SomeData_ThrowsException()
        {
            var state = CreateWaitForCharState(battleData);

            Assert.Throws<InvalidOperationException>(() => state.SetMove(player, ai, new Move(new MoveData())));
        }

        [TestCase]
        public void SetItem_SomeData_ThrowsException()
        {
            var state = CreateWaitForCharState(battleData);

            Assert.Throws<InvalidOperationException>(() => state.SetItem(player, ai, new Item()));
        }

        [TestCase]
        public void SetCharacter_IdNeedsNoChar_ThrowsException()
        {
            battleData.GetPokemon(player).Pokemon = CreatePokemon(StatusCondition.Normal);
            battleData.GetPokemon(ai).Pokemon = CreatePokemon(StatusCondition.KO);
            var state = CreateWaitForCharState(battleData);

            Assert.Throws<InvalidOperationException>(() => 
                state.SetCharacter(player, CreatePokemon(StatusCondition.Normal)));

        }

        [TestCase]
        public void SetCharacter_NeededChar_UpdateDoesNotChangeBattleData()
        {
            var state = CreateWaitForCharState(battleData);

            state.SetCharacter(player, CreatePokemon());
            state.Update(battleData);

            var playerPkmn = battleData.GetPokemon(player);
            Assert.IsNull(playerPkmn.Pokemon);
        }

        [TestCase]
        public void Update_AfterSettingAllCharacters_IsDoneReturnsTrue()
        {
            var state = CreateWaitForCharState(battleData);

            state.SetCharacter(player, CreatePokemon());
            state.SetCharacter(ai, CreatePokemon());
            state.Update(battleData);

            Assert.IsTrue(state.IsDone);
        }

        [TestCase]
        public void Update_AfterSettingSomeCharacters_IsDoneReturnsFalse()
        {
            var state = CreateWaitForCharState(battleData);

            state.SetCharacter(player, CreatePokemon());
            state.Update(battleData);

            Assert.IsFalse(state.IsDone);
        }

        [TestCase]
        public void Init_SomePKMNAreNull_IsDoneReturnsFalse()
        {
            battleData.GetPokemon(ai).Pokemon = CreatePokemon(StatusCondition.Normal);
            var state = CreateWaitForCharState();

            state.Init(battleData);

            Assert.IsFalse(state.IsDone);
        }

        [TestCase]
        public void Update_SetAllCharacters_BattleDataContainsTheRightCharacters()
        {
            var state = CreateWaitForCharState(battleData);
            var playerCharacter = CreatePokemon();
            var aiCharacter = CreatePokemon();

            state.SetCharacter(player, playerCharacter);
            state.SetCharacter(ai, aiCharacter);
            state.Update(battleData);

            Assert.AreEqual(playerCharacter, battleData.GetPokemon(player).Pokemon);
            Assert.AreEqual(aiCharacter, battleData.GetPokemon(ai).Pokemon);
        }
        private WaitForCharState CreateWaitForCharState(BattleData battleData)
        {
            var state = CreateWaitForCharState();
            state.Init(battleData);
            return state;
        }
        private WaitForCharState CreateWaitForCharState()
        {
            return new WaitForCharState(new Mock<IEventCreator>().Object);
        }

        private Pokemon CreatePokemon(StatusCondition statusCondition = StatusCondition.Normal)
        {
            return new Pokemon(new PokemonData(), new Stats()) { Condition = statusCondition };
        }
    }
}
