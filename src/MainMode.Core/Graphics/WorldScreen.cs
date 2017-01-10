using System;
using GameEngine.Graphics;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace MainMode.Core.Graphics
{
    [GameService(typeof(IWorldScreenController))]
    public class WorldScreen : IWorldScreenController
    {
        private readonly AbstractCharacterSprite _player;
        private readonly ISceneController _sceneController;

        public WorldScreen(ISceneController sceneController, ISpriteLoader spriteLoader)
        {
            _sceneController = sceneController;
            _player = spriteLoader.GetSprite("player");

            Scene.AddSprite(_player);
            Init();
        }

        protected void Init()
        {
            //TODO remove hardcoded values
            _player.Position = new Rectangle(0, 0, 128, 128);
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
            _sceneController.MoveMap(ReverseDirection(direction));
        }

        public void SetMap(Map map)
        {
            _sceneController.LoadMap(map);
            _sceneController.CenterField(map.PlayerStart.X, map.PlayerStart.Y);
        }

        public Scene Scene => _sceneController.Scene;
    }
}