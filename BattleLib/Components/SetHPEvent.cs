using BattleLib.GraphicComponents;
using GameEngine;
using System;

namespace BattleLib.Components
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
