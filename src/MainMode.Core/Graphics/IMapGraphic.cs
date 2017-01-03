using GameEngine.GUI;

namespace MainMode.Core.Graphics
{
    public interface IMapGraphic : IGuiComponent
    {
        float TextureSize { get; }
        float GetXPositionOfColumn(int column);
        float GetYPositionOfRow(int row);
    }
}