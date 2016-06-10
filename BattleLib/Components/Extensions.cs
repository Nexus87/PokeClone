using Base.Data;
using BattleLib.Components.BattleState;
using BattleLib.GraphicComponents;
using GameEngine;

namespace BattleLib.Components
{
    public static class Extensions
    {
        public static void AddHPEvent(this IEventQueue queue, IBattleGraphicController graphic, ClientIdentifier id, int hp)
        {
            queue.AddEvent(new SetHPEvent(graphic, id, hp));
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