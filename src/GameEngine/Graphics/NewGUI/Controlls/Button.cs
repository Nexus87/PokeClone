using System;

namespace GameEngine.Graphics.NewGUI.Controlls
{
    public class Button : AbstractGraphicComponent
    {
        public event EventHandler ButtonPressed;

        public override void HandleKeyInput(CommandKeys key)
        {
            OnButtonPressed();
        }

        protected virtual void OnButtonPressed()
        {
            ButtonPressed?.Invoke(this, EventArgs.Empty);
        }
    }
}