using GameEngine.Graphics;

namespace MainMode.Core.Graphics {
    public abstract class AbstractCharacterSprite : Sprite
    {
        public abstract void TurnToDirection(Direction direction);
    }
}