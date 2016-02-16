using GameEngine.Graphics.Widgets;
using GameEngine.Wrapper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GameEngine.Graphics
{
    public class GUIManager : IInputHandler
    {
        private IWidget focusedWidget;
        private LinkedList<IWidget> widgets = new LinkedList<IWidget>();
        private LinkedList<IWidget> notVisibleWidgets = new LinkedList<IWidget>();

        public void AddWidget(IWidget widget)
        {
            widget.OnVisibilityChanged += widget_OnVisibilityChanged;
            if (widget.IsVisible)
            {
                focusedWidget = widget;
                widgets.AddLast(widget);
            }
            else
                notVisibleWidgets.AddLast(widget);
        }

        void widget_OnVisibilityChanged(object sender, VisibilityChangedArgs e)
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

        public bool HandleInput(Keys key)
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

        public void Setup(ContentManager Content)
        {
            foreach (var w in widgets)
                w.Setup(Content);
            foreach (var w in notVisibleWidgets)
                w.Setup(Content);
        }
    }
}