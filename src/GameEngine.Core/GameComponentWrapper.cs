using System;
using GameEngine.Entities;
using Microsoft.Xna.Framework;

namespace GameEngine.Core
{
    internal class GameComponentWrapper : GameComponent
    {
        protected bool Equals(GameComponentWrapper other)
        {
            return Entity.Equals(other.Entity);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((GameComponentWrapper) obj);
        }

        public override int GetHashCode()
        {
            return Entity.GetHashCode();
        }

        public static bool operator ==(GameComponentWrapper left, GameComponentWrapper right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GameComponentWrapper left, GameComponentWrapper right)
        {
            return !Equals(left, right);
        }

        public IGameEntity Entity { get; }

        public GameComponentWrapper(IGameEntity entity, Game game) :
            base(game)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Entity = entity;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Entity.Update(gameTime);
        }

    }
}
