﻿using GameEngine.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace GameEngine.Graphics.GUI
{
    public class GUIManager : IInputHandler
    {
        private IWidget FocusedWidget { get { return widgets.Count == 0 ? null : widgets.Last.Value; } }
        private LinkedList<IWidget> widgets = new LinkedList<IWidget>();
        private LinkedList<IWidget> notVisibleWidgets = new LinkedList<IWidget>();

        public void AddWidget(IWidget widget)
        {
            widget.CheckNull("widget");

            widget.VisibilityChanged += VisibilityChangedHandler;

            if (widget.IsVisible)
                widgets.AddLast(widget);
            else
                notVisibleWidgets.AddLast(widget);
        }

        void VisibilityChangedHandler(object sender, VisibilityChangedEventArgs e)
        {
            var widget = (IWidget)sender;
            if (e.Visible == false)
            {
                widgets.Remove(widget);
                notVisibleWidgets.AddLast(widget);
            }
            else
            {
                notVisibleWidgets.Remove(widget);
                widgets.AddLast(widget);
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

            return widgets.Contains(widget) || notVisibleWidgets.Contains(widget);
        }

        private void RemoveWidgetFromList(IWidget widget)
        {
            if (widget.IsVisible)
                widgets.Remove(widget);
            else
                notVisibleWidgets.Remove(widget);
        }

        public GUIManager()
        {
            IsActive = false;
        }

        private bool IsActive { get; set; }

        public bool HandleInput(CommandKeys key)
        {
            if (FocusedWidget == null)
                return false;

            return FocusedWidget.HandleInput(key);
        }

        public void Close()
        {
            IsActive = false;
        }

        public void Draw(GameTime time, ISpriteBatch batch)
        {
            if (!IsActive)
                return;
            foreach (var w in widgets)
                w.Draw(time, batch);
        }

        public void Show()
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