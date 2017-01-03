using System;
using BattleMode.Shared;
using GameEngine.Entities;

namespace BattleMode.Gui
{
    public interface IGuiEntity : IGameEntity
    {
        event EventHandler TextDisplayed;
        event EventHandler MenuShowed;

        void ShowMenu();
        void SetText(string text);
        void SetHp(int hp, ClientIdentifier target);
    }
}
