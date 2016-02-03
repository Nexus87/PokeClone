using GameEngine;
using GameEngine.Graphics;
using GameEngine.Graphics.Basic;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.GraphicComponents
{
    class PokemonSprite : AbstractGraphicComponent
    {
        public event EventHandler OnPokemonAppeared = delegate { };
        public event EventHandler OnAttackAnimationPlayed = delegate { };

        TextureProvider provider = new TextureProvider();
        TextureBox box;
        bool front;
        private int id;

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

        public PokemonSprite(bool front, PokeEngine game)
            : base(game)
        {
            this.front = front;
            box = new TextureBox(game);
        }
        public override void Setup(ContentManager content)
        {
            provider.Content = content;
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            box.Draw(time, batch);
        }

        protected override void Update()
        {
            base.Update();
            
            box.X = X;
            box.Y = Y;
            box.Width = Width;
            box.Height = Height;

            box.Image = front ? provider.getTexturesFront(id) : provider.getTextureBack(id);
        }
    }
}
