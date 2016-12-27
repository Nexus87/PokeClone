using System.Collections.Generic;
using GameEngine.TypeRegistry;
using MainMode.Core.Graphics;
using Microsoft.Xna.Framework;

namespace MainMode.Core
{
    [GameService(typeof(IGameStateComponent))]
    public class GameStateComponent : GameEngine.Core.IGameComponent, IGameStateComponent
    {
        private readonly IWorldScreenController controller;
        private Map map;
        private readonly Dictionary<int, FieldCoordinate> sprites = new Dictionary<int, FieldCoordinate>();

        public void SetMap(Map map)
        {
            this.map = map;
            controller.SetMap(map);

        }

        public void PlaceSprite(int spriteId, FieldCoordinate fieldCoordinate)
        {
            sprites[spriteId] = fieldCoordinate;
        }

        public FieldCoordinate GetPosition(int spriteId)
        {
            return sprites[spriteId];
        }

        public GameStateComponent(IWorldScreenController controller)
        {
            this.controller = controller;
        }
        public void Move(int spriteId, Direction direction)
        {
            var newPosition = Move(direction, sprites[spriteId]);
            if(IsOutOfBound(newPosition))
                return;
            if (IsBlocked(newPosition))
                return;
            if (IsPlayer(spriteId))
                controller.PlayerMoveDirection(direction);
            sprites[spriteId] = newPosition;
        }

        private bool IsOutOfBound(FieldCoordinate newPosition)
        {
            if (newPosition.X < 0 || newPosition.X > map.Tiles.Columns)
                return true;
            if (newPosition.Y < 0 || newPosition.Y > map.Tiles.Rows)
                return true;

            return false;
        }

        private bool IsBlocked(FieldCoordinate newPosition)
        {
            return !map.Tiles[newPosition.Y, newPosition.X].IsAccessable;
        }

        private FieldCoordinate Move(Direction direction, FieldCoordinate fieldCoordinate)
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
                controller.PlayerTurnDirection(direction);
        }

        public void Initialize()
        {
            throw new System.NotImplementedException();
        }

        public void Update(GameTime time)
        {
            throw new System.NotImplementedException();
        }
    }
}