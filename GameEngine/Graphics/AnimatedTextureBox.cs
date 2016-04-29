using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameEngine.Graphics
{
    internal class AnimatedTextureBox : ForwardingGraphicComponent<TextureBox>
    {
        public readonly List<ITexture2D> Textures = new List<ITexture2D>();
        private int currentIndex;
        private int seconds;

        public AnimatedTextureBox(IPokeEngine game) : base(new TextureBox(game), game)
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