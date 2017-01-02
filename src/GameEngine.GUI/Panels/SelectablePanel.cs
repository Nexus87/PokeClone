using System;
using GameEngine.Globals;
using GameEngine.Graphics.General;
using GameEngine.GUI.Renderers;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Panels
{
    [GameType]
    public class SelectablePanel : AbstractPanel
    {
        private readonly SelectablePanelRenderer _renderer;
        private IGraphicComponent _content;
        public bool ShouldHandleKeyInput { get; set; }

        public SelectablePanel(SelectablePanelRenderer renderer)
        {
            _renderer = renderer;
            IsSelectable = true;
        }

        public IGraphicComponent Content
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


        protected override void Update()
        {
            Content.Area = _renderer.GetContentArea(this);
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            _renderer.Render(batch, this);
            _content.Draw(time, batch);
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