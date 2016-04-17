using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class GameComponentWrapper : GameComponent
    {
        public IGameComponent Component { get; private set; }

        public GameComponentWrapper(IGameComponent component, Game game) :
            base(game)
        {
            Component = component;
        }

        public override void Initialize()
        {
            base.Initialize();
            Component.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Component.Update(gameTime);
        }


    }
}
