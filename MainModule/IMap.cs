using GameEngine.Graphics;

namespace MainModule
{
    public interface IMap : IGraphicComponent
    {
        float TextureSize { get; }
        FieldSize FieldSize { get; }
        float GetXPositionOfColumn(int column);
        float GetYPositionOfRow(int row);
    }
}