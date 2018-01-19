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
        private IGuiComponent _component;
        private readonly Dictionary<CommandKeys, Action> _inputListeners = new Dictionary<CommandKeys, Action>();

        public void SetContent(IGuiComponent component)
        {
            if (_component != null)
                RemoveChild(_component);
            _component = component;
            AddChild(component);
        }

        public override void Update()
        {
            if(!NeedsUpdate)
                return;

            _component.SetCoordinates(Area.X + 20, Area.Y + 20, Area.Width - 40, Area.Height - 40);
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