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

        public DamagedAnimation(long blinkTime)
        {
            this.blinkTime = blinkTime;
        }
        public void Update(GameTime time, IGraphicComponent component)
        {
            if (firstRun)
            {
                lastTime = time;
                firstRun = false;
                component.IsVisible = false;
                return;
            }

            var diff = time.TotalGameTime - lastTime.TotalGameTime;
            
            if (diff.Ticks < blinkTime)
                return;

            lastTime = time;
            component.IsVisible = !component.IsVisible;
        }
    }
}
