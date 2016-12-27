using System;
using Base.Data;
using BattleMode.Core.Components.GraphicComponents;
using BattleMode.Shared;
using GameEngine.Core.GameEngineComponents;

namespace BattleMode.Core.Components
{
    internal class SetStatusEvent : IEvent
    {
        private readonly StatusCondition _condition;
        private readonly IBattleGraphicController _graphic;
        private readonly ClientIdentifier _id;

        public SetStatusEvent(IBattleGraphicController graphic, ClientIdentifier id, StatusCondition condition)
        {
            _graphic = graphic;
            _id = id;
            _condition = condition;
        }

        public event EventHandler EventProcessed
        {
            add { _graphic.ConditionSet += value; }
            remove { _graphic.ConditionSet -= value; }
        }

        public void Dispatch()
        {
            _graphic.SetPokemonStatus(_id, _condition);
        }
    }
}