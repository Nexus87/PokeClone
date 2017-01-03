using System;
using Base;
using Base.Data;
using BattleMode.Entities.BattleState;
using BattleMode.Shared;
using BattleModeTest.Utils;
using FakeItEasy;
using NUnit.Framework;

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
            _factory.CreateAIPokemon();

            state.Init(_factory.BattleData);

            Assert.IsFalse(state.IsDone);
        }

        [TestCase]
        public void Init_NoPKMNAreKO_IsDoneReturnsTrue()
        {
            var state = CreateWaitForCharState();
            _factory.CreatePlayerPokemon();
            _factory.CreateAIPokemon();

            state.Init(_factory.BattleData);

            Assert.IsTrue(state.IsDone);
        }

        [TestCase]
        public void SetMove_SomeData_ThrowsException()
        {
            var state = CreateWaitForCharState(_factory.BattleData);

            Assert.Throws<InvalidOperationException>(() => 
                state.SetMove(_factory.PlayerID, _factory.AIID, _factory.CreateMove())
                );
        }

        [TestCase]
        public void SetItem_SomeData_ThrowsException()
        {
            var state = CreateWaitForCharState(_factory.BattleData);

            Assert.Throws<InvalidOperationException>(() => 
                state.SetItem(_factory.PlayerID, _factory.AIID, _factory.CreateItem())
                );
        }

        [TestCase]
        public void SetCharacter_IdNeedsNoChar_ThrowsException()
        {
            _factory.CreatePlayerPokemon();
            _factory.CreateAIPokemon(StatusCondition.KO);

            var state = CreateWaitForCharState(_factory.BattleData);

            Assert.Throws<InvalidOperationException>(() => 
                state.SetCharacter(_factory.PlayerID, TestFactory.CreatePokemon())
                );

        }

        [TestCase]
        public void SetCharacter_NeededChar_UpdateDoesNotChangeBattleData()
        {
            var state = CreateWaitForCharState(_factory.BattleData);

            SetNewPlayerCharacter(state);
            state.Update(_factory.BattleData);

            var playerPkmn = _factory.GetPlayerPokemon();
            Assert.IsNull(playerPkmn);
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
            _factory.CreateAIPokemon();
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

            SetNewPlayerCharacter(state, playerCharacter);
            SetNewAiCharacter(state, aiCharacter);
            state.Update(_factory.BattleData);

            Assert.AreEqual(playerCharacter, _factory.GetPlayerPokemon());
            Assert.AreEqual(aiCharacter, _factory.GetAIPokemon());
        }

        private static WaitForCharState CreateWaitForCharState(BattleData battleData)
        {
            var state = CreateWaitForCharState();
            state.Init(battleData);
            return state;
        }

        private static WaitForCharState CreateWaitForCharState()
        {
            return new WaitForCharState(A.Fake<IEventCreator>());
        }

        private void SetNewPlayerCharacter(IBattleState state, Pokemon pokemon = null)
        {
            SetNewCharacter(state, _factory.PlayerID, pokemon);
        }

        private void SetNewAiCharacter(IBattleState state, Pokemon pokemon = null)
        {
            SetNewCharacter(state, _factory.AIID, pokemon);
        }

        private static void SetNewCharacter(IBattleState state, ClientIdentifier id, Pokemon pokemon = null){
            if(pokemon == null)
                pokemon = TestFactory.CreatePokemon();

            state.SetCharacter(id, pokemon);
        }
    }
}
