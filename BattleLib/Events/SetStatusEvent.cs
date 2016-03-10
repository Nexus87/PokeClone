using Base.Data;
using BattleLib.Components.BattleState;
using BattleLib.GraphicComponents;
using GameEngine.EventComponent;
using System;

namespace BattleLib.Events
{
    internal class SetStatusEvent : IEvent
    {
        private StatusCondition condition;
        private IBattleGraphicService graphic;
        private ClientIdentifier id;

        public SetStatusEvent(IBattleGraphicService graphic, ClientIdentifier id, StatusCondition condition)
        {
            this.graphic = graphic;
            this.id = id;
            this.condition = condition;
        }

        public event EventHandler OnEventProcessed
        {
            add { graphic.ConditionSet += value; }
            remove { graphic.ConditionSet -= value; }
        }

        public void Dispatch()
        {
            graphic.SetPokemonStatus(id, condition);
        }
    }
}