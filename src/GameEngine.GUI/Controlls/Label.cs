using GameEngine.GUI.Renderers;

namespace GameEngine.GUI.Controlls
{
    public class Label : AbstractGuiComponent
    {
        private string _text;
        private float _textHeight = 32;

        public Label()
        {
            IsSelectable = false;
            Text = null;
        }

        public string Text
        {
            get => _text;
            set
            {
                _text = value ?? "";
                Invalidate();
            }
        }

        public float TextHeight
        {
            get => _textHeight;
            set
            {
                _textHeight = value;
                Invalidate();
            }
        }
    }
}