using BattleMode.Shared;
using GameEngine.Core.ECS;
using PokemonShared.Models;

namespace BattleMode.Entities.Actions
{
    public static class ActionCreator
    {
        public static void SendDoDamageAction(this IMessageBus messageBus, int damage, MoveEfficiency moveEfficiency, Entity target, bool critical, object sender = null)
        {
            messageBus.SendAction(new DoDamageAction(damage, moveEfficiency, target, critical), sender);
        }
        public static void SendEndTurnAction(this IMessageBus messageBus, object sender = null)
        {
            messageBus.SendAction(new EndTurnAction(), sender);
        }
        public static void SendExecuteNextCommandAction(this IMessageBus messageBus, object sender = null)
        {
            messageBus.SendAction(new ExecuteNextCommandAction(), sender);
        }
        public static void SendMoveMissedAction(this IMessageBus messageBus, object sender = null)
        {
            messageBus.SendAction(new MoveMissedAction(), sender);
        }
        public static void SendSetCommandAction(this IMessageBus messageBus, ICommand command, Entity action, object sender = null)
        {
            messageBus.SendAction(new SetCommandAction(command, action), sender);
        }
        public static void SendStartNewTurnAction(this IMessageBus messageBus, object sender = null)
        {
            messageBus.SendAction(new StartNewTurnAction(), sender);
        }
        public static void SendUseItemAction(this IMessageBus messageBus, Item item, Entity target, object sender = null)
        {
            messageBus.SendAction(new UseItemAction(item, target), sender);
        }
        public static void SendUseMoveAction(this IMessageBus messageBus, Move move, Entity source, Entity target, object sender = null)
        {
            messageBus.SendAction(new UseMoveAction(move, source, target), sender);
        }
        public static void SendUsePokemonChange(this IMessageBus messageBus, Pokemon pokemon, Entity target, object sender = null)
        {
            messageBus.SendAction(new UsePokemonChange(pokemon, target), sender);
        }
    }
}