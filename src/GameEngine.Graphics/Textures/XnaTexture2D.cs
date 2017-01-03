using System.IO;
using GameEngine.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Graphics.Textures
{
    public class XnaTexture2D : ITexture2D
    {
        private readonly string _textureName;
        private readonly ContentManager _content;
        public Texture2D Texture { get; set; }

        public XnaTexture2D(){}
        public XnaTexture2D(Texture2D texture)
        {
            texture.CheckNull("texture");
            Texture = texture;
        }

        public XnaTexture2D(string textureName, ContentManager content)
        {
            textureName.CheckNull("textureName");
            content.CheckNull("content");

            _textureName = textureName;
            _content = content;
        }

        public Rectangle Bounds => Texture.Bounds;
        public int Height => Texture.Height;
        public int Width => Texture.Width;

        public void GetData<T>(T[] data) where T : struct
        {
            Texture.GetData(data);
        }

        public void GetData<T>(T[] data, int startIndex, int elementCount) where T : struct
        {
            Texture.GetData(data, startIndex, elementCount);
        }

        public void GetData<T>(int level, Rectangle? rect, T[] data, int startIndex, int elementCount) where T : struct
        {
            Texture.GetData(level, rect, data, startIndex, elementCount);
        }

        public void GetData<T>(int level, int arraySlice, Rectangle? rect, T[] data, int startIndex, int elementCount) where T : struct
        {
            Texture.GetData(level, arraySlice, rect, data, startIndex, elementCount);
        }

        public void Reload(Stream textureStream)
        {
            Texture.Reload(textureStream);
        }

        public void SaveAsJpeg(Stream stream, int width, int height)
        {
            Texture.SaveAsJpeg(stream, width, height);
        }

        public void SaveAsPng(Stream stream, int width, int height)
        {
            Texture.SaveAsPng(stream, width, height);
        }

        public void SetData<T>(T[] data) where T : struct
        {
            Texture.SetData(data);
        }

        public void SetData<T>(T[] data, int startIndex, int elementCount) where T : struct
        {
            Texture.SetData(data, startIndex, elementCount);
        }

        public void SetData<T>(int level, Rectangle? rect, T[] data, int startIndex, int elementCount) where T : struct
        {
            Texture.SetData(level, rect, data, startIndex, elementCount);
        }

        public void SetData<T>(int level, int arraySlice, Rectangle? rect, T[] data, int startIndex, int elementCount) where T : struct
        {
            Texture.SetData(level, arraySlice, rect, data, startIndex, elementCount);
        }


        public void LoadContent()
        {
            if(Texture == null)
                Texture = _content.Load<Texture2D>(_textureName);
        }

        public Rectangle? AbsoluteBound(Rectangle? relativeBounds)
        {
            return relativeBounds;
        }
    }
}
