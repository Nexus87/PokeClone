using BattleLib.GraphicComponents;
using GameEngine.Graphics;
using GameEngine.Utils;
using GameEngineTest.Graphics;
using GameEngineTest.TestUtils;
using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BattleLibTest.GraphicComponents
{
    [TestFixture]
    public class HPLineTest : IGraphicComponentTest
    {

        private HPLine CreateLine()
        {
            var hpLineStub = new Mock<IGraphicComponent>();

            return CreateLine(hpLineStub.Object);
        }

        private HPLine CreateLine(IGraphicComponent hpLine)
        {
            var outerLine = new Mock<IGraphicComponent>().Object;
            var innerLine = new Mock<IGraphicComponent>().Object;
            var line = new HPLine(outerLine, innerLine, hpLine, Color.White);
            line.MaxHP = 100;
            line.Setup();

            return line;
        }

        public static List<TestCaseData> HPTestData = new List<TestCaseData>
        {
            new TestCaseData(100),
            new TestCaseData(25),
            new TestCaseData(0),
        };

        [TestCaseSource("HPTestData")]
        public void AnimationSetHP_WaitTillAnimationFinished_CurrentHPIsAsExpected(int hp)
        {
            bool animationDone = false;
            var line = CreateLine();
            line.SetCoordinates(0, 0, 300, 300);
            line.AnimationDone += delegate { animationDone = true; };
            
            line.AnimationSetHP(hp);

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

            Assert.Throws<ArgumentOutOfRangeException>(() => line.AnimationSetHP(hp));
        }

        public static List<TestCaseData> HPColorTestData = new List<TestCaseData>
        {
            new TestCaseData(100, Color.Green),
            new TestCaseData(50, Color.Green),
            new TestCaseData(49, Color.Yellow),
            new TestCaseData(25, Color.Yellow),
            new TestCaseData(24, Color.Red)
        };

        [TestCaseSource("HPColorTestData")]
        public void Draw_SetNumberOfHp_HPLineHasExpectedColor(int hp, Color color)
        {
            var hpLineStub = new Mock<IGraphicComponent>();
            var line = CreateLine(hpLineStub.Object);
            line.SetCoordinates(0, 0, 500, 500);

            line.Current = hp;
            line.Draw();

            hpLineStub.VerifySet(o => o.Color = color, Times.Once);
        }

        [TestCase]
        public void Draw_ZeroMaxHP_DoesNotThrow()
        {
            var line = CreateLine();
            line.SetCoordinates(0, 0, 500, 500);

            line.MaxHP = 0;
            line.Current = 0;

            Assert.DoesNotThrow(()=> line.Draw());
        }

        [TestCase]
        public void Draw_ZeroMaxHP_HPLineHasZeroSize()
        {
            float height = -1.0f;
            float width = -1.0f;
            var hpLine = new Mock<IGraphicComponent>();
            hpLine.SetupSet(o => o.Height = It.IsAny<float>()).Callback<float>(f => height = f);
            hpLine.SetupSet(o => o.Width = It.IsAny<float>()).Callback<float>(f => width = f);
            var line = CreateLine(hpLine.Object);
            
            line.SetCoordinates(0, 0, 500, 500);

            line.MaxHP = 0;
            line.Current = 0;

            line.Draw();

            Assert.IsTrue(height.AlmostEqual(0) || width.AlmostEqual(0));
            
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
        public void Current_LowerMaxHp_IsChangedToFitInRange(int initalHP, int maxHP, int resultHP)
        {
            var line = CreateLine();
            line.Current = initalHP;
            line.MaxHP = maxHP;

            Assert.AreEqual(resultHP, line.Current);
        }

        [TestCase(-10)]
        public void MaxHP_SetOutOfBound_ThrowsException(int hp)
        {
            var line = CreateLine();

            Assert.Throws<ArgumentOutOfRangeException>(() => line.MaxHP = hp);
        }

        protected override IGraphicComponent CreateComponent()
        {
            return CreateLine();
        }
    }
}
