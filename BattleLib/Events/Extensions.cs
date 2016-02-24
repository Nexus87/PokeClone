using Base;
using BattleLib.GraphicComponents;
using GameEngine.EventComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Events
{
    public static class Extensions
    {
        public static void AddHPEvent(this IEventQueue queue, IBattleGraphicService graphic, bool player, int hp)
        {
            queue.AddEvent(new SetHPEvent(graphic, player, hp));
        }

        public static void AddStatusEvent(this IEventQueue queue, IBattleGraphicService graphic, bool player, StatusCondition condition)
        {
            queue.AddEvent(new SetStatusEvent(graphic, player, condition));
        }

        public static void AddShowMenuEvent(this IEventQueue queue, IGUIService service)
        {
            queue.AddEvent(new ShowMenuEvent(service));
        }

        public static void AddShowMessageEvent(this IEventQueue queue, IGUIService service, string message)
        {
            queue.AddEvent(new ShowMessageEvent(service, message));
        }
    }
}
