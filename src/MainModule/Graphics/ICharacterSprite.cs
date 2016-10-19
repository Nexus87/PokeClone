using GameEngine.Graphics;
using MainModule;

namespace MainModule.Graphics {
    public interface ICharacterSprite : IGraphicComponent
    {
        void TurnToDirection(Direction direction);
    }
}