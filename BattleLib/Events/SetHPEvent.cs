using BattleLib.GraphicComponents;
using GameEngine.EventComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Events
{
    class SetHPEvent : IEvent
    {
        public event EventHandler OnEventProcessed = delegate { };
        private IBattleGraphicService graphic;
        private bool player;
        private int hp;

        public SetHPEvent(IBattleGraphicService graphic, bool player, int hp)
        {
            this.graphic = graphic;
            this.player = player;
            this.hp = hp;
        }

        public void Dispatch()
        {
            graphic.OnHPSet += HPSetHandler;
            if (player)
                graphic.SetPlayerHP(hp);
            else
                graphic.SetAIHP(hp);
        }

        private void HPSetHandler(object sender, EventArgs e)
        {
            graphic.OnHPSet -= HPSetHandler;
            OnEventProcessed(this, null);
        }
    }
}
