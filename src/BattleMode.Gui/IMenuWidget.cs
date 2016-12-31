﻿using System;
using GameEngine.Globals;
using GameEngine.GUI;
using GameEngine.GUI.Graphics;

namespace BattleMode.Gui
{
    public interface IMenuWidget<T> : IInputHandler, IGraphicComponent
    {
        event EventHandler ExitRequested;
        event EventHandler<SelectionEventArgs<T>> ItemSelected;

        void ResetSelection();
    }
}