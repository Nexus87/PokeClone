﻿using System;

namespace GameEngine.Graphics.Widgets
{
    public interface IWidget : IGraphicComponent, IInputHandler
    {
        event EventHandler<VisibilityChangedArgs> OnVisibilityChanged;

        bool IsVisible { get; }
    }

    public class VisibilityChangedArgs : EventArgs
    {
    }
}