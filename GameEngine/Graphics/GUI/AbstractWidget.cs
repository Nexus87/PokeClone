using Microsoft.Xna.Framework;
using System;

namespace GameEngine.Graphics.GUI
{
    public abstract class AbstractWidget : AbstractGraphicComponent, IWidget
    {

        public abstract bool HandleInput(CommandKeys key);
    }
}