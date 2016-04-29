using System;

namespace BattleLib.GraphicComponents
{
    public interface IGUIService
    {
        event EventHandler TextDisplayed;
        event EventHandler MenuShowed;

        void ShowMenu();
        void SetText(string Text);
    }
}
