using Base;
using BattleLib.Components.BattleState;
using BattleLib.GraphicComponents;
using GameEngine.EventComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Events
{
    class SetStatusEvent : IEvent
    {
        public event EventHandler OnEventProcessed
        {
            add { graphic.ConditionSet += value; }
            remove { graphic.ConditionSet -= value; }
        }

        private IBattleGraphicService graphic;
        private StatusCondition condition;
        private ClientIdentifier id;

        public SetStatusEvent(IBattleGraphicService graphic, ClientIdentifier id, StatusCondition condition)
        {
            this.graphic = graphic;
            this.id = id;
            this.condition = condition;
        }

        public void Dispatch()
        {
            graphic.SetPokemonStatus(id, condition);
        }
    }
}
