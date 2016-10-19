using Base.Data;
using GameEngine;
using System;
using BattleLib.Components.GraphicComponents;
using GameEngine.GameEngineComponents;

namespace BattleLib.Components
{
    internal class SetStatusEvent : IEvent
    {
        private readonly StatusCondition condition;
        private readonly IBattleGraphicController graphic;
        private readonly ClientIdentifier id;

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