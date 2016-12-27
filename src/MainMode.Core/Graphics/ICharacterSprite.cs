using GameEngine.GUI.Graphics;

namespace MainMode.Core.Graphics {
    public interface ICharacterSprite : IGraphicComponent
    {
        void TurnToDirection(Direction direction);
    }
}