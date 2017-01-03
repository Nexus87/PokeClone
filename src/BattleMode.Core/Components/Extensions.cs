using Base.Data;
using BattleMode.Core.Components.GraphicComponents;
using BattleMode.Entities.BattleState;
using BattleMode.Gui;
using BattleMode.Shared;
using GameEngine.Entities;

namespace BattleMode.Core.Components
{
    public static class Extensions
    {
        public static void AddHpEvent(this IEventQueue queue, IBattleGraphicController graphic, ClientIdentifier id, int hp)
        {
            queue.AddEvent(new SetHpEvent(graphic, id, hp));
        }

        public static void AddSetPokemonEvent(this IEventQueue queue, IBattleGraphicController service, ClientIdentifier id, PokemonWrapper pokemon)
        {
            queue.AddEvent(new SetPokemonEvent(service, id, pokemon));
        }
        public static void AddShowMenuEvent(this IEventQueue queue, IGUIService service)
        {
            queue.AddEvent(new ShowMenuEvent(service));
        }
        public static void AddShowMessageEvent(this IEventQueue queue, IGUIService service, string message)
        {
            queue.AddEvent(new ShowMessageEvent(service, message));
        }
        public static void AddStatusEvent(this IEventQueue queue, IBattleGraphicController graphic, ClientIdentifier id, StatusCondition condition)
        {
            queue.AddEvent(new SetStatusEvent(graphic, id, condition));
        }
    }
}