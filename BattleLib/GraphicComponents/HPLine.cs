﻿using GameEngine;
using GameEngine.Graphics;
using GameEngine.Graphics.Basic;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;

namespace BattleLib.GraphicComponents
{
    public interface ILine : IGraphicComponent
    {
        Color Color { get; set; }
    }

    public class DefaultLine : Line, ILine
    {
        public DefaultLine(PokeEngine game) : base(game) { }
    }

    public class HPLine : AbstractGraphicComponent
    {
        private const int border = 10;
        private int currentHp = 0;
        private ILine hpLine;
        private ILine innerLine;
        private int maxHp = 0;
        private ILine outerLine;

        public HPLine(PokeEngine game)
            : this(new DefaultLine(game), new DefaultLine(game), new DefaultLine(game), game)
        { }
        public HPLine(ILine outerLine, ILine innerLine, ILine hpLine, PokeEngine game) :
            base(game)
        {
            this.outerLine = outerLine;
            this.innerLine = innerLine;
            this.hpLine = hpLine;
            outerLine.Color = Color.Black;
            innerLine.Color = PokeEngine.BackgroundColor;
        }

        public event EventHandler AnimationDone = delegate { };

        public int Current { get { return currentHp; } set { currentHp = value; Invalidate(); } }

        public int MaxHP { get { return maxHp; } set { maxHp = value; Invalidate(); } }

        public void AnimationSetHP(int newHP)
        {
            var animation = new HPResizeAnimation(newHP, this);
            animation.AnimationFinished += animation_AnimationFinished;
            PlayAnimation(animation);
        }

        public override void Setup()
        {
            outerLine.Setup();
            innerLine.Setup();
            hpLine.Setup();
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            outerLine.Draw(time, batch);
            innerLine.Draw(time, batch);
            hpLine.Draw(time, batch);
        }

        protected override void Update()
        {
            outerLine.XPosition = XPosition;
            outerLine.YPosition = YPosition;
            outerLine.Width = Width;
            outerLine.Height = Height;

            innerLine.XPosition = XPosition + border;
            innerLine.YPosition = YPosition + border;
            innerLine.Width = Math.Max(0, Width - 2 * border);
            innerLine.Height = Math.Max(0, Height - 2 * border);

            float factor = maxHp == 0 ? 0 : ((float)currentHp) / ((float)maxHp);

            hpLine.XPosition = XPosition + border;
            hpLine.YPosition = YPosition + border;
            hpLine.Width = Math.Max(0, factor * (Width - 2 * border));
            hpLine.Height = Math.Max(0, Height - 2 * border);

            if (factor >= 0.50f)
                hpLine.Color = Color.Green;
            else if (factor >= 0.25f)
                hpLine.Color = Color.Yellow;
            else
                hpLine.Color = Color.Red;
        }

        private void animation_AnimationFinished(object sender, EventArgs e)
        {
            var animation = (IAnimation)sender;
            animation.AnimationFinished -= animation_AnimationFinished;
            AnimationDone(this, null);
        }
    }

    public class HPResizeAnimation : IAnimation
    {
        private int currentHP = 0;
        private HPLine line;
        private bool lower;
        private int startHP;
        private int targetHP;

        public HPResizeAnimation(int startHP, int targetHP, HPLine line)
        {
            this.startHP = startHP;
            this.targetHP = targetHP;
            this.line = line;
            this.currentHP = startHP;

            if (startHP > targetHP)
                lower = true;
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

            if (time.ElapsedGameTime.TotalMilliseconds != 0)
                return;

            if (lower)
                currentHP = Math.Max(currentHP - 1, targetHP);
            else
                currentHP = Math.Min(currentHP + 1, targetHP);

            line.Current = currentHP;
        }
    }
}