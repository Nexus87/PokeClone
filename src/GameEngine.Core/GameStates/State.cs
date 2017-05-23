using GameEngine.Core.ECS;
using Microsoft.Xna.Framework;

namespace GameEngine.Core.GameStates
{
    public abstract class State
    {
        protected readonly EntityManager EntityManager;
        protected readonly SystemManager SystemManager;

        protected State(EntityManager entityManager, SystemManager systemManager)
        {
            EntityManager = entityManager;
            SystemManager = systemManager;
        }

        public void Update(GameTime time)
        {
            SystemManager.Update(time);
        }

        public abstract void Init();
        public abstract void Pause();
        public abstract void Resume();
        public abstract void Stop();
    }
}