using System;
using BattleMode.Gui;
using BattleMode.Shared;
using GameEngine.Entities;

namespace BattleMode.Core.Components
{
    internal class SetHpEvent : IEvent
    {
        public event EventHandler EventProcessed
        {
            add { _guiController.HpSet += value; }
            remove { _guiController.HpSet -= value; }
        }

        private readonly IGuiController _guiController;
        private readonly int _hp;
        private readonly ClientIdentifier _id;

        public SetHpEvent(IGuiController guiController, ClientIdentifier id, int hp)
        {
            _id = id;
            _hp = hp;
            _guiController = guiController;
        }

        public void Dispatch()
        {
            _guiController.SetHp(_id, _hp);
        }
    }
}
