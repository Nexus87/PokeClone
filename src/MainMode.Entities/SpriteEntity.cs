using MainMode.Globals;
using Microsoft.Xna.Framework;

namespace MainMode.Entities
{
    public abstract class SpriteEntity
    {
        private readonly GameStateEntity _gameStateEntity;
        public SpriteId SpriteId { get; }

        internal Point Position { get; set; }
        internal bool BlockInput { get; set; }
        protected internal Direction Direction { get; set; }

        protected SpriteEntity(GameStateEntity gameStateEntity, SpriteId spriteId)
        {
            _gameStateEntity = gameStateEntity;
            SpriteId = spriteId;
        }

        protected void Move(Direction direction)
        {
            _gameStateEntity.Move(SpriteId, direction);
        }

        protected void Turn(Direction direction)
        {
            _gameStateEntity.Turn(SpriteId, direction);
        }

        public virtual void Update(GameTime time)
        {
        }
    }

}