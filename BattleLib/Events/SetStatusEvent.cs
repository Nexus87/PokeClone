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
    class SetStatusEvent : IEvent
    {
        public event EventHandler OnEventProcessed = delegate { };

        private IBattleGraphicService graphic;
        private bool player;
        private StatusCondition condition;

        public SetStatusEvent(IBattleGraphicService graphic, bool player, StatusCondition condition)
        {
            this.graphic = graphic;
            this.player = player;
            this.condition = condition;
        }

        public void Dispatch()
        {
            graphic.ConditionSet += ConditionSetHandler;
            if (player)
                graphic.SetPlayerPKMNStatus(condition);
            else
                graphic.SetAIPKMNStatus(condition);
        }

        private void ConditionSetHandler(object sender, EventArgs e)
        {
            graphic.ConditionSet -= ConditionSetHandler;
            OnEventProcessed(this, EventArgs.Empty);
        }
    }
}
