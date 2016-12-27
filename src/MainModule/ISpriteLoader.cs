using MainMode.Core.Graphics;

namespace MainMode.Core
{
    public interface ISpriteLoader
    {
        ICharacterSprite GetSprite(string spriteName);
        void Setup();
    }
}