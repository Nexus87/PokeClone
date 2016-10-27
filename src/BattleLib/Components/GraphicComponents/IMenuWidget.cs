using GameEngine.Graphics;
using GameEngine.Graphics.GUI;
using System;

namespace BattleLib.Components.GraphicComponents
{
    public interface IMenuWidget<T> : IWidget
    {
        event EventHandler ExitRequested;
        event EventHandler<SelectionEventArgs<T>> ItemSelected;

        void ResetSelection();
    }
}
