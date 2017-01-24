namespace GameEngine.Graphics.Textures
{
    public class FontItem
    {
        public string Path { get; }
        public string FontName { get; }

        public FontItem(string path, string fontName)
        {
            Path = path;
            FontName = fontName;
        }
    }
}