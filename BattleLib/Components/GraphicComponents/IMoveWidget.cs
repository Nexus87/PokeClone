using Base;
using GameEngine.Graphics.GUI;
using System;
namespace BattleLib.Components.GraphicComponents
{
    public interface IMoveWidget : IWidget
    {
        event EventHandler ExitRequested;
        event EventHandler<GameEngine.Graphics.SelectionEventArgs<Move>> ItemSelected;

        void ResetSelection();
    }
}
