using GameEngine.Graphics;
using Microsoft.Xna.Framework;
using System;
using GameEngine.GUI.Graphics;

namespace BattleLib.Animations
{
    public class DamagedAnimation  : IAnimation
    {
        public event EventHandler AnimationFinished = delegate { };
        private readonly long blinkTime;
        private bool firstRun = true;
        private GameTime lastTime;
        private readonly int numberOfBlinks;
        private int currentNumberOfBlinks;

        public DamagedAnimation(long blinkTime, int numberOfBlinks)
        {
            this.blinkTime = blinkTime;
            this.numberOfBlinks = numberOfBlinks;
        }
        public void Update(GameTime time, IGraphicComponent component)
        {
            if (!IsTimeIntervalOver(time, lastTime))
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

        private bool IsTimeIntervalOver(GameTime time, GameTime previousTime)
        {
            if (firstRun)
            {
                firstRun = false;
                return true;
            }

            var diff = time.TotalGameTime - previousTime.TotalGameTime;
            return diff.Milliseconds >= blinkTime;
        }
    }
}
