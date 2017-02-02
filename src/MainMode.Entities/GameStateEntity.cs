using System;
using System.Collections.Generic;
using GameEngine.Entities;
using GameEngine.Globals;
using GameEngine.TypeRegistry;
using MainMode.Globals;
using Microsoft.Xna.Framework;

namespace MainMode.Entities
{
    [GameService(typeof(GameStateEntity))]
    public class GameStateEntity : IGameEntity
    {
        public event EventHandler<SpritePositionChangedEventArgs> SpritePositionChanged;
        public event EventHandler<SpriteTurnedEventArgs> SpriteTurned;

        private Table<MapField> _mapFields = new Table<MapField>();
        private readonly Dictionary<SpriteId, SpriteEntity> _entities = new Dictionary<SpriteId, SpriteEntity>();

        public void PlaceSpriteEntity(SpriteEntity spriteEntity, Point location)
        {
            _entities[spriteEntity.SpriteId] = spriteEntity;
            spriteEntity.Position= location;
            _mapFields.Get(location).SpriteEntity = spriteEntity;
        }

        public void ClearState()
        {
            _entities.Clear();
            _mapFields = new Table<MapField>();
        }

        public void SetMap(Table<Tile> mapFields)
        {
            _mapFields = new Table<MapField>(mapFields.Rows, mapFields.Columns);
            mapFields.LoopOverTable( (row, column) =>
            {
                _mapFields[row, column] = new MapField(mapFields[row, column].IsAccessable);
            });
        }

        public void Update(GameTime time)
        {
            foreach (var entitiesValue in _entities.Values)
            {
                entitiesValue.Update(time);
            }
        }

        private void OnSpritePositionChanged(Point position, SpriteId spriteId)
        {
            SpritePositionChanged?.Invoke(this, new SpritePositionChangedEventArgs(position, spriteId));
        }

        public void UnlockSprite(SpriteId spriteId)
        {
            _entities[spriteId].BlockInput = false;
        }

        public void Move(SpriteId spriteId, Direction direction)
        {
            var entity = _entities[spriteId];
            if(entity.BlockInput)
                return;

            var newPosition = entity.Position + new Point(XMovement(direction), YMovement(direction));

            if(!IsAccessable(newPosition))
                return;

            _mapFields.Get(entity.Position).SpriteEntity = null;
            entity.BlockInput = true;
            entity.Position = newPosition;
            _mapFields.Get(entity.Position).SpriteEntity = entity;

            OnSpritePositionChanged(newPosition, spriteId);
        }

        private bool IsAccessable(Point location)
        {
            var field = _mapFields.Get(location);
            return field.IsAccessable && field.SpriteEntity == null;
        }

        private static int YMovement(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return -1;
                case Direction.Down:
                    return 1;
                default:
                    return 0;
            }
        }

        private static int XMovement(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    return -1;
                case Direction.Right:
                    return 1;
                default:
                    return 0;
            }
        }

        public void Turn(SpriteId spriteId, Direction direction)
        {
            var entity = _entities[spriteId];
            if(entity.BlockInput)
                return;

            entity.Direction = direction;
            OnSpriteTurned(spriteId, direction);
        }

        protected virtual void OnSpriteTurned(SpriteId spriteId, Direction direction)
        {
            SpriteTurned?.Invoke(this, new SpriteTurnedEventArgs(spriteId, direction));
        }
    }
}