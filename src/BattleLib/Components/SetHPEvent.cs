using GameEngine;
using System;
using BattleLib.Components.GraphicComponents;

namespace BattleLib.Components
{
    internal class SetHPEvent : IEvent
    {
        public event EventHandler EventProcessed
        {
            add { graphic.OnHPSet += value; }
            remove { graphic.OnHPSet -= value; }
        }

        private readonly IBattleGraphicController graphic;
        private readonly int hp;
        private readonly ClientIdentifier id;

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
