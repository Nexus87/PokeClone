using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.GUI.General
{
    public interface ITexture2D
    {
        Rectangle Bounds { get; }
        int Height { get; }
        int Width { get; }

        Texture2D Texture { get; }
        void LoadContent();

        Rectangle? AbsoluteBound(Rectangle? relativeBounds);
    }
}
