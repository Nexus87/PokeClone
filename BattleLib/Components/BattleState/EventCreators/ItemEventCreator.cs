using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleLib.Components.BattleState.Commands;
using GameEngine.EventComponent;
using BattleLib.Events;
using BattleLib.GraphicComponents;
using Base;

namespace BattleLib.Components.BattleState.EventCreators
{
    class ItemEventCreator : IEventCreator
    {
        private List<IEvent> events = new List<IEvent>();
        
        private IEventQueue queue;
        private IGUIService gui;
        private IBattleGraphicService graphic;

        private BattleData data;
        private ClientIdentifier identifier;

        public ItemEventCreator(IEventQueue queue, IGUIService gui, IBattleGraphicService graphic, BattleData data)
        {
            this.gui = gui;
            this.queue = queue;
            this.data = data;
            this.graphic = graphic;
        }

        public void ActionFailedHandler()
        {
            events.Add(new ShowMessageEvent(gui, "Failed"));
        }

        public void ConditionChangedHandler(OnConditionChangedArgs e)
        {
            string message;
            if (e.condition == StatusCondition.Normal)
                message = e.pkmn.Name + " is no longer " + e.oldCondition;
            else
                message = e.pkmn.Name + " is " + e.condition;

            events.Add(new ShowMessageEvent(gui, message));
            events.Add(new SetStatusEvent(graphic, identifier == data.player, e.condition));
        }


        public void DamageTakenHandler(OnDamageTakenArgs e)
        {
            events.Add(new SetHPEvent(graphic, identifier == data.player, e.newHP));
        }

        public void EndCommand()
        {
            foreach (var e in events)
                queue.AddEvent(e);
        }

        public void StartCommand(ICommand command)
        {
            identifier = command.Source;
            events.Clear();
        }

        public void StatsChangedHandler(OnStatsChangedArgs e)
        {
            string message = e.pkmn.Name + "'s " + e.state + (e.lowered ? " was lowered." : " rises.");
            events.Add(new ShowMessageEvent(gui, message));
        }


        public void ItemUsed(ItemUsedArgs e)
        {
            string message = identifier.Name + " uses " + e.item.Name;
            events.Add(new ShowMessageEvent(gui, message));
        }

        public void MoveUsed(MoveUsedArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
