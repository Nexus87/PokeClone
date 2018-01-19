using System;
using GameEngine.Globals;
using GameEngine.GUI.Renderers;

namespace GameEngine.GUI.Panels
{
    public class SelectablePanel : AbstractPanel
    {
        private IGuiComponent _content;
        public bool ShouldHandleKeyInput { get; set; }

        public SelectablePanel()
        {
            IsSelectable = true;
        }

        public IGuiComponent Content
        {
            get { return _content; }
            set
            {
                if (Content != null)
                    RemoveChild(Content);
                _content = value;
                AddChild(Content);
            }
        }

        public bool Enabled
        {
            get { return IsSelectable; }
            set { IsSelectable = value; }
        }

        public override void HandleKeyInput(CommandKeys key)
        {
            if (ShouldHandleKeyInput && key == CommandKeys.Select)
            {
                OnPanelPressed();
            }

        }

        public event EventHandler PanelPressed;

        protected virtual void OnPanelPressed()
        {
            PanelPressed?.Invoke(this, EventArgs.Empty);
        }
    }
}