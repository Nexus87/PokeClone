using System;
using GameEngine.Globals;
using GameEngine.Graphics.Textures;
using GameEngine.TypeRegistry;
using MainMode.Core;
using MainMode.Core.Graphics;
using Microsoft.Xna.Framework;

namespace PokemonGame.MainModuleClasses
{
    [GameService(typeof(IMapLoader))]
    public class DummyMapLoader : IMapLoader
    {
        private readonly TextureProvider _textureProvider;
        private readonly TextureBuilder _builder;

        public DummyMapLoader(TextureProvider textureProvider, TextureBuilder builder)
        {
            _textureProvider = textureProvider;
            _builder = builder;
        }

        public IMapGraphic LoadMap(Map map)
        {
            const int textureSize = 128;
            var tiles = map.Tiles;
            var table = new Table<Tuple<Rectangle, ITexture2D>>();

            for (var i = 0; i < tiles.Rows; i++)
            {
                for (var j = 0; j < tiles.Columns; j++)
                {
                    var position = new Rectangle(j * textureSize, i*textureSize, textureSize, textureSize);
                    var image = _textureProvider.GetTexture(MainModule.Key, tiles[i, j].TextureName);
                    table[i, j] = Tuple.Create(position, image);
                }
            }
            return new MapGraphic(_builder.BuildTexture(table));
        }
    }
}