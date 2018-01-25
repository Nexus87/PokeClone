using System;
using GameEngine.Core.ECS;

namespace BattleMode.Core.Actions
{
    public class HpChangeFinishedAction
    {
        public readonly Guid Entity;
        public HpChangeFinishedAction(Guid entity)
        {
            Entity = entity;
        }
    }

    public static class ActionCreator
    {
        public static void SendHpChangedFinishedAction(this IMessageBus messageBus, Guid entity, object sender)
        {
            messageBus.SendAction(new HpChangeFinishedAction(entity), sender);
        }
    }
}