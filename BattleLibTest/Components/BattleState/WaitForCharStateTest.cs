using Base;
using Base.Data;
using BattleLib;
using BattleLib.Components.BattleState;
using BattleLibTest.Utils;
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
        TestFactory factory;

        [SetUp]
        public void Setup()
        {
            factory = new TestFactory();
        }

        [TestCase]
        public void Init_SomePKMNAreKO_IsDoneReturnsFalse()
        {
            var state = CreateWaitForCharState();
            factory.CreatePlayerPokemon(StatusCondition.KO);
            factory.CreateAIPokemon();

            state.Init(factory.BattleData);

            Assert.IsFalse(state.IsDone);
        }

        [TestCase]
        public void Init_NoPKMNAreKO_IsDoneReturnsTrue()
        {
            var state = CreateWaitForCharState();
            factory.CreatePlayerPokemon();
            factory.CreateAIPokemon();

            state.Init(factory.BattleData);

            Assert.IsTrue(state.IsDone);
        }

        [TestCase]
        public void SetMove_SomeData_ThrowsException()
        {
            var state = CreateWaitForCharState(factory.BattleData);

            Assert.Throws<InvalidOperationException>(() => 
                state.SetMove(factory.PlayerID, factory.AIID, factory.CreateMove())
                );
        }

        [TestCase]
        public void SetItem_SomeData_ThrowsException()
        {
            var state = CreateWaitForCharState(factory.BattleData);

            Assert.Throws<InvalidOperationException>(() => 
                state.SetItem(factory.PlayerID, factory.AIID, factory.CreateItem())
                );
        }

        [TestCase]
        public void SetCharacter_IdNeedsNoChar_ThrowsException()
        {
            factory.CreatePlayerPokemon();
            factory.CreateAIPokemon(StatusCondition.KO);

            var state = CreateWaitForCharState(factory.BattleData);

            Assert.Throws<InvalidOperationException>(() => 
                state.SetCharacter(factory.PlayerID, factory.CreatePokemon())
                );

        }

        [TestCase]
        public void SetCharacter_NeededChar_UpdateDoesNotChangeBattleData()
        {
            var state = CreateWaitForCharState(factory.BattleData);

            SetNewPlayerCharacter(state);
            state.Update(factory.BattleData);

            var playerPkmn = factory.GetPlayerPokemon();
            Assert.IsNull(playerPkmn);
        }

        [TestCase]
        public void Update_AfterSettingAllCharacters_IsDoneReturnsTrue()
        {
            var state = CreateWaitForCharState(factory.BattleData);

            SetNewPlayerCharacter(state);
            SetNewAICharacter(state);
            state.Update(factory.BattleData);

            Assert.IsTrue(state.IsDone);
        }

        [TestCase]
        public void Update_AfterSettingSomeCharacters_IsDoneReturnsFalse()
        {
            var state = CreateWaitForCharState(factory.BattleData);

            SetNewPlayerCharacter(state);
            state.Update(factory.BattleData);

            Assert.IsFalse(state.IsDone);
        }

        [TestCase]
        public void Init_SomePKMNAreNull_IsDoneReturnsFalse()
        {
            factory.CreateAIPokemon();
            var state = CreateWaitForCharState();

            state.Init(factory.BattleData);

            Assert.IsFalse(state.IsDone);
        }

        [TestCase]
        public void Update_SetAllCharacters_BattleDataContainsTheRightCharacters()
        {
            var state = CreateWaitForCharState(factory.BattleData);
            var playerCharacter = factory.CreatePokemon();
            var aiCharacter = factory.CreatePokemon();

            SetNewPlayerCharacter(state, playerCharacter);
            SetNewAICharacter(state, aiCharacter);
            state.Update(factory.BattleData);

            Assert.AreEqual(playerCharacter, factory.GetPlayerPokemon());
            Assert.AreEqual(aiCharacter, factory.GetAIPokemon());
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

        private void SetNewPlayerCharacter(WaitForCharState state, Pokemon pokemon = null)
        {
            SetNewCharacter(state, factory.PlayerID, pokemon);
        }
        private void SetNewAICharacter(WaitForCharState state, Pokemon pokemon = null)
        {
            SetNewCharacter(state, factory.AIID, pokemon);
        }

        private void SetNewCharacter(WaitForCharState state, ClientIdentifier id, Pokemon pokemon = null){
            if(pokemon == null)
                pokemon = factory.CreatePokemon();

            state.SetCharacter(id, pokemon);
        }
    }
}
