using System;

namespace GameEngine.Core.ECS
{
    public interface IMessageBus
    {
        void RegisterForAction<TAction>(Action<TAction, IEntityManager, IMessageBus> handler);
        void RegisterForAction<TAction>(Action<IEntityManager, IMessageBus> handler);
        void RegisterForAction<TAction>(Action<TAction, IMessageBus> handler);
        void RegisterForAction<TAction>(Action<IMessageBus> handler);
        void RegisterForAction<TAction>(Action handler);
        void UnregisterHandler<TAction>(Action<TAction, IEntityManager, IMessageBus> handler);
        void UnregisterHandler<TAction>(Action<IEntityManager, IMessageBus> handler);
        void UnregisterHandler<TAction>(Action<TAction, IMessageBus> handler);
        void UnregisterHandler<TAction>(Action<IMessageBus> handler);
        void UnregisterHandler<TAction>(Action handler);
        
        void StartProcess();
        void SendAction<TAction>(TAction action);
        void SendAction(object action);
        int ActionCount { get; }
    }
}