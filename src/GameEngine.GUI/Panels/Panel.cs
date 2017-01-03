using System;
using System.Collections.Generic;
using GameEngine.Globals;
using GameEngine.Graphics.General;
using GameEngine.GUI.Renderers;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Panels
{
    public class Panel : AbstractPanel
    {
        public Color BackgroundColor { get; set; }
        private readonly PanelRenderer _renderer;
        private IGuiComponent _component;
        private Dictionary<CommandKeys, Action> _inputListeners = new Dictionary<CommandKeys, Action>();

        public Panel(PanelRenderer renderer)
        {
            _renderer = renderer;
        }

        public void SetContent(IGuiComponent component)
        {
            if (_component != null)
                RemoveChild(_component);
            _component = component;
            AddChild(component);
        }

        protected override void Update()
        {

            _component.SetCoordinates(Area.X + 20, Area.Y + 20, Area.Width - 40, Area.Height - 40);
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            _renderer.Render(batch, this);
            _component?.Draw(time, batch);
        }

        public void AddInputListener(CommandKeys key, Action action)
        {
            _inputListeners[key] = action;
        }

        public override void HandleKeyInput(CommandKeys key)
        {
            if (_inputListeners.ContainsKey(key))
                _inputListeners[key]();
            else
                _component.HandleKeyInput(key);
        }
    }
}