using System;
using BattleMode.Entities.BattleState;
using BattleMode.Entities.BattleState.States;
using BattleMode.Shared;
using BattleModeTest.Utils;
using NUnit.Framework;
using PokemonShared.Models;

namespace BattleModeTest.Components.BattleState
{
    [TestFixture]
    public class WaitForCharStateTest
    {
        private TestFactory _factory;

        [SetUp]
        public void Setup()
        {
            _factory = new TestFactory();
        }

        [TestCase]
        public void Init_SomePKMNAreKO_IsDoneReturnsFalse()
        {
            var state = CreateWaitForCharState();
            _factory.CreatePlayerPokemon(StatusCondition.KO);
            _factory.CreateAiPokemon();

            state.Init(_factory.BattleData);

            Assert.IsFalse(state.IsDone);
        }

        [TestCase]
        public void Init_NoPKMNAreKO_IsDoneReturnsTrue()
        {
            var state = CreateWaitForCharState();
            _factory.CreatePlayerPokemon();
            _factory.CreateAiPokemon();

            state.Init(_factory.BattleData);

            Assert.IsTrue(state.IsDone);
        }

        [TestCase]
        public void SetMove_SomeData_ThrowsException()
        {
            var state = CreateWaitForCharState(_factory.BattleData);

            Assert.Throws<InvalidOperationException>(() => 
                state.SetMove(_factory.PlayerId, _factory.Aiid, _factory.CreateMove())
                );
        }

        [TestCase]
        public void SetItem_SomeData_ThrowsException()
        {
            var state = CreateWaitForCharState(_factory.BattleData);

            Assert.Throws<InvalidOperationException>(() => 
                state.SetItem(_factory.PlayerId, _factory.Aiid, _factory.CreateItem())
                );
        }

        [TestCase]
        public void SetCharacter_IdNeedsNoChar_ThrowsException()
        {
            _factory.CreatePlayerPokemon();
            _factory.CreateAiPokemon(StatusCondition.KO);

            var state = CreateWaitForCharState(_factory.BattleData);

            Assert.Throws<InvalidOperationException>(() => 
                state.SetCharacter(_factory.PlayerId, TestFactory.CreatePokemon())
                );

        }

        [TestCase]
        public void SetCharacter_NeededChar_UpdateDoesNotChangeBattleData()
        {
            var state = CreateWaitForCharState(_factory.BattleData);

            SetNewPlayerCharacter(state);
            state.Update(_factory.BattleData);

            var playerPkmn = _factory.GetPlayerPokemon();
            Assert.IsFalse(playerPkmn.HasPokemon);
        }

        [TestCase]
        public void Update_AfterSettingAllCharacters_IsDoneReturnsTrue()
        {
            var state = CreateWaitForCharState(_factory.BattleData);

            SetNewPlayerCharacter(state);
            SetNewAiCharacter(state);
            state.Update(_factory.BattleData);

            Assert.IsTrue(state.IsDone);
        }

        [TestCase]
        public void Update_AfterSettingSomeCharacters_IsDoneReturnsFalse()
        {
            var state = CreateWaitForCharState(_factory.BattleData);

            SetNewPlayerCharacter(state);
            state.Update(_factory.BattleData);

            Assert.IsFalse(state.IsDone);
        }

        [TestCase]
        public void Init_SomePKMNAreNull_IsDoneReturnsFalse()
        {
            _factory.CreateAiPokemon();
            var state = CreateWaitForCharState();

            state.Init(_factory.BattleData);

            Assert.IsFalse(state.IsDone);
        }

        [TestCase]
        public void Update_SetAllCharacters_BattleDataContainsTheRightCharacters()
        {
            var state = CreateWaitForCharState(_factory.BattleData);
            var playerCharacter = TestFactory.CreatePokemon();
            var aiCharacter = TestFactory.CreatePokemon();

            var playerPokemonChanged = false;
            var aiPokemonChanged = false;

            _factory.GetPlayerPokemon().PokemonChanged += delegate{ playerPokemonChanged = true; };
            _factory.GetAiPokemon().PokemonChanged += delegate { aiPokemonChanged = true; };

            SetNewPlayerCharacter(state, playerCharacter);
            SetNewAiCharacter(state, aiCharacter);
            state.Update(_factory.BattleData);


            Assert.IsTrue(playerPokemonChanged);
            Assert.IsTrue(aiPokemonChanged);
        }

        private static WaitForPokemonState CreateWaitForCharState(BattleData battleData)
        {
            var state = CreateWaitForCharState();
            state.Init(battleData);
            return state;
        }

        private static WaitForPokemonState CreateWaitForCharState()
        {
            return new WaitForPokemonState();
        }

        private void SetNewPlayerCharacter(IBattleState state, Pokemon pokemon = null)
        {
            SetNewCharacter(state, _factory.PlayerId, pokemon);
        }

        private void SetNewAiCharacter(IBattleState state, Pokemon pokemon = null)
        {
            SetNewCharacter(state, _factory.Aiid, pokemon);
        }

        private static void SetNewCharacter(IBattleState state, ClientIdentifier id, Pokemon pokemon = null){
            if(pokemon == null)
                pokemon = TestFactory.CreatePokemon();

            state.SetCharacter(id, pokemon);
        }
    }
}
