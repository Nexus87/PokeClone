﻿using System;
using Microsoft.Xna.Framework;

namespace GameEngine.Graphics.NewGUI
{
    public enum ResizeBehavoir
    {
        Auto,
        Preferred
    }

    public interface IArea
    {
        event EventHandler PreferedSizeChanged;

        Rectangle Constraints { get; set; }
        Rectangle ScissorArea { get; set; }

        float PreferedWidth { get; }
        float PreferedHeight { get; }

    }
}