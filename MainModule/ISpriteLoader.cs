using MainModule.Graphics;

namespace MainModule
{
    public interface ISpriteLoader
    {
        CharacterSprite GetSprite(string spriteName);
        void Setup();
    }
}