using GameEngine.GUI;

namespace MainMode.Core.Graphics {
    public interface ICharacterSprite : IGraphicComponent
    {
        void TurnToDirection(Direction direction);
    }
}