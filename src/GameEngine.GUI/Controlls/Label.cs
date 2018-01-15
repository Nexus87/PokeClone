using GameEngine.GUI.Renderers;

namespace GameEngine.GUI.Controlls
{
    public class Label : AbstractGuiComponent
    {
        private readonly LabelRenderer _renderer;
        private string _text;
        private float _textHeight = 32;

        public Label(LabelRenderer renderer)
        {
            IsSelectable = false;
            _renderer = renderer;
            Text = null;
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value ?? "";
                Invalidate();
            }
        }

        public float TextHeight
        {
            get { return _textHeight; }
            set
            {
                _textHeight = value;
                Invalidate();
            }
        }

        public override void Update()
        {
            if (!NeedsUpdate)
                return;

            PreferredHeight = _renderer.GetPreferedHeight(this);
            PreferredWidth = _renderer.GetPreferedWidth(this);
        }
    }
}