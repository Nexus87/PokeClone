namespace MainMode.Core
{
    public class TilePosition
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public TilePosition(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}