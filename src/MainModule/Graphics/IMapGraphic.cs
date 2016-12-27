using GameEngine.GUI.Graphics;

namespace MainMode.Core.Graphics
{
    public interface IMapGraphic : IGraphicComponent
    {
        float TextureSize { get; }
        float GetXPositionOfColumn(int column);
        float GetYPositionOfRow(int row);
    }
}