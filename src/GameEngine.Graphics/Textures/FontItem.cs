namespace GameEngine.Graphics.Textures
{
    public class FontItem
    {
        public string Path { get; }
        public string FontName { get; }
        public bool IsPlatfromSpecific { get; }

        public FontItem(string path, string fontName, bool isPlatfromSpecific)
        {
            Path = path;
            FontName = fontName;
            IsPlatfromSpecific = isPlatfromSpecific;
        }
    }
}