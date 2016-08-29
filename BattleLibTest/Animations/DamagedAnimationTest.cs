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
        [TestCase(10)]
        //[TestCase(1000)]
        public void Update_NormalUsage_BlinkingTakesGivenTime(long time)
        {
            var animation = CreateAnimation(time);
            var graphicMock = new GraphicComponentMock();
            var startTime = new GameTime();
            var endTime = CreateNextTime(time, startTime);

            animation.Update(startTime, graphicMock);
            graphicMock.Draw();

            Assert.False(graphicMock.WasDrawn);

            animation.Update(endTime, graphicMock);
            graphicMock.Draw();

            Assert.True(graphicMock.WasDrawn);
            
            
        }

        [TestCase(10, 10, true)]
        [TestCase(9, 10, false)]
        [TestCase(11, 10, true)]
        public void Update_WithDifferentTimes_GraphicComponentVisibilityIsAsExpected(long time, long animationTime, bool expectedVisibility)
        {
            var animation = CreateAnimation(animationTime);
            var graphicMock = new GraphicComponentMock();
            var startTime = new GameTime();
            var endTime = CreateNextTime(time, startTime);

            animation.Update(startTime, graphicMock);
            animation.Update(endTime, graphicMock);
            graphicMock.Draw();

            Assert.AreEqual(expectedVisibility, graphicMock.IsVisible);
        }

        [TestCase(10, 3)]
        public void Update_WithGivenNumberOfBlinks_BlinksForGivenTimes(long blinkTime, int number)
        {
            var animation = CreateAnimation(blinkTime, number);
            var graphicMock = new GraphicComponentMock();
            var nextTime = new GameTime();
            bool animationFinished = false;
            int numberOfBlinks = 0;
            animation.AnimationFinished += delegate { animationFinished = true; };
            graphicMock.VisibilityChanged += delegate { numberOfBlinks++; };

            while (!animationFinished && numberOfBlinks < 2 * number)
            {
                animation.Update(nextTime, graphicMock);
                nextTime = CreateNextTime(blinkTime, nextTime);
            }

            Assert.True(animationFinished);
            Assert.AreEqual(numberOfBlinks, 2 * number);
        }

        private static GameTime CreateNextTime(long time, GameTime startTime)
        {
            var endElapsedGameTime = new TimeSpan(0, 0, 0, 0, startTime.ElapsedGameTime.Milliseconds + (int) time);
            var endElapsedToalGameTime = new TimeSpan(0, 0, 0, 0, startTime.TotalGameTime.Milliseconds + (int) time);
            var endTime = new GameTime(endElapsedToalGameTime, endElapsedGameTime);
            return endTime;
        }

        private DamagedAnimation CreateAnimation(long time, int number = 5)
        {
            return new DamagedAnimation(time, number);
        }
    }
}
