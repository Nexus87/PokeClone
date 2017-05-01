using GameEngine.ECS;

namespace GameEngine.Core.GameStates
{
    public abstract class AbstractState : IState
    {
        protected readonly EntityManager EntityManager;
        protected readonly SystemManager SystemManager;

        protected AbstractState(EntityManager entityManager, SystemManager systemManager)
        {
            EntityManager = entityManager;
            SystemManager = systemManager;
        }

        public abstract void Init();
        public abstract void Pause();
        public abstract void Resume();
        public abstract void Stop();
    }
}