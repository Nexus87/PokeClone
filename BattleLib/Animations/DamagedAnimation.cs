using GameEngine.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleLib.Animations
{
    public class DamagedAnimation  : IAnimation
    {
        public event EventHandler AnimationFinished;
        private long blinkTime;
        private bool firstRun = true;
        private GameTime lastTime;
        private int numberOfBlinks;
        private int currentNumberOfBlinks;

        public DamagedAnimation(long blinkTime, int numberOfBlinks)
        {
            this.blinkTime = blinkTime;
            this.numberOfBlinks = numberOfBlinks;
        }
        public void Update(GameTime time, IGraphicComponent component)
        {
            if (!IsTimeIntervalOfer(time, lastTime))
                return;

            lastTime = time;
            component.IsVisible = !component.IsVisible;
            currentNumberOfBlinks++;

            if (AnimationDone())
                AnimationFinished(this, EventArgs.Empty);
        }

        private bool AnimationDone()
        {
            return currentNumberOfBlinks == 2 * numberOfBlinks;
        }

        private bool IsTimeIntervalOfer(GameTime time, GameTime lastTime)
        {
            if (firstRun)
            {
                firstRun = false;
                return true;
            }

            var diff = time.TotalGameTime - lastTime.TotalGameTime;
            return diff.Milliseconds >= blinkTime;
        }
    }
}
