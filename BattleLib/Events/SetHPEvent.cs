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
    class SetHPEvent : IEvent
    {
        public event EventHandler OnEventProcessed
        {
            add { graphic.OnHPSet += value; }
            remove { graphic.OnHPSet -= value; }
        }

        private IBattleGraphicService graphic;
        private int hp;
        private ClientIdentifier id;

        public SetHPEvent(IBattleGraphicService graphic, ClientIdentifier id, int hp)
        {
            this.graphic = graphic;
            this.id = id;
            this.hp = hp;
        }

        public void Dispatch()
        {
            graphic.SetHP(id, hp);
        }
    }
}
