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
        private ItemCommand command;
        private IGUIService gui;
        private BattleData data;
        private PokemonWrapper source;

        public ItemEventCreator(IEventQueue queue, IGUIService gui, BattleData data)
        {
            this.gui = gui;
            this.queue = queue;
            this.data = data;
        }
        public void ActionFailedHandler()
        {
            events.Add(new ShowMessageEvent(gui, "Failed"));
        }

        public void ConditionChangedHandler(OnConditionChangedArgs e)
        {
            if (e.condition == StatusCondition.Normal)
                events.Add(new ShowMessageEvent(gui, source.Name + " is no longer " + e.oldCondition));
            else
                events.Add(new ShowMessageEvent(gui, source.Name + " is " + e.condition));

        public void DamageTakenHandler(OnDamageTakenArgs e)
        {
            
        }

        public void EndCommand()
        {
            throw new NotImplementedException();
        }

        public void StartCommand(ICommand command)
        {
            throw new NotImplementedException();
        }

        public void StatsChangedHandler(OnStatsChangedArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
