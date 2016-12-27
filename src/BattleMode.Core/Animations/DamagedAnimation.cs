using System;
using GameEngine.GUI.Graphics;
using Microsoft.Xna.Framework;

namespace BattleMode.Core.Animations
{
    public class DamagedAnimation  : IAnimation
    {
        public event EventHandler AnimationFinished = delegate { };
        private readonly long _blinkTime;
        private bool _firstRun = true;
        private GameTime _lastTime;
        private readonly int _numberOfBlinks;
        private int _currentNumberOfBlinks;

        public DamagedAnimation(long blinkTime, int numberOfBlinks)
        {
            _blinkTime = blinkTime;
            _numberOfBlinks = numberOfBlinks;
        }
        public void Update(GameTime time, IGraphicComponent component)
        {
            if (!IsTimeIntervalOver(time, _lastTime))
                return;

            _lastTime = time;
            component.IsVisible = !component.IsVisible;
            _currentNumberOfBlinks++;

            if (AnimationDone())
                AnimationFinished(this, EventArgs.Empty);
        }

        private bool AnimationDone()
        {
            return _currentNumberOfBlinks == 2 * _numberOfBlinks;
        }

        private bool IsTimeIntervalOver(GameTime time, GameTime previousTime)
        {
            if (_firstRun)
            {
                _firstRun = false;
                return true;
            }

            var diff = time.TotalGameTime - previousTime.TotalGameTime;
            return diff.Milliseconds >= _blinkTime;
        }
    }
}
