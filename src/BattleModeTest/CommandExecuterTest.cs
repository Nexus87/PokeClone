﻿using BattleMode.Entities.BattleState;
using BattleMode.Entities.BattleState.Commands;
using BattleMode.Shared;
using BattleModeTest.Utils;
using FakeItEasy;
using NUnit.Framework;
using PokemonShared.Models;

namespace BattleModeTest
{
    [TestFixture]
    public class CommandExecuterTest
    {
        private MoveEffectCalculatorStub _calculator;
        private IEventCreator _creatorMock;
        private TestFactory _factory;

        [SetUp]
        public void Setup()
        {
            _calculator = new MoveEffectCalculatorStub();
            _creatorMock = A.Fake<IEventCreator>();
            _factory = new TestFactory();
        }

        [TestCase(100, 10, 90)]
        public void ExecuteMove_NonCritical_PokemonHPReduces(int hp, int damage, int result)
        {
            var executer = CreateExecuter();
            _factory.CreateAllPokemon(hp);

            ExecuteMoveCommand(executer, _factory.PlayerId, damage);

            Assert.AreEqual(result, _factory.GetPlayerPokemon().Hp);
        }

        [Test]
        public void ExecuteMove_Critical_EventCreatorCriticalCalled()
        {
            var executer = CreateExecuter();
            _factory.CreateAllPokemon();

            ExecuteMoveCommand(executer, true);

            A.CallTo(() => _creatorMock.Critical()).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void ExecuteMove_Critical_EventCreatorCriticalNotCalled()
        {
            var executer = CreateExecuter();
            _factory.CreateAllPokemon();

            ExecuteMoveCommand(executer);

            A.CallTo(() => _creatorMock.Critical()).MustNotHaveHappened();
        }

        [TestCase(1.1f, MoveEfficiency.VeryEffective)]
        [TestCase(1.0f, MoveEfficiency.Normal)]
        [TestCase(0.9f, MoveEfficiency.NotEffective)]
        [TestCase(0.0f, MoveEfficiency.NoEffect)]
        public void ExecuteMove_MoveEffective_EventCreatorEffectivCalled(float modifier, MoveEfficiency expected)
        {
            var executer = CreateExecuter();
            _factory.CreateAllPokemon();

            ExecuteMoveCommand(executer, typeModifier: modifier);

            A.CallTo(() => _creatorMock.Effective(expected, A<PokemonEntity>.Ignored))
                .MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestCase(StatusCondition.Normal)]
        [TestCase(StatusCondition.KO)]
        [TestCase(StatusCondition.Paralyzed)]
        public void ExecuteMove_ChangingStatusCondition_PokemonConditionChanged(StatusCondition condition)
        {
            var executer = CreateExecuter();
            _factory.CreateAllPokemon();

            ExecuteMoveCommand(executer, target: _factory.PlayerId, newCondition: condition);

            Assert.AreEqual(condition, _factory.GetPlayerPokemon().Condition);
        }

        private void ExecuteMoveCommand(CommandExecuter executer, ClientIdentifier clientIdentifier, int damage)
        {
            var command = new MoveCommand(clientIdentifier, clientIdentifier, _factory.CreateMove());
            _calculator.Damage = damage;
            executer.DispatchCommand(command);
        }

        private void ExecuteMoveCommand(CommandExecuter executer, bool critical = false, float typeModifier = 1,
            ClientIdentifier target = null, StatusCondition newCondition = StatusCondition.Normal)
        {
            if (target == null)
                target = _factory.PlayerId;
            var command = new MoveCommand(target, target, _factory.CreateMove());
            _calculator.IsCritical = critical;
            _calculator.TypeModifier = typeModifier;
            _calculator.StatusCondition = newCondition;
            executer.DispatchCommand(command);
        }

        private CommandExecuter CreateExecuter()
        {
            return new CommandExecuter(_calculator, _creatorMock) {Data = _factory.BattleData};
        }
    }
}