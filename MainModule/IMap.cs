using GameEngine.Graphics;

namespace MainModule
{
    public interface IMap : IGraphicComponent
    {
        FieldSize FieldSize { get; }
        void CenterField(int fieldX, int fieldY);
        void MoveMap(Direction moveDirection);
    }
}