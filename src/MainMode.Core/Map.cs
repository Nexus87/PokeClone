
using GameEngine.GUI.Utils;

namespace MainMode.Core
{
    public class Map
    {
        public Map(Table<Tile> tiles, TilePosition playerStart)
        {
            Tiles = tiles;
            PlayerStart = playerStart;
        }

        public Table<Tile> Tiles { get; private set; }
        public TilePosition PlayerStart { get; private set; }
    }

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