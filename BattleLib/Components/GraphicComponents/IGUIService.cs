using System;

namespace BattleLib.Components.GraphicComponents
{
    public interface IGUIService
    {
        event EventHandler TextDisplayed;
        event EventHandler MenuShowed;

        void ShowMenu();
        void SetText(string Text);
    }
}
