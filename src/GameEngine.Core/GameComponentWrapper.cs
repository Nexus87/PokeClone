using System;
using Microsoft.Xna.Framework;

namespace GameEngine.Core
{
    internal class GameComponentWrapper : GameComponent
    {
        protected bool Equals(GameComponentWrapper other)
        {
            return Component.Equals(other.Component);
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
            return Component.GetHashCode();
        }

        public static bool operator ==(GameComponentWrapper left, GameComponentWrapper right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GameComponentWrapper left, GameComponentWrapper right)
        {
            return !Equals(left, right);
        }

        public Components.IGameComponent Component { get; }

        public GameComponentWrapper(Components.IGameComponent component, Game game) :
            base(game)
        {
            if (component == null) throw new ArgumentNullException(nameof(component));
            Component = component;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Component.Update(gameTime);
        }

    }
}
