using System;

namespace BattleMode.Gui
{
    public interface IGUIService
    {
        event EventHandler TextDisplayed;
        event EventHandler MenuShowed;

        void ShowMenu();
        void SetText(string text);
    }
}
