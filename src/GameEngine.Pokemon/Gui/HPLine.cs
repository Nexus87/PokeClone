using System;
using GameEngine.Graphics.General;
using GameEngine.GUI;
using GameEngine.Pokemon.Gui.Renderer;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace GameEngine.Pokemon.Gui
{
    [GameType]
    public class HpLine : AbstractGuiComponent
    {
        private readonly HpLineRenderer _renderer;


        private int _currentHp;
        private int _maxHp;


        public HpLine(HpLineRenderer renderer)
        {
            _renderer = renderer;
        }

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
    }
}