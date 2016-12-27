using System;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.GUI;

namespace BattleMode.Gui
{
    public interface IMenuWidget<T> : IWidget
    {
        event EventHandler ExitRequested;
        event EventHandler<SelectionEventArgs<T>> ItemSelected;

        void ResetSelection();
    }
}
