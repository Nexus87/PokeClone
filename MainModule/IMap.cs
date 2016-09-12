using GameEngine.Graphics;

namespace MainModule
{
    public interface IMap : IGraphicComponent
    {
        FieldSize FieldSize { get; }
    }
}