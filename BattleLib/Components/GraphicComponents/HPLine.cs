using System;
using GameEngine;
using GameEngine.Graphics;
using GameEngine.Registry;
using Microsoft.Xna.Framework;

namespace BattleLib.Components.GraphicComponents
{
    [GameType]
    public class HPLine : AbstractGraphicComponent
    {
        private const float RelativeBorderSize = 0.2f;
        private float BorderSize { get { return RelativeBorderSize * Height; } }
        private int currentHp;
        private readonly IGraphicComponent hpLine;
        private readonly IGraphicComponent innerLine;
        private int maxHp;
        private readonly IGraphicComponent outerLine;


        public HPLine(Line outerLine, Line innerLine, Line hpLine, ScreenConstants screen) :
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

            var animation = new HpResizeAnimation(newHP, this);
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
            hpLine.XPosition = XPosition + BorderSize;
            hpLine.YPosition = YPosition + BorderSize;
            hpLine.Width = Math.Max(0, factor * (Width - 2 * BorderSize));
            hpLine.Height = Math.Max(0, Height - 2 * BorderSize);
        }

        private void SetInnerLineCoordinates()
        {
            innerLine.XPosition = XPosition + BorderSize;
            innerLine.YPosition = YPosition + BorderSize;
            innerLine.Width = Math.Max(0, Width - 2 * BorderSize);
            innerLine.Height = Math.Max(0, Height - 2 * BorderSize);
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
}