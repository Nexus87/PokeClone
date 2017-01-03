using GameEngine.GUI;

namespace MainMode.Core.Graphics {
    public interface ICharacterSprite : IGuiComponent
    {
        void TurnToDirection(Direction direction);
    }
}