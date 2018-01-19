using System;
using GameEngine.Graphics.General;
using GameEngine.GUI;
using Microsoft.Xna.Framework;
using PokemonShared.Gui.Renderer;

namespace PokemonShared.Gui
{
    public class HpLine : AbstractGuiComponent
    {


        private int _currentHp;
        private int _maxHp;

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

        public override void Update()
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