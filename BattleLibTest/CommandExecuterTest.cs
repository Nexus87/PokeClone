using Base;
using Base.Rules;
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
    public class MoveEffectCalculatorStub : IMoveEffectCalculator
    {

        public void Init(IBattlePokemon source, Move move, IBattlePokemon target)
        {
        }

        public bool IsHit { get; set; }
        public bool IsCritical { get; set; }
        public float TypeModifier { get; set; }
        public int Damage { get; set; }
    }

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

        private void ExecuteMoveCommand(CommandExecuter executer, ClientIdentifier clientIdentifier, int damage)
        {
            var command = new MoveCommand(clientIdentifier, clientIdentifier, factory.CreateMove());
            calculator.Damage = damage;
            executer.DispatchCommand(command);
        }

        private void ExecuteMoveCommand(CommandExecuter executer, bool critical = false, float typeModifier = 1)
        {
            var command = new MoveCommand(factory.PlayerID, factory.PlayerID, factory.CreateMove());
            calculator.IsCritical = critical;
            calculator.TypeModifier = typeModifier;
            executer.DispatchCommand(command);
        }

        private CommandExecuter CreateExecuter()
        {
            return new CommandExecuter(calculator, creatorMock.Object) { Data = factory.BattleData };
        }
    }
}
