using System;
using BattleMode.Core.Components.GraphicComponents;
using GameEngine.Core.GameEngineComponents;

namespace BattleMode.Core.Components
{
    internal class SetHPEvent : IEvent
    {
        public event EventHandler EventProcessed
        {
            add { graphic.HpSet += value; }
            remove { graphic.HpSet -= value; }
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
            graphic.SetHp(id, hp);
        }
    }
}
