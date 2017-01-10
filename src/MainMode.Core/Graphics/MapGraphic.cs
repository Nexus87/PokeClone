using GameEngine.Graphics.Textures;
using GameEngine.TypeRegistry;

namespace MainMode.Core.Graphics
{
    [GameType]
    public class MapGraphic : IMapGraphic
    {
        public float TextureSize { get; }


        public MapGraphic(ITexture2D fieldTextures, float textureSize = 128.0f)
        {
            TextureSize = textureSize;
            Texture2D = fieldTextures;
        }


        public float GetXPositionOfColumn(int column)
        {
            return column * TextureSize;
        }

        public float GetYPositionOfRow(int row)
        {
            return row * TextureSize;
        }

        public ITexture2D Texture2D { get; }
    }
}