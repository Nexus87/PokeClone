using GameEngine.Graphics.Textures;
using GameEngine.GUI.Renderers;

namespace GameEngine.GUI.Controlls
{
    public class ImageBox : AbstractGuiComponent
    {
        private readonly ImageBoxRenderer _renderer;
        private ITexture2D _image;

        public ITexture2D Image
        {
            get { return _image; }
            set
            {
                _image = value;
                Invalidate();
            }
        }


        public ImageBox(ImageBoxRenderer renderer)
        {
            _renderer = renderer;
        }
    }
}