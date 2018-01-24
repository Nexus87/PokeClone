using System;
using System.Collections.Generic;
using GameEngine.Globals;

namespace GameEngine.GUI.Panels
{
    public class Window : AbstractPanel
    {
        private IGuiComponent _content;
        private readonly Dictionary<CommandKeys, Action> _inputListeners = new Dictionary<CommandKeys, Action>();

        public IGuiComponent Content { 
            get => _content; 
            set => SetContent(value); 
        }

        private void SetContent(IGuiComponent component)
        {
            RemoveChild(_content);
            _content = component;
            AddChild(_content);
            Invalidate();
        }

        public override void HandleKeyInput(CommandKeys key)
        {
            if (_inputListeners.ContainsKey(key))
                _inputListeners[key]();
            else
                _content?.HandleKeyInput(key);
        }

        public void SetInputListener(CommandKeys key, Action action)
        {
            _inputListeners[key] = action;
        }
    }
}