using System;
using GameEngine.Core;
using GameEngine.Graphics.General;
using GameEngine.GUI;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace MainMode.Core.Graphics
{
    [GameService(typeof(IWorldScreenController))]
    public class WorldScreen : AbstractGraphicComponent, IWorldScreenController
    {
        private ICharacterSprite _player;
        private readonly IMapController _mapController;
        private readonly ISpriteLoader _spriteLoader;
        private readonly ScreenConstants _constants;

        public WorldScreen(IMapController mapController, ISpriteLoader spriteLoader, ScreenConstants constants)
        {
            _mapController = mapController;
            _spriteLoader = spriteLoader;
            _constants = constants;
            _player = _spriteLoader.GetSprite("player");

        }

        protected override void Update()
        {
            base.Update();

            var playerX = _constants.ScreenWidth / 2.0f - 64;
            var playerY = _constants.ScreenHeight / 2.0f - 64;

            //TODO remove hardcoded values
            _player.Area = new Rectangle((int) playerX, (int) playerY, 128, 128);
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            _mapController.Draw(time, batch);
            _player.Draw(time, batch);
        }

        private static Direction ReverseDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return Direction.Down;
                case Direction.Down:
                    return Direction.Up;
                case Direction.Left:
                    return Direction.Right;
                case Direction.Right:
                    return Direction.Left;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        public void PlayerTurnDirection(Direction direction)
        {
            _player.TurnToDirection(direction);
        }

        public void PlayerMoveDirection(Direction direction)
        {
            _mapController.MoveMap(ReverseDirection(direction));
        }

        public void SetMap(Map map)
        {
            _mapController.LoadMap(map);
            _mapController.CenterField(map.PlayerStart.X, map.PlayerStart.Y);
        }
    }
}