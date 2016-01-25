using GameEngine;
using GameEngine.Graphics;
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
        TextureProvider provider = new TextureProvider();
        Texture2D texture;
        bool front;
        private int id;

        public PokemonSprite(bool front)
        {
            this.front = front;
        }
        public override void Setup(ContentManager content)
        {
            provider.Content = content;
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
        }

        protected override void Update()
        {
            base.Update();
            texture = front ? provider.getTexturesFront(id) : provider.getTextureBack(id);
        }
    }
}
