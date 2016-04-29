﻿using GameEngine.Utils;
using System;

namespace GameEngine.Graphics.GUI
{
    public class MessageBox : ForwardingGraphicComponent<MultlineTextBox>, IWidget
    {
        private MultlineTextBox textBox;

        public MessageBox(ISpriteFont font, ITextSplitter splitter, int lineNumber = 2)
            : base(new MultlineTextBox(font, splitter, lineNumber))
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