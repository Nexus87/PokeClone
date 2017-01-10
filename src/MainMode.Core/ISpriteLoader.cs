using MainMode.Core.Graphics;

namespace MainMode.Core
{
    public interface ISpriteLoader
    {
        AbstractCharacterSprite GetSprite(string spriteName);
    }
}