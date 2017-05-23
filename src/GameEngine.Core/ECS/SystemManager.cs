using System.Collections.Generic;
using GameEngine.Core.ECS.Systems;
using GameEngine.Graphics.General;
using Microsoft.Xna.Framework;

namespace GameEngine.Core.ECS
{
    public class SystemManager
    {
        private readonly Screen _screen;
        private readonly ISpriteBatch _spriteBatch;
        private readonly Systems.GuiSystem _guiSystem;
        private readonly RenderSystem _renderSystem;
        private readonly List<ISystem> _systems = new List<ISystem>();
        
        internal SystemManager(Screen screen, ISpriteBatch spriteBatch, Systems.GuiSystem guiSystem, RenderSystem renderSystem, InputSystem inputSystem)
        {
            _screen = screen;
            _spriteBatch = spriteBatch;
            _guiSystem = guiSystem;
            _renderSystem = renderSystem;
            _systems.Add(inputSystem);
        }

        public void AddSystem(ISystem system)
        {
            _systems.Add(system);
        }

        public void RemoveSystem(ISystem system)
        {
            _systems.Remove(system);
        }

        public void Update(GameTime time)
        {
            _screen.Begin(_spriteBatch);
            _renderSystem.Update(time);
            _guiSystem.Update(time);
            _screen.End(_spriteBatch);

            _systems.ForEach(x => x.Update(time));
        }
    }
}