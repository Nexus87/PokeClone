using Microsoft.Xna.Framework;

namespace GameEngine.Core
{
    internal class GameComponentWrapper : GameComponent
    {
        public IGameComponent Component { get; private set; }

        public GameComponentWrapper(IGameComponent component, Game game) :
            base(game)
        {
            Component = component;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Component.Update(gameTime);
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
                return true;
            if (!(obj is GameComponentWrapper))
                return false;

            return ((GameComponentWrapper)obj).Component.Equals(Component);
        }

        public override int GetHashCode()
        {
            return Component.GetHashCode();
        }
    }
}
