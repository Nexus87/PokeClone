using System.Collections.Generic;
using GameEngine.Globals;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.General;
using GameEngine.GUI.Graphics.GUI;
using GameEngine.Registry;
using GameEngine.Utils;
using Microsoft.Xna.Framework;

namespace GameEngine
{
    [GameService(typeof(GuiManager))]
    public class GuiManager : IInputHandler
    {
        private IWidget FocusedWidget => _widgets.Count == 0 ? null : _widgets.Last.Value;
        private readonly LinkedList<IWidget> _widgets = new LinkedList<IWidget>();
        private readonly LinkedList<IWidget> _notVisibleWidgets = new LinkedList<IWidget>();

        public void AddWidget(IWidget widget)
        {
            widget.CheckNull("widget");

            widget.VisibilityChanged += VisibilityChangedHandler;

            if (widget.IsVisible)
                _widgets.AddLast(widget);
            else
                _notVisibleWidgets.AddLast(widget);
        }

        private void VisibilityChangedHandler(object sender, VisibilityChangedEventArgs e)
        {
            var widget = (IWidget)sender;
            if (e.Visible == false)
            {
                _widgets.Remove(widget);
                _notVisibleWidgets.AddLast(widget);
            }
            else
            {
                _notVisibleWidgets.Remove(widget);
                _widgets.AddLast(widget);
            }
        }

        public void RemoveWidget(IWidget widget)
        {
            if (!Contains(widget))
                return;

            RemoveWidgetFromList(widget);
            widget.VisibilityChanged -= VisibilityChangedHandler;
        }

        private bool Contains(IWidget widget)
        {
            if (widget == null)
                return false;

            return _widgets.Contains(widget) || _notVisibleWidgets.Contains(widget);
        }

        private void RemoveWidgetFromList(IWidget widget)
        {
            if (widget.IsVisible)
                _widgets.Remove(widget);
            else
                _notVisibleWidgets.Remove(widget);
        }

        public GuiManager()
        {
            IsActive = false;
        }

        private bool IsActive { get; set; }

        public void HandleKeyInput(CommandKeys key)
        {

            FocusedWidget?.HandleKeyInput(key);
        }

        public void Close()
        {
            IsActive = false;
        }

        public void Draw(GameTime time, ISpriteBatch batch)
        {
            if (!IsActive)
                return;
            foreach (var w in _widgets)
                w.Draw(time, batch);
        }

        public void Show()
        {
            IsActive = true;
        }

        public void Setup()
        {
            foreach (var w in _widgets)
                w.Setup();
            foreach (var w in _notVisibleWidgets)
                w.Setup();
        }
    }
}