
using GameEngine.Globals;

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
}