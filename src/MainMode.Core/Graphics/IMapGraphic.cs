using GameEngine.Graphics.Textures;

namespace MainMode.Core.Graphics
{
    public interface IMapGraphic
    {
        ITexture2D Texture2D { get; }
        float TextureSize { get; }
        float GetXPositionOfColumn(int column);
        float GetYPositionOfRow(int row);
    }
}