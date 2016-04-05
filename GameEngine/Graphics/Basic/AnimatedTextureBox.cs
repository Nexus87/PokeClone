using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameEngine.Graphics.Basic
{
    internal class AnimatedTextureBox : ForwardingGraphicComponent<TextureBox>
    {
        public readonly List<ITexture2D> Textures = new List<ITexture2D>();
        private int currentIndex = 0;
        private int seconds = 0;

        public AnimatedTextureBox(PokeEngine game) : base(new TextureBox(game), game)
        {
            SecondsPerImage = 1;
        }

        public int SecondsPerImage { get; set; }

        public override void Draw(GameTime time, ISpriteBatch batch)
        {
            seconds += time.ElapsedGameTime.Seconds;
            if (seconds >= SecondsPerImage)
            {
                Invalidate();
                seconds = 0;
            }
            base.Draw(time, batch);
        }

        public override void Setup()
        {
            base.Setup();
        }

        protected override void Update()
        {
            if (Textures.Count == 0)
                return;

            currentIndex = (currentIndex + 1) % Textures.Count;
            InnerComponent.Image = Textures[currentIndex];
        }
    }
}