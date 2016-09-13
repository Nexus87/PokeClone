using System;
using GameEngine.Graphics;
using GameEngine.Utils;
using Microsoft.Xna.Framework;

namespace BattleLib.GraphicComponents
{
    public class HPResizeAnimation : IAnimation
    {
        int currentHP;
        readonly HPLine line;
        readonly bool lower;
        readonly int startHP;
        readonly int targetHP;

        public HPResizeAnimation(int startHP, int targetHP, HPLine line)
        {
            this.startHP = startHP;
            this.targetHP = targetHP;
            this.line = line;
            currentHP = startHP;

            lower = startHP > targetHP;
        }

        public HPResizeAnimation(int targetHP, HPLine line)
            : this(line.Current, targetHP, line)
        {
        }

        public event EventHandler AnimationFinished;

        public void Update(GameTime time, IGraphicComponent component)
        {
            if (targetHP == currentHP)
            {
                AnimationFinished(this, null);
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