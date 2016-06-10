using GameEngine;
using GameEngine.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace BattleLib.GraphicComponents
{
    public class PokemonSprite : AbstractGraphicComponent
    {
        public event EventHandler OnPokemonAppeared = delegate { };
        public event EventHandler OnAttackAnimationPlayed = delegate { };

        TextureProvider provider = new TextureProvider();
        TextureBox box;
        bool front;
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

        public PokemonSprite(bool front, IPokeEngine game)
        {
            this.front = front;
            box = new TextureBox();
            provider.Content = game.Content;
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
            
            box.XPosition = XPosition;
            box.YPosition = YPosition;
            box.Width = Width;
            box.Height = Height;

            box.Image = front ? provider.GetTexturesFront(id) : provider.GetTextureBack(id);
        }
    }
}
