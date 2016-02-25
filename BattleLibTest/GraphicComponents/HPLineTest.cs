using BattleLib.GraphicComponents;
using GameEngineTest;
using GameEngineTest.Util;
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
            contentMock.SetupLoad();
            line = new HPLine(gameMock.Object);
            line.MaxHP = 100;
            line.Current = 50;
            line.Setup(contentMock.Object);

            testObj = line;
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
            line.X = 300;
            line.Y = 300;
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
    }
}
