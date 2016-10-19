using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace GameEngine
{
    public interface ITexture2D
    {
        Rectangle Bounds { get; }
        int Height { get; }
        int Width { get; }

        void GetData<T>(T[] data) where T : struct;
        void GetData<T>(T[] data, int startIndex, int elementCount) where T : struct;
        void GetData<T>(int level, Rectangle? rect, T[] data, int startIndex, int elementCount) where T : struct;
        void GetData<T>(int level, int arraySlice, Rectangle? rect, T[] data, int startIndex, int elementCount) where T : struct;
        void Reload(Stream textureStream);
        void SaveAsJpeg(Stream stream, int width, int height);
        void SaveAsPng(Stream stream, int width, int height);
        void SetData<T>(T[] data) where T : struct;
        void SetData<T>(T[] data, int startIndex, int elementCount) where T : struct;
        void SetData<T>(int level, Rectangle? rect, T[] data, int startIndex, int elementCount) where T : struct;
        void SetData<T>(int level, int arraySlice, Rectangle? rect, T[] data, int startIndex, int elementCount) where T : struct;

        Texture2D Texture { get; }
        void LoadContent();
    }
}
