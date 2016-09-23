using GameEngine.Graphics;

namespace MainModule.Graphics
{
    public interface IMap : IGraphicComponent
    {
        float TextureSize { get; }
        float GetXPositionOfColumn(int column);
        float GetYPositionOfRow(int row);
    }
}