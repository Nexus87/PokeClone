using GameEngine.Graphics.Textures;

namespace GameEngine.GUI.Controlls
{
    public class ImageBox : AbstractGuiComponent
    {
        private ITexture2D _image;

        public ITexture2D Image
        {
            get => _image;
            set
            {
                _image = value;
                Invalidate();
            }
        }
    }
}