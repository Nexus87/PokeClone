﻿using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine.Globals;
using GameEngine.Graphics.General;
using GameEngine.GUI;
using Microsoft.Xna.Framework;

namespace GameEngine.Core.ECS.Systems
{
    public class GuiSystem
    {
        private readonly List<WidgetItem> _widgets = new List<WidgetItem>();
        private readonly ISkin _skin;

        public GuiSystem(ISkin skin)
        {
            _skin = skin;
        }

        private IGuiComponent FocusedWidget => _widgets.LastOrDefault()?.Component;

        public void Update(GameTime time, ISpriteBatch spriteBatch)
        {
            spriteBatch.GraphicsDevice.Clear(Color.Transparent);
            spriteBatch.Begin();
            Draw(spriteBatch);
            spriteBatch.End();
        }

        public void ClearScreen(ISpriteBatch spriteBatch)
        {
            spriteBatch.GraphicsDevice.Clear(Color.Transparent);
        }


        public void ShowWidget(IGuiComponent widget, int? priority = null)
        {
            if (_widgets.Any(x => x.Component == widget))
                return;

            if (!priority.HasValue)
                priority = _widgets.Count == 0 ? 0 : _widgets.Max(x => x.Priority) + 1;

            _widgets.Add(new WidgetItem(priority.Value, widget));
            _widgets.Sort();
        }

        public void CloseWidget(IGuiComponent widget)
        {
            var w = _widgets.FirstOrDefault(x => x.Component == widget);
            if (w != null)
                _widgets.Remove(w);
        }


        public void HandleInput(CommandKeys key)
        {
            FocusedWidget?.HandleKeyInput(key);
        }

        private void Draw(ISpriteBatch batch)
        {
            var completeArea = batch.GraphicsDevice.ScissorRectangle;
            _widgets.ForEach(x => DrawRecursive(x.Component, batch, completeArea));
            batch.GraphicsDevice.ScissorRectangle = completeArea;
        }

        private void DrawRecursive(IGuiComponent component, ISpriteBatch batch,
            Rectangle parentRectangle)
        {
            var componentRectangle = Rectangle.Intersect(parentRectangle, component.Area);
            batch.GraphicsDevice.ScissorRectangle = componentRectangle;

            component.Update();
            _skin.GetRendererForComponent(component.GetType())?
                .Render(batch, component);

            foreach (var componentChild in component.Children)
                DrawRecursive(componentChild, batch, componentRectangle);
        }

        private class WidgetItem : IComparable<WidgetItem>
        {
            public readonly IGuiComponent Component;
            public readonly int Priority;

            public WidgetItem(int priority, IGuiComponent component)
            {
                Priority = priority;
                Component = component;
            }

            public int CompareTo(WidgetItem other)
            {
                return Priority.CompareTo(other.Priority);
            }

            public override bool Equals(object obj)
            {
                var other = obj as WidgetItem;
                if (other == null)
                    return false;

                return Component == other.Component;
            }

            public override int GetHashCode()
            {
                return Component?.GetHashCode() ?? 0;
            }
        }
    }
}