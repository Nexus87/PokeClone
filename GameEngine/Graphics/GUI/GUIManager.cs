using GameEngine.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace GameEngine.Graphics.GUI
{
    public class GUIManager : IInputHandler
    {
        private IWidget focusedWidget;
        private LinkedList<IWidget> widgets = new LinkedList<IWidget>();
        private LinkedList<IWidget> notVisibleWidgets = new LinkedList<IWidget>();

        public void AddWidget(IWidget widget)
        {
            widget.CheckNull("widget");

            widget.VisibilityChanged += widget_OnVisibilityChanged;
            if (widget.IsVisible)
            {
                focusedWidget = widget;
                widgets.AddLast(widget);
            }
            else
                notVisibleWidgets.AddLast(widget);
        }

        void widget_OnVisibilityChanged(object sender, VisibilityChangedEventArgs e)
        {
            var widget = (IWidget)sender;
            if (e.Visible == false)
            {
                widgets.Remove(widget);
                notVisibleWidgets.AddLast(widget);

                if (focusedWidget == widget)
                    focusedWidget = widgets.Last.Value;
            }
            else
            {
                focusedWidget = widget;
                notVisibleWidgets.Remove(widget);
                widgets.AddLast(widget);
            }
        }


        public void RemoveWidget(IWidget widget)
        {
            widget.CheckNull("widget");

            if(widget.IsVisible)
                widgets.Remove(widget);
            else
                notVisibleWidgets.Remove(widget);
        }

        public void FocusWidget(IWidget widget)
        {
            if (!widgets.Remove(widget))
                return;

            widgets.AddLast(widget);
            focusedWidget = widget;
        }

        public GUIManager()
        {
            IsActive = false;
        }

        public event EventHandler GUIClose = delegate { };

        internal bool IsActive { get; private set; }

        public bool HandleInput(CommandKeys key)
        {
            if (focusedWidget == null)
                return false;

            return focusedWidget.HandleInput(key);
        }

        internal void Close()
        {
            IsActive = false;
        }

        internal void Draw(GameTime time, ISpriteBatch batch)
        {
            if (!IsActive)
                return;
            foreach (var w in widgets)
                w.Draw(time, batch);
        }

        internal void Show()
        {
            IsActive = true;
        }

        public void Setup()
        {
            foreach (var w in widgets)
                w.Setup();
            foreach (var w in notVisibleWidgets)
                w.Setup();
        }
    }
}