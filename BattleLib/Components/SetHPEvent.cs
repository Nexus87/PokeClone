using BattleLib.GraphicComponents;
using GameEngine;
using System;

namespace BattleLib.Components
{
    class SetHPEvent : IEvent
    {
        public event EventHandler EventProcessed
        {
            add { graphic.OnHPSet += value; }
            remove { graphic.OnHPSet -= value; }
        }

        readonly IBattleGraphicController graphic;
        readonly int hp;
        readonly ClientIdentifier id;

        public SetHPEvent(IBattleGraphicController graphic, ClientIdentifier id, int hp)
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
