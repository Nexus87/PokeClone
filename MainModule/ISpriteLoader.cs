using MainModule.Graphics;

namespace MainModule
{
    public interface ISpriteLoader
    {
        ICharacterSprite GetSprite(string spriteName);
        void Setup();
    }
}