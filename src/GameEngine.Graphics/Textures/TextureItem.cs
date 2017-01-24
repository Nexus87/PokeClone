namespace GameEngine.Graphics.Textures
{
    public class TextureItem
    {
        public TextureItem(string path, string textureName)
        {
            Path = path;
            TextureName = textureName;
        }

        public string Path { get; }
        public string TextureName { get; }
    }
}