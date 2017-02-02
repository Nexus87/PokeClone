namespace MainMode.Globals
{
    public class Tile
    {
        public Tile(string textureName, bool isAccessable)
        {
            TextureName = textureName;
            IsAccessable = isAccessable;
        }

        public string TextureName { get; private set; }
        public bool IsAccessable { get; private set; }
    }
}