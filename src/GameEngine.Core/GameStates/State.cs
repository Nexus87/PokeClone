using GameEngine.Core.ECS;
using GameEngine.Core.ECS.Systems;
using GameEngine.Graphics.General;
using Microsoft.Xna.Framework;

namespace GameEngine.Core.GameStates
{
    public abstract class State
    {
        private readonly Screen _screen;
        private readonly ISpriteBatch _spriteBatch;
        protected readonly EntityManager EntityManager;
        protected readonly SystemManager SystemManager;

        public ECS.Systems.GuiSystem GuiSystem { get; }
        public RenderSystem RenderSystem { get; }

        protected State(Screen screen, ISpriteBatch spriteBatch, EntityManager entityManager, SystemManager systemManager)
        {
            _screen = screen;
            _spriteBatch = spriteBatch;
            EntityManager = entityManager;
            SystemManager = systemManager;
            RenderSystem = new RenderSystem(spriteBatch);
            GuiSystem = new ECS.Systems.GuiSystem(new GuiManager(), spriteBatch);
        }

        public void Update(GameTime time)
        {
            _screen.Begin(_spriteBatch);
            RenderSystem.Update(time, EntityManager);
            GuiSystem.Update(time, EntityManager);
            _screen.End(_spriteBatch);

            SystemManager.Update(time, EntityManager);
        }

        public abstract void Init();
        public abstract void Pause();
        public abstract void Resume();
        public abstract void Stop();
    }
}