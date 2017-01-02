using GameEngine.GUI.General;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Graphics.General
{
    public class XnaSpriteSheetTexture2D : ITexture2D
    {
        private readonly XnaTexture2D _sourceTexture;

        public XnaSpriteSheetTexture2D(Rectangle bounds, XnaTexture2D sourceTexture)
        {
            _sourceTexture = sourceTexture;
            Bounds = bounds;
            Height = bounds.Height;
            Width = bounds.Width;
        }

        public Rectangle Bounds { get; }
        public int Height { get; }
        public int Width { get; }

        public Texture2D Texture => _sourceTexture.Texture;
        public void LoadContent()
        {
            _sourceTexture.LoadContent();
        }

        public Rectangle? AbsoluteBound(Rectangle? relativeBounds)
        {
            if (relativeBounds == null)
                return null;
            return new Rectangle(relativeBounds.Value.Location + Bounds.Location, relativeBounds.Value.Size);
        }
    }
}