using System;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.General;
using GameEngine.GUI.Renderers;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Components
{
    [GameType]
    public class HpLine : AbstractGraphicComponent
    {
        private readonly IHpLineRenderer _renderer;


        private int _currentHp;
        private int _maxHp;


        public HpLine(IHpLineRenderer renderer)
        {
            _renderer = renderer;
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

        public Color Color { get; set; }

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
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            _renderer.Render(batch, this);
        }

        protected override void Update()
        {
            var factor = _maxHp == 0 ? 0 : _currentHp / ((float)_maxHp);
            SetHpLineColor(factor);
        }

        private void SetHpLineColor(float factor)
        {
            if (factor >= 0.50f)
                Color = Color.Green;
            else if (factor >= 0.25f)
                Color = Color.Yellow;
            else
                Color = Color.Red;
        }

        private void animation_AnimationFinished(object sender, EventArgs e)
        {
            var animation = (IAnimation)sender;
            animation.AnimationFinished -= animation_AnimationFinished;
            AnimationDone(this, null);
        }
    }
}