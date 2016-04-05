using BattleLib.GraphicComponents;
using GameEngine.Graphics;
using GameEngineTest;
using GameEngineTest.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLibTest.GraphicComponents
{
    [TestFixture]
    public class HPLineTest : IGraphicComponentTest
    {
        private HPLine line;

        [SetUp]
        public void Setup()
        {
            line = new HPLine(gameMock.Object);
            line.MaxHP = 100;
            line.Current = 50;
            line.Setup();

        }

        public static List<TestCaseData> HPTestData = new List<TestCaseData>
        {
            new TestCaseData(100),
            new TestCaseData(25),
            new TestCaseData(0),
            new TestCaseData(110),
            new TestCaseData(-5)
        };

        [TestCaseSource("HPTestData")]
        public void HPAnimationTest(int hp)
        {
            line.XPosition = 300;
            line.YPosition = 300;
            line.Width = 300;
            line.Height = 300;

            Func<int, int> change;
            if (hp < line.Current)
                change = i => i - 1;
            else
                change = i => i + 1;

            int currentHP = line.Current;
            var spriteBatch = new SpriteBatchMock();
            bool animationDone = false;
            line.AnimationDone += delegate { animationDone = true; };
            
            line.AnimationSetHP(hp);

            while (!animationDone)
            {
                line.Draw(spriteBatch);

                Assert.GreaterOrEqual(line.Current, 0);
                Assert.LessOrEqual(line.Current, line.MaxHP);
            }
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
        public void ColorTest(int hp, Color color)
        {
            var spriteBatch = new SpriteBatchMock();

            int maxHP = 100;

            line.Width = 500;
            line.Height = 500;

            line.MaxHP = maxHP;
            line.Current = hp;

            line.Draw(spriteBatch);

            var coloredLines = (from obj in spriteBatch.DrawnObjects where obj.Color == color select obj).Count();

            Assert.AreNotEqual(0, coloredLines);
        }

        [TestCase]
        public void ZeroMaxHPTest()
        {
            var spriteBatch = new SpriteBatchMock();
            line.SetCoordinates(0, 0, 500, 500);

            line.MaxHP = 0;
            line.Current = 0;

            line.Draw(spriteBatch);
            foreach (var obj in spriteBatch.DrawnObjects)
                obj.IsInConstraints(line);
        }

        protected override IGraphicComponent CreateComponent()
        {
            line = new HPLine(gameMock.Object);
            line.MaxHP = 100;
            line.Current = 50;
            line.Setup();

            return line;
        }
    }
}
