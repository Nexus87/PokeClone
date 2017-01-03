namespace GameEngine.Graphics.Textures
{
    public class TextureItem
    {
        public TextureItem(string path, string textureName, bool isPlatformSpecific)
        {
            Path = path;
            TextureName = textureName;
            IsPlatformSpecific = isPlatformSpecific;
        }

        public string Path { get; }
        public string TextureName { get; }
        public bool IsPlatformSpecific { get; }
    }
}