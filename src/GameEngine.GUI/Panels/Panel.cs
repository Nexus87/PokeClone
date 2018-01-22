using System;
using System.Collections.Generic;
using GameEngine.Globals;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Panels
{
    public class Panel : AbstractPanel
    {
        private readonly Dictionary<CommandKeys, Action> _inputListeners = new Dictionary<CommandKeys, Action>();
        private IGuiComponent _component;
        public Color BackgroundColor { get; set; }

        public int MarginLeft { get; set; } = 20;
        public int MarginRight { get; set; } = 20;
        public int MarginTop { get; set; } = 20;
        public int MarginBottom { get; set; } = 20;

        public void SetContent(IGuiComponent component)
        {
            if (_component != null)
                RemoveChild(_component);
            _component = component;
            AddChild(component);
        }

        public override void Update()
        {
            if (!NeedsUpdate)
                return;

            _component.SetCoordinates(
                Area.X + MarginLeft,
                Area.Y + MarginTop,
                Area.Width - (MarginLeft + MarginRight),
                Area.Height - (MarginTop + MarginBottom));
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