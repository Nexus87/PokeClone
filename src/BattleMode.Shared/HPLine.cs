using System;
using GameEngine.Core;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.General;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace BattleMode.Shared
{
    [GameType]
    public class HpLine : AbstractGraphicComponent
    {
        private const float RelativeBorderSize = 0.2f;
        private float BorderSize => RelativeBorderSize * Area.Height;
        private int _currentHp;
        private readonly IGraphicComponent _hpLine;
        private readonly IGraphicComponent _innerLine;
        private int _maxHp;
        private readonly IGraphicComponent _outerLine;


        public HpLine(Line outerLine, Line innerLine, Line hpLine, ScreenConstants screen) :
            this(outerLine, innerLine, hpLine, screen.BackgroundColor)
        {}
        internal HpLine(IGraphicComponent outerLine, IGraphicComponent innerLine, IGraphicComponent hpLine, Color backgroundColor)
        {
            _outerLine = outerLine;
            _innerLine = innerLine;
            _hpLine = hpLine;
            outerLine.Color = Color.Black;
            innerLine.Color = backgroundColor;
        }

        public event EventHandler AnimationDone = delegate { };

        public int Current { 
            get { return _currentHp; }
            set
            {
                if (value < 0 || value > _maxHp)
                    throw new ArgumentOutOfRangeException(nameof(value));
                _currentHp = value;
                Invalidate();
            } 
        }

        public int MaxHp {
            get { return _maxHp; }
            set 
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value));
                _maxHp = value;
                if (Current > _maxHp)
                    Current = _maxHp;
                Invalidate(); 
            } 
        }

        public void AnimationSetHp(int newHp)
        {
            if (newHp < 0 || newHp > _maxHp)
                throw new ArgumentOutOfRangeException(nameof(newHp));

            var animation = new HpResizeAnimation(newHp, this);
            animation.AnimationFinished += animation_AnimationFinished;
            PlayAnimation(animation);
        }

        public override void Setup()
        {
            _outerLine.Setup();
            _innerLine.Setup();
            _hpLine.Setup();
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            _outerLine.Draw(time, batch);
            _innerLine.Draw(time, batch);
            _hpLine.Draw(time, batch);
        }

        protected override void Update()
        {
            SetOuterLineCoordinates();
            SetInnerLineCoordinates();

            float factor = _maxHp == 0 ? 0 : _currentHp / ((float)_maxHp);
            SetHpLineCoordinates(factor);
            SetHpLineColor(factor);
        }

        private void SetHpLineCoordinates(float factor)
        {
            var xPosition = Area.X + BorderSize;
            var yPosition = Area.Y + BorderSize;
            var width = Math.Max(0, factor * (Area.Width - 2 * BorderSize));
            var height = Math.Max(0, Area.Height - 2 * BorderSize);

            _hpLine.SetCoordinates(xPosition, yPosition, width, height);
        }

        private void SetInnerLineCoordinates()
        {
            var xPosition = Area.X + BorderSize;
            var yPosition = Area.Y + BorderSize;
            var width = Math.Max(0, Area.Width - 2 * BorderSize);
            var height = Math.Max(0, Area.Height - 2 * BorderSize);
            _innerLine.SetCoordinates(xPosition, yPosition, width, height);
        }

        private void SetOuterLineCoordinates()
        {
            var xPosition = Area.X;
            var yPosition = Area.Y;
            var width = Area.Width;
            var height = Area.Height;
            _outerLine.SetCoordinates(xPosition, yPosition, width, height);
        }

        private void SetHpLineColor(float factor)
        {
            if (factor >= 0.50f)
                _hpLine.Color = Color.Green;
            else if (factor >= 0.25f)
                _hpLine.Color = Color.Yellow;
            else
                _hpLine.Color = Color.Red;
        }

        private void animation_AnimationFinished(object sender, EventArgs e)
        {
            var animation = (IAnimation)sender;
            animation.AnimationFinished -= animation_AnimationFinished;
            AnimationDone(this, null);
        }
    }
}