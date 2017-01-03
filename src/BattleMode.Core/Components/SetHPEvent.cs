using System;
using BattleMode.Core.Components.GraphicComponents;
using BattleMode.Shared;
using GameEngine.Components;

namespace BattleMode.Core.Components
{
    internal class SetHpEvent : IEvent
    {
        public event EventHandler EventProcessed
        {
            add { _graphic.HpSet += value; }
            remove { _graphic.HpSet -= value; }
        }

        private readonly IBattleGraphicController _graphic;
        private readonly int _hp;
        private readonly ClientIdentifier _id;

        public SetHpEvent(IBattleGraphicController graphic, ClientIdentifier id, int hp)
        {
            _graphic = graphic;
            _id = id;
            _hp = hp;
        }

        public void Dispatch()
        {
            _graphic.SetHp(_id, _hp);
        }
    }
}
