using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleLib.Animations;
using GameEngineTest.TestUtils;
using Microsoft.Xna.Framework;

namespace BattleLibTest.Animations
{
    [TestFixture]
    public class DamagedAnimationTest
    {
        [TestCase(1000000)]
        public void Update_NormalUsage_BlinkingTakesGivenTime(long time)
        {
            var animation = CreateAnimation(time);
            var graphicMock = new GraphicComponentMock();
            var startTime = new GameTime();
            var endTime = new GameTime(new TimeSpan(startTime.ElapsedGameTime.Ticks + time), new TimeSpan(startTime.TotalGameTime.Ticks + time));

            animation.Update(startTime, graphicMock);
            graphicMock.Draw();

            Assert.False(graphicMock.WasDrawn);

            animation.Update(endTime, graphicMock);
            graphicMock.Draw();

            Assert.True(graphicMock.WasDrawn);
            
            
        }

        private DamagedAnimation CreateAnimation(long time)
        {
            return new DamagedAnimation(time);
        }
    }
}
