using System;
using System.Collections.Generic;
using GameEngine.Globals;
using GameEngine.Graphics.General;
using GameEngine.GUI.Renderers;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace GameEngine.GUI.Panels
{
    [GameType]
    public class Window : AbstractPanel
    {
        private readonly WindowRenderer _renderer;
        private IGuiComponent _content;
        private readonly Dictionary<CommandKeys, Action> _inputListeners = new Dictionary<CommandKeys, Action>();

        public Window(WindowRenderer renderer)
        {
            _renderer = renderer;
        }

        public void SetContent(IGuiComponent component)
        {
            RemoveChild(_content);
            _content = component;
            AddChild(_content);
            Invalidate();
        }


        public override void Update()
        {
            if(!NeedsUpdate)
                return;

            _content?.SetCoordinates(
                Area.X + _renderer.LeftMargin,
                Area.Y + _renderer.TopMargin,
                Area.Width - (_renderer.RightMargin + _renderer.LeftMargin),
                Area.Height - (_renderer.BottomMargin + _renderer.TopMargin)
            );
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