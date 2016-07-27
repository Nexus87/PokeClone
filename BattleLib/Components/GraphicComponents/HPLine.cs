using GameEngine;
using GameEngine.Graphics;
using GameEngine.Registry;
using Microsoft.Xna.Framework;
using System;

namespace BattleLib.GraphicComponents
{
    [GameType]
    public class HPLine : AbstractGraphicComponent
    {
        private const int LINE_BORDER_SIZE = 10;
        private int currentHp = 0;
        private IGraphicComponent hpLine;
        private IGraphicComponent innerLine;
        private int maxHp = 0;
        private IGraphicComponent outerLine;


        public HPLine(Line outerLine, Line innerLine, Line hpLine, Screen screen) :
            this(outerLine, innerLine, hpLine, screen.BackgroundColor)
        {}
        internal HPLine(IGraphicComponent outerLine, IGraphicComponent innerLine, IGraphicComponent hpLine, Color backgroundColor)
        {
            this.outerLine = outerLine;
            this.innerLine = innerLine;
            this.hpLine = hpLine;
            outerLine.Color = Color.Black;
            innerLine.Color = backgroundColor;
        }

        public event EventHandler AnimationDone = delegate { };

        public int Current { 
            get { return currentHp; }
            set
            {
                if (value < 0 || value > maxHp)
                    throw new ArgumentOutOfRangeException("value");
                currentHp = value; 
                Invalidate();
            } 
        }

        public int MaxHP { 
            get { return maxHp; } 
            set 
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("value");
                maxHp = value;
                if (Current > maxHp)
                    Current = maxHp;
                Invalidate(); 
            } 
        }

        public void AnimationSetHP(int newHP)
        {
            if (newHP < 0 || newHP > maxHp)
                throw new ArgumentOutOfRangeException("newHP");

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
            SetOuterLineCoordinates();
            SetInnerLineCoordinates();

            float factor = maxHp == 0 ? 0 : ((float)currentHp) / ((float)maxHp);
            SetHPLineCoordinates(factor);
            SetHPLineColor(factor);
        }

        private void SetHPLineCoordinates(float factor)
        {
            hpLine.XPosition = XPosition + LINE_BORDER_SIZE;
            hpLine.YPosition = YPosition + LINE_BORDER_SIZE;
            hpLine.Width = Math.Max(0, factor * (Width - 2 * LINE_BORDER_SIZE));
            hpLine.Height = Math.Max(0, Height - 2 * LINE_BORDER_SIZE);
        }

        private void SetInnerLineCoordinates()
        {
            innerLine.XPosition = XPosition + LINE_BORDER_SIZE;
            innerLine.YPosition = YPosition + LINE_BORDER_SIZE;
            innerLine.Width = Math.Max(0, Width - 2 * LINE_BORDER_SIZE);
            innerLine.Height = Math.Max(0, Height - 2 * LINE_BORDER_SIZE);
        }

        private void SetOuterLineCoordinates()
        {
            outerLine.XPosition = XPosition;
            outerLine.YPosition = YPosition;
            outerLine.Width = Width;
            outerLine.Height = Height;
        }

        private void SetHPLineColor(float factor)
        {
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