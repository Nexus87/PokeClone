using GameEngine.GUI.Graphics.General;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Graphics
{
    [GameType]
    public class TextureBox : AbstractGraphicComponent, IImageBox
    {
        private ITexture2D _image;

        public ITexture2D Image { get { return _image; } set { _image = value; Invalidate(); } }


        public TextureBox(ITexture2D image = null)
        {
            _image = image;
        }

        public override void Setup()
        {
            _image?.LoadContent();
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            if (_image != null)
                batch.Draw(texture: _image, destinationRectangle: Area, color: Color.White);
        }

    }
}