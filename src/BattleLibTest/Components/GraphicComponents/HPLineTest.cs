using System;
using System.Collections.Generic;
using BattleMode.Core.Components.GraphicComponents;
using FakeItEasy;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Utils;
using GameEngineTest.Graphics;
using GameEngineTest.TestUtils;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace BattleModeTest.Components.GraphicComponents
{
    [TestFixture]
    public class HpLineTest : IGraphicComponentTest
    {

        private static HpLine CreateLine()
        {
            var hpLineStub = A.Fake<IGraphicComponent>();

            return CreateLine(hpLineStub);
        }

        private static HpLine CreateLine(IGraphicComponent hpLine)
        {
            var outerLine = A.Fake<IGraphicComponent>();
            var innerLine = A.Fake<IGraphicComponent>();
            var line = new HpLine(outerLine, innerLine, hpLine, Color.White) {MaxHp = 100};
            line.Setup();

            return line;
        }

        public static List<TestCaseData> HpTestData = new List<TestCaseData>
        {
            new TestCaseData(100),
            new TestCaseData(25),
            new TestCaseData(0),
        };

        [TestCaseSource(nameof(HpTestData))]
        public void AnimationSetHP_WaitTillAnimationFinished_CurrentHPIsAsExpected(int hp)
        {
            var animationDone = false;
            var line = CreateLine();
            line.SetCoordinates(0, 0, 300, 300);
            line.AnimationDone += delegate { animationDone = true; };
            
            line.AnimationSetHp(hp);

            while (!animationDone)
                line.Draw();

            Assert.AreEqual(hp, line.Current);
        }

        [TestCase(110)]
        [TestCase(-10)]
        public void AnimationSetHP_SetInvalidData_ThrowsException(int hp)
        {
            var line = CreateLine();
            line.SetCoordinates(0, 0, 300, 300);

            Assert.Throws<ArgumentOutOfRangeException>(() => line.AnimationSetHp(hp));
        }

        public static List<TestCaseData> HpColorTestData = new List<TestCaseData>
        {
            new TestCaseData(100, Color.Green),
            new TestCaseData(50, Color.Green),
            new TestCaseData(49, Color.Yellow),
            new TestCaseData(25, Color.Yellow),
            new TestCaseData(24, Color.Red)
        };

        [TestCaseSource(nameof(HpColorTestData))]
        public void Draw_SetNumberOfHp_HPLineHasExpectedColor(int hp, Color color)
        {
            var hpLineStub = A.Fake<IGraphicComponent>();
            var line = CreateLine(hpLineStub);
            line.SetCoordinates(0, 0, 500, 500);

            line.Current = hp;
            line.Draw();

            A.CallToSet(() => hpLineStub.Color).To(color).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestCase]
        public void Draw_ZeroMaxHP_DoesNotThrow()
        {
            var line = CreateLine();
            line.SetCoordinates(0, 0, 500, 500);

            line.MaxHp = 0;
            line.Current = 0;

            Assert.DoesNotThrow(()=> line.Draw());
        }

        [TestCase]
        public void Draw_ZeroMaxHP_HPLineHasZeroSize()
        {
            var hpLine = A.Fake<IGraphicComponent>();
            hpLine.Height(-1.0f);
            hpLine.Width(1.0f);
            var line = CreateLine(hpLine);
            
            line.SetCoordinates(0, 0, 500, 500);

            line.MaxHp = 0;
            line.Current = 0;

            line.Draw();

            Assert.IsTrue(hpLine.Width().AlmostEqual(0) || hpLine.Height().AlmostEqual(0));
            
        }

        [TestCase(110)]
        [TestCase(-10)]
        public void Current_SetOutOfBound_ThrowsException(int hp)
        {
            var line = CreateLine();

            Assert.Throws<ArgumentOutOfRangeException>(() => line.Current = hp);
        }

        [TestCase(100, 90, 90)]
        [TestCase(80, 70, 70)]
        [TestCase(20, 0, 0)]
        public void Current_LowerMaxHp_IsChangedToFitInRange(int initalHp, int maxHp, int resultHp)
        {
            var line = CreateLine();
            line.Current = initalHp;
            line.MaxHp = maxHp;

            Assert.AreEqual(resultHp, line.Current);
        }

        [TestCase(-10)]
        public void MaxHP_SetOutOfBound_ThrowsException(int hp)
        {
            var line = CreateLine();

            Assert.Throws<ArgumentOutOfRangeException>(() => line.MaxHp = hp);
        }

        protected override IGraphicComponent CreateComponent()
        {
            return CreateLine();
        }
    }
}
