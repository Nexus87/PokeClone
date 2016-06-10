using Base.Data;
using BattleLib.GraphicComponents;
using GameEngine;
using System;

namespace BattleLib.Components
{
    internal class SetStatusEvent : IEvent
    {
        private StatusCondition condition;
        private IBattleGraphicController graphic;
        private ClientIdentifier id;

        public SetStatusEvent(IBattleGraphicController graphic, ClientIdentifier id, StatusCondition condition)
        {
            this.graphic = graphic;
            this.id = id;
            this.condition = condition;
        }

        public event EventHandler EventProcessed
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