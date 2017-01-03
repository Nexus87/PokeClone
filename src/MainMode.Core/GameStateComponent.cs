using System.Collections.Generic;
using GameEngine.TypeRegistry;
using MainMode.Core.Graphics;
using Microsoft.Xna.Framework;

namespace MainMode.Core
{
    [GameService(typeof(IGameStateComponent))]
    public class GameStateComponent : GameEngine.Components.IGameComponent, IGameStateComponent
    {
        private readonly IWorldScreenController _controller;
        private Map _map;
        private readonly Dictionary<int, FieldCoordinate> _sprites = new Dictionary<int, FieldCoordinate>();

        public void SetMap(Map map)
        {
            _map = map;
            _controller.SetMap(map);

        }

        public void PlaceSprite(int spriteId, FieldCoordinate fieldCoordinate)
        {
            _sprites[spriteId] = fieldCoordinate;
        }

        public FieldCoordinate GetPosition(int spriteId)
        {
            return _sprites[spriteId];
        }

        public GameStateComponent(IWorldScreenController controller)
        {
            _controller = controller;
        }
        public void Move(int spriteId, Direction direction)
        {
            var newPosition = Move(direction, _sprites[spriteId]);
            if(IsOutOfBound(newPosition))
                return;
            if (IsBlocked(newPosition))
                return;
            if (IsPlayer(spriteId))
                _controller.PlayerMoveDirection(direction);
            _sprites[spriteId] = newPosition;
        }

        private bool IsOutOfBound(FieldCoordinate newPosition)
        {
            if (newPosition.X < 0 || newPosition.X > _map.Tiles.Columns)
                return true;
            if (newPosition.Y < 0 || newPosition.Y > _map.Tiles.Rows)
                return true;

            return false;
        }

        private bool IsBlocked(FieldCoordinate newPosition)
        {
            return !_map.Tiles[newPosition.Y, newPosition.X].IsAccessable;
        }

        private static FieldCoordinate Move(Direction direction, FieldCoordinate fieldCoordinate)
        {
            switch (direction)
            {
                case Direction.Down:
                    return new FieldCoordinate(fieldCoordinate.X, fieldCoordinate.Y + 1);
                case Direction.Left:
                    return new FieldCoordinate(fieldCoordinate.X - 1, fieldCoordinate.Y);
                case Direction.Right:
                    return new FieldCoordinate(fieldCoordinate.X + 1, fieldCoordinate.Y);
                case Direction.Up:
                    return new FieldCoordinate(fieldCoordinate.X, fieldCoordinate.Y - 1);
            }
            return default(FieldCoordinate);
        }

        private static bool IsPlayer(int spriteId)
        {
            return spriteId == 0;
        }

        public void Turn(int spriteId, Direction direction)
        {
            if (IsPlayer(spriteId))
                _controller.PlayerTurnDirection(direction);
        }

        public void Update(GameTime time)
        {
            throw new System.NotImplementedException();
        }
    }
}