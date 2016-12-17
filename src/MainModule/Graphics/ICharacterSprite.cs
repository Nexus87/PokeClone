using GameEngine.GUI.Graphics;

namespace MainModule.Graphics {
    public interface ICharacterSprite : IGraphicComponent
    {
        void TurnToDirection(Direction direction);
    }
}