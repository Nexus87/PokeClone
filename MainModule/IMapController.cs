using GameEngine.Graphics;

namespace MainModule
{
    public interface IMapController : IGraphicComponent
    {
        FieldSize FieldSize { get; }
        void CenterField(int fieldX, int fieldY);
        void MoveMap(Direction moveDirection);
    }
}