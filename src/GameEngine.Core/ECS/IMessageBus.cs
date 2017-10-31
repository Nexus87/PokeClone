using System;

namespace GameEngine.Core.ECS
{
    public interface IMessageBus
    {
        void RegisterForAction<TAction>(Action<TAction> handler);
        void UnregisterHandler<TAction>(Action<TAction> handler);

        void SendAction<TAction>(TAction action);
    }
}