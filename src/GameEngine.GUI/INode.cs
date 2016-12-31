using System;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI
{
    public interface INode
    {
        event EventHandler PreferedSizeChanged;

        Rectangle Constraints { get; set; }
        Rectangle ScissorArea { get; set; }

        float PreferedWidth { get; }
        float PreferedHeight { get; }

    }
}