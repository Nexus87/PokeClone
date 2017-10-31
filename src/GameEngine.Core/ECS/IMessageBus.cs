using System;

namespace GameEngine.Core.ECS
{
    public interface IMessageBus
    {
        void RegisterForAction<TAction>(Action<TAction, IEntityManager> handler);
        void UnregisterHandler<TAction>(Action<TAction, IEntityManager> handler);

        void SendAction<TAction>(TAction action);
        int ActionCount { get; }
    }
}