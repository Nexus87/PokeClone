﻿using System;
using GameEngine;
using GameEngine.Graphics;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.General;
using GameEngine.Registry;
using Microsoft.Xna.Framework;

namespace BattleLib.Components.GraphicComponents
{
    [GameType]
    public class PokemonSprite : AbstractGraphicComponent
    {
        public event EventHandler OnPokemonAppeared = delegate { };
        public event EventHandler OnAttackAnimationPlayed = delegate { };

        private readonly TextureProvider provider;
        private readonly TextureBox box;
        private bool isPlayer;

        public bool IsPlayer { get { return isPlayer; } set { isPlayer = value; Invalidate(); } }
        private int id = -1;

        public void SetPokemon(int id)
        {
            this.id = id;
            Invalidate();
            OnPokemonAppeared(this, null);
        }

        public void PlayAttackAnimation()
        {
            OnAttackAnimationPlayed(this, null);
        }

        public PokemonSprite(TextureBox box, TextureProvider provider)
        {
            this.box = box;
            this.provider = provider;
        }
        public override void Setup()
        {
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            box.Draw(time, batch);
        }

        protected override void Update()
        {
            base.Update();
            box.SetCoordinates(this);

            box.Image = isPlayer ? provider.GetTextureBack(id) : provider.GetTexturesFront(id);
        }
    }
}
