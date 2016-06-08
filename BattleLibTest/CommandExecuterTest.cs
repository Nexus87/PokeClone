using Base;
using Base.Rules;
using BattleLib;
using BattleLib.Components.BattleState;
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
        private Mock<EventCreator> creatorMock;
        [SetUp]
        public void Setup()
        {
            calculator = new MoveEffectCalculatorStub();
            creatorMock = new Mock<EventCreator>();
        }
        
        private CommandExecuter CreateExecuter()
        {
            return new CommandExecuter(calculator, creatorMock.Object);
        }
    }
}
