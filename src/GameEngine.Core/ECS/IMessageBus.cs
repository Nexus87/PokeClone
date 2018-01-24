using System;

namespace GameEngine.Core.ECS
{
    public interface IMessageBus
    {
        void RegisterForAction<TAction>(Action<TAction, IEntityManager> handler);
        void RegisterForAction<TAction>(Action<IEntityManager> handler);
        void RegisterForAction<TAction>(Action<TAction> handler);
        void RegisterForAction<TAction>(Action handler);
        void UnregisterHandler<TAction>(Action<TAction, IEntityManager> handler);

        void StartProcess();
        void SendAction<TAction>(TAction action);
        void SendAction(object action);
        int ActionCount { get; }
    }
}