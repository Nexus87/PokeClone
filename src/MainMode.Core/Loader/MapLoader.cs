using System;
using GameEngine.Globals;
using GameEngine.Graphics.Textures;
using GameEngine.TypeRegistry;
using MainMode.Globals;
using Microsoft.Xna.Framework;
using PokemonShared.Core;

namespace MainMode.Core.Loader
{
    [GameService(typeof(MapLoader))]
    public class MapLoader
    {
        private readonly TextureBuilder _textureBuilder;
        private readonly TextureProvider _textureProvider;

        public MapLoader(TextureBuilder textureBuilder, TextureProvider textureProvider)
        {
            _textureBuilder = textureBuilder;
            _textureProvider = textureProvider;
        }

        public Map LoadMap(string name)
        {
            var tiles = new Table<Tile>
            {
                [0, 0] = new Tile("Tile0303", false),
                [1, 0] = new Tile("Tile0403", false),
                [2, 0] = new Tile("Tile0503", false),
                [0, 1] = new Tile("Tile0304", true),
                [1, 1] = new Tile("Tile0404", true),
                [2, 1] = new Tile("Tile0504", false),
                [0, 2] = new Tile("Tile0305", false),
                [1, 2] = new Tile("Tile0405", false),
                [2, 2] = new Tile("Tile0505", false)
            };

            var textures = new Table<Tuple<Rectangle, ITexture2D>>();
            tiles.LoopOverTable((row, column) =>
            {
                var t = _textureProvider.GetTexture(PokemonSharedModule.TextureKey, tiles[row, column].TextureName);
                textures[row, column] = Tuple.Create(new Rectangle(new Point(column, row) *Constants.Size, Constants.Size), t);
            });

            return new Map(name, tiles, _textureBuilder.BuildTexture(textures));
        }
    }

    public class Map
    {
        public Map(string mapName, Table<Tile> tiles, ITexture2D mapTexture)
        {
            MapName = mapName;
            Tiles = tiles;
            MapTexture = mapTexture;
        }

        public ITexture2D MapTexture { get; }
        public string MapName { get; }
        public Table<Tile> Tiles { get; }
    }
}