using Base.Data;
using BattleLib;
using BattleLib.Components.BattleState;
using BattleLib.Components.BattleState.Commands;
using BattleLibTest.Utils;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLibTest
{
    [TestFixture]
    public class CommandExecuterTest
    {
        private MoveEffectCalculatorStub calculator;
        private Mock<IEventCreator> creatorMock;
        private TestFactory factory;

        [SetUp]
        public void Setup()
        {
            calculator = new MoveEffectCalculatorStub();
            creatorMock = new Mock<IEventCreator>();
            factory = new TestFactory();
        }
        
        [TestCase(100, 10, 90)]
        public void ExecuteMove_NonCritical_PokemonHPReduces(int HP, int damage, int result)
        {
            var executer = CreateExecuter();
            factory.CreateAllPokemon(HP: HP);
            
            ExecuteMoveCommand(executer, factory.PlayerID, damage);

            Assert.AreEqual(result, factory.GetPlayerPokemon().HP);
        }

        [TestCase(100, 10, 90)]
        public void ExecuteMove_NonCritical_EventCreatorSetHPCalled(int HP, int damage, int result)
        {
            var executer = CreateExecuter();
            factory.CreateAllPokemon(HP: HP);

            ExecuteMoveCommand(executer, factory.PlayerID, damage);

            creatorMock.Verify(c => c.SetHP(factory.PlayerID, result), Times.Once);
        }

        [Test]
        public void ExecuteMove_Critical_EventCreatorCriticalCalled()
        {
            var executer = CreateExecuter();
            factory.CreateAllPokemon();

            ExecuteMoveCommand(executer, critical: true);

            creatorMock.Verify(c => c.Critical(), Times.Once);
        }

        [Test]
        public void ExecuteMove_Critical_EventCreatorCriticalNotCalled()
        {
            var executer = CreateExecuter();
            factory.CreateAllPokemon();

            ExecuteMoveCommand(executer, critical: false);

            creatorMock.Verify(c => c.Critical(), Times.Never);
        }

        [TestCase(1.1f, MoveEfficiency.VeryEffective)]
        [TestCase(1.0f, MoveEfficiency.Normal)]
        [TestCase(0.9f, MoveEfficiency.NotEffective)]
        [TestCase(0.0f, MoveEfficiency.NoEffect)]
        public void ExecuteMove_MoveEffective_EventCreatorEffectivCalled(float modifier, MoveEfficiency expected)
        {
            var executer = CreateExecuter();
            factory.CreateAllPokemon();

            ExecuteMoveCommand(executer, typeModifier: modifier);

            creatorMock.Verify(c => c.Effective(expected, It.IsAny<PokemonWrapper>()), Times.Once);
        }

        [TestCase(StatusCondition.Normal)]
        [TestCase(StatusCondition.KO)]
        [TestCase(StatusCondition.Paralyzed)]
        public void ExecuteMove_ChangingStatusCondition_PokemonConditionChanged(StatusCondition condition)
        {
            var executer = CreateExecuter();
            factory.CreateAllPokemon();

            ExecuteMoveCommand(executer, target: factory.PlayerID, newCondition: condition);

            Assert.AreEqual(condition, factory.GetPlayerPokemon().Condition);
        }

        [TestCase(StatusCondition.KO)]
        [TestCase(StatusCondition.Paralyzed)]
        public void ExecuteMove_ChangingStatusCondition_EventCreatorSetStatusCalled(StatusCondition condition)
        {
            var executer = CreateExecuter();
            factory.CreateAllPokemon();

            ExecuteMoveCommand(executer, target: factory.PlayerID, newCondition: condition);

            creatorMock.Verify(c => c.SetStatus(It.IsAny<PokemonWrapper>(), condition), Times.Once);
        }

        [Test]
        public void ExecuteMove_StatusDoesNotChange_EventCreatorSetStatusNotCalled()
        {
            var executer = CreateExecuter();
            factory.CreateAllPokemon();

            ExecuteMoveCommand(executer, target: factory.PlayerID, newCondition: StatusCondition.Normal);

            creatorMock.Verify(c => c.SetStatus(It.IsAny<PokemonWrapper>(), It.IsAny<StatusCondition>()), Times.Never);
        }

        private void ExecuteMoveCommand(CommandExecuter executer, ClientIdentifier clientIdentifier, int damage)
        {
            var command = new MoveCommand(clientIdentifier, clientIdentifier, factory.CreateMove());
            calculator.Damage = damage;
            executer.DispatchCommand(command);
        }

        private void ExecuteMoveCommand(CommandExecuter executer, bool critical = false, float typeModifier = 1, ClientIdentifier target = null, StatusCondition newCondition = StatusCondition.Normal)
        {
            if (target == null)
                target = factory.PlayerID;
            var command = new MoveCommand(target, target, factory.CreateMove());
            calculator.IsCritical = critical;
            calculator.TypeModifier = typeModifier;
            calculator.StatusCondition = newCondition;
            executer.DispatchCommand(command);
        }

        private CommandExecuter CreateExecuter()
        {
            return new CommandExecuter(calculator, creatorMock.Object) { Data = factory.BattleData };
        }
    }
}
