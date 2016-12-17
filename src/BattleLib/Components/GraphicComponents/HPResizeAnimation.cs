using System;
using GameEngine.Graphics;
using GameEngine.GUI.Graphics;
using GameEngine.Utils;
using Microsoft.Xna.Framework;

namespace BattleLib.Components.GraphicComponents
{
    public class HpResizeAnimation : IAnimation
    {
        private int currentHP;
        private readonly HPLine line;
        private readonly bool lower;
        private readonly int targetHP;

        public HpResizeAnimation(int startHp, int targetHp, HPLine line)
        {
            this.targetHP = targetHp;
            this.line = line;
            currentHP = startHp;

            lower = startHp > targetHp;
        }

        public HpResizeAnimation(int targetHp, HPLine line)
            : this(line.Current, targetHp, line)
        {
        }

        public event EventHandler AnimationFinished;

        public void Update(GameTime time, IGraphicComponent component)
        {
            if (targetHP == currentHP)
            {
                if (AnimationFinished != null) AnimationFinished(this, null);
                currentHP = 0;
                return;
            }

            if (!time.ElapsedGameTime.TotalMilliseconds.AlmostEqual(0))
                return;

            if (lower)
                currentHP = Math.Max(currentHP - 1, targetHP);
            else
                currentHP = Math.Min(currentHP + 1, targetHP);

            line.Current = currentHP;
        }
    }
}