namespace MainMode.Entities
{
    public class MapField
    {
        public MapField(bool isAccessable)
        {
            IsAccessable = isAccessable;
        }

        public bool IsAccessable { get; }
        public SpriteEntity SpriteEntity { get; set; }
    }
}