using GameEngine.Graphics.Basic;
using GameEngine.Graphics.Layouts;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;

namespace GameEngine.Graphics.Widgets
{
    public class MessageBox : ForwardingGraphicComponent<MultlineTextBox>, IWidget
    {
        private MultlineTextBox textBox;

        public MessageBox(Configuration config, PokeEngine game)
            : base(new MultlineTextBox(game), game)
        {
            textBox = InnerComponent;
        }

        public event EventHandler OnAllLineShowed = delegate { };
        public event EventHandler<VisibilityChangedEventArgs> OnVisibilityChanged = delegate { };

        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                if (value == isVisible)
                    return;

                isVisible = value;
                OnVisibilityChanged(this, new VisibilityChangedEventArgs(isVisible));
            }
        }
        private bool isVisible;
        public void DisplayText(string text)
        {
            textBox.Text = text;
        }

        public bool HandleInput(CommandKeys key)
        {
            if (key != CommandKeys.Select)
                return false;

            if (textBox.HasNext())
                textBox.NextLine();
            else
                OnAllLineShowed(this, null);

            return true;
        }

        public void ResetText()
        {
            textBox.Text = "";
        }

        protected override void Update()
        {
        }
    }
}