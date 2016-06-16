using Microsoft.Xna.Framework;
using System;

namespace GameEngine.Graphics.GUI
{
    public abstract class AbstractWidget : AbstractGraphicComponent, IWidget
    {
        private bool isVisible;

        public event EventHandler<VisibilityChangedEventArgs> VisibilityChanged;

        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                if (value == isVisible)
                    return;

                isVisible = value;
                VisibilityChanged(this, new VisibilityChangedEventArgs(isVisible));
            }
        }

        public abstract bool HandleInput(CommandKeys key);
    }
}