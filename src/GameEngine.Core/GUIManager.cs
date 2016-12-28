using System.Collections.Generic;
using GameEngine.Globals;
using GameEngine.GUI;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.General;
using GameEngine.GUI.Utils;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace GameEngine.Core
{
    [GameService(typeof(GuiManager))]
    public class GuiManager : IInputHandler
    {
        private IGraphicComponent FocusedWidget => _widgets.Count == 0 ? null : _widgets.Last.Value;
        private readonly LinkedList<IGraphicComponent> _widgets = new LinkedList<IGraphicComponent>();
        private readonly LinkedList<IGraphicComponent> _notVisibleWidgets = new LinkedList<IGraphicComponent>();

        public void AddWidget(IGraphicComponent widget)
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
            var widget = (IGraphicComponent)sender;
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

        public void RemoveWidget(IGraphicComponent widget)
        {
            if (!Contains(widget))
                return;

            RemoveWidgetFromList(widget);
            widget.VisibilityChanged -= VisibilityChangedHandler;
        }

        private bool Contains(IGraphicComponent widget)
        {
            if (widget == null)
                return false;

            return _widgets.Contains(widget) || _notVisibleWidgets.Contains(widget);
        }

        private void RemoveWidgetFromList(IGraphicComponent widget)
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