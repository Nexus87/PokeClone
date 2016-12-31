using System;
using GameEngine;
using GameEngine.Core;
using GameEngine.GUI;
using GameEngine.GUI.Controlls;
using GameEngine.GUI.General;
using GameEngine.GUI.Graphics;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace BattleMode.Core.Components.GraphicComponents
{
    [GameType]
    public class PokemonSprite : AbstractGraphicComponent
    {
        public event EventHandler OnPokemonAppeared = delegate { };
        public event EventHandler OnAttackAnimationPlayed = delegate { };

        private readonly TextureProvider _provider;
        private readonly ImageBox _box;
        private bool _isPlayer;

        public bool IsPlayer { get { return _isPlayer; } set { _isPlayer = value; Invalidate(); } }
        private int _id = -1;

        public void SetPokemon(int id)
        {
            _id = id;
            Invalidate();
            OnPokemonAppeared(this, null);
        }

        public void PlayAttackAnimation()
        {
            OnAttackAnimationPlayed(this, null);
        }

        public PokemonSprite(ImageBox box, TextureProvider provider)
        {
            _box = box;
            _provider = provider;
        }
        public override void Setup()
        {
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            _box.Draw(time, batch);
        }

        protected override void Update()
        {
            base.Update();
            _box.SetCoordinates(this);

            _box.Image = _isPlayer ? _provider.GetTextureBack(_id) : _provider.GetTexturesFront(_id);
        }
    }
}
