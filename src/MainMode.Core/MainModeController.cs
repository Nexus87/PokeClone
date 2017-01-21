using GameEngine.Core;
using GameEngine.Globals;
using GameEngine.Graphics;
using GameEngine.TypeRegistry;
using MainMode.Core.Entities;
using MainMode.Core.Graphic;
using MainMode.Core.Loader;
using MainMode.Entities;
using MainMode.Globals;
using MainMode.Graphic;
using Microsoft.Xna.Framework;

namespace MainMode.Core
{
    [GameService(typeof(MainModeController))]
    public class MainModeController
    {
        private readonly Scene _scene;
        private readonly MapLoader _mapLoader;
        private readonly CharacterSpriteLoader _spriteLoader;
        private readonly GameStateEntity _gameStateEntity;
        private readonly GraphicController _graphicController;
        private readonly IEngineInterface _engineInterface;
        private PlayerSpriteEntity _player;

        public IInputHandler InputHandler => _player;

        public MainModeController(Scene scene, MapLoader mapLoader, CharacterSpriteLoader spriteLoader, GameStateEntity gameStateEntity, GraphicController graphicController, IEngineInterface engineInterface)
        {
            _scene = scene;
            _mapLoader = mapLoader;
            _spriteLoader = spriteLoader;
            _gameStateEntity = gameStateEntity;
            _graphicController = graphicController;
            _engineInterface = engineInterface;
        }

        public void SetMap(string mapName)
        {
            _gameStateEntity.ClearState();
            _graphicController.ClearScreen();

            var map = _mapLoader.LoadMap(mapName);

            _gameStateEntity.SetMap(map.Tiles);

            var playerTexutres = _spriteLoader.GetSpriteTextures("Player");
            var playerSprite = new PlayerSpriteController(_scene, playerTexutres);
            _player = new PlayerSpriteEntity(new SpriteId(), _gameStateEntity, _engineInterface);

            _gameStateEntity.PlaceSpriteEntity(_player, 1, 0);
            _graphicController.AddSpriteEntity(_player.SpriteId, playerSprite, new Point(1, 0) * Constants.Size);
            _scene.SetBackground(map.MapTexture);

        }
    }
}