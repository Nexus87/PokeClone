using GameEngine.Core.ECS;

namespace BattleMode.Gui.Actions
{
    public static class ActionCreator
    {
        public static void SendChangeHpAction(this IMessageBus messageBus, int diff, Entity target, object sender)
        {
            messageBus.SendAction(new ChangeHpAction(diff, target), sender);
        }
        public static void SendShowMainMenuAction(this IMessageBus messageBus, object sender)
        {
            messageBus.SendAction(new ShowMainMenuAction(), sender);
        }
        public static void SendShowMenuAction(this IMessageBus messageBus, MainMenuEntries entry, object sender)
        {
            messageBus.SendAction(new ShowMenuAction(entry), sender);
        }
        public static void SendShowMessageAction(this IMessageBus messageBus, string text, object sender)
        {
            messageBus.SendAction(new ShowMessageAction(text), sender);
        }
    }
}