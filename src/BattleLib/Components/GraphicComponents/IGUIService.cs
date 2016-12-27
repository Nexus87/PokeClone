using System;

namespace BattleMode.Core.Components.GraphicComponents
{
    public interface IGUIService
    {
        event EventHandler TextDisplayed;
        event EventHandler MenuShowed;

        void ShowMenu();
        void SetText(string Text);
    }
}
