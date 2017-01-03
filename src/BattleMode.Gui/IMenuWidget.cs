using System;
using GameEngine.Globals;
using GameEngine.GUI;

namespace BattleMode.Gui
{
    public interface IMenuWidget<T> : IInputHandler, IGuiComponent
    {
        event EventHandler ExitRequested;
        event EventHandler<SelectionEventArgs<T>> ItemSelected;

        void ResetSelection();
    }
}
