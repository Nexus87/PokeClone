using System;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Utils;
using Microsoft.Xna.Framework;

namespace BattleMode.Core.Components.GraphicComponents
{
    public class HpResizeAnimation : IAnimation
    {
        private int _currentHp;
        private readonly HpLine _line;
        private readonly bool _lower;
        private readonly int _targetHp;

        public HpResizeAnimation(int startHp, int targetHp, HpLine line)
        {
            _targetHp = targetHp;
            _line = line;
            _currentHp = startHp;

            _lower = startHp > targetHp;
        }

        public HpResizeAnimation(int targetHp, HpLine line)
            : this(line.Current, targetHp, line)
        {
        }

        public event EventHandler AnimationFinished;

        public void Update(GameTime time, IGraphicComponent component)
        {
            if (_targetHp == _currentHp)
            {
                AnimationFinished?.Invoke(this, null);
                _currentHp = 0;
                return;
            }

            if (!time.ElapsedGameTime.TotalMilliseconds.AlmostEqual(0))
                return;

            if (_lower)
                _currentHp = Math.Max(_currentHp - 1, _targetHp);
            else
                _currentHp = Math.Min(_currentHp + 1, _targetHp);

            _line.Current = _currentHp;
        }
    }
}