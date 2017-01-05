using Base.Data;
using BattleMode.Entities.BattleState;
using BattleMode.Graphic;
using BattleMode.Gui;
using BattleMode.Shared;
using GameEngine.Entities;

namespace BattleMode.Core.Components
{
    public static class Extensions
    {
        public static void AddHpEvent(this IEventQueue queue, IGuiController guiController, ClientIdentifier id, int hp)
        {
            queue.AddEvent(new SetHpEvent(guiController, id, hp));
        }

        public static void AddSetPokemonEvent(this IEventQueue queue, IBattleGraphicController service, IGuiController guiController, ClientIdentifier id, PokemonWrapper pokemon)
        {
            queue.AddEvent(new SetPokemonEvent(service, guiController, id, pokemon));
        }
        public static void AddShowMenuEvent(this IEventQueue queue, IGuiController controller)
        {
            queue.AddEvent(new ShowMenuEvent(controller));
        }
        public static void AddShowMessageEvent(this IEventQueue queue, IGuiController controller, string message)
        {
            queue.AddEvent(new ShowMessageEvent(controller, message));
        }
        public static void AddStatusEvent(this IEventQueue queue, IBattleGraphicController graphic, ClientIdentifier id, StatusCondition condition)
        {
            queue.AddEvent(new SetStatusEvent(graphic, id, condition));
        }
    }
}