using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine.Core.ECS.Actions;
using GameEngine.Core.ECS.Components;
using GameEngine.Graphics.General;
using GameEngine.GUI;
using Microsoft.Xna.Framework;

namespace GameEngine.Core.ECS.Systems
{
    public class GuiSystem
    {
        private readonly List<WidgetItem> _widgets = new List<WidgetItem>();

        private IGuiComponent FocusedWidget => _widgets.LastOrDefault()?.Component;
        public GuiFactory Factory { get; set; }

        public void Update(TimeAction action, IEntityManager entityManager)
        {
            var guiComponent = entityManager.GetFirstComponentOfType<GuiComponent>();
            if (!guiComponent.GuiVisible)
            {
                return;
            }

            var spriteBatch = guiComponent.SpriteBatch;

            spriteBatch.GraphicsDevice.Clear(Color.Transparent);
            spriteBatch.Begin();
            Draw(spriteBatch, guiComponent.Skin);
            spriteBatch.End();
        }

        public void ClearScreen(ISpriteBatch spriteBatch)
        {
            spriteBatch.GraphicsDevice.Clear(Color.Transparent);
        }

        public void SetWidgetVisibility(SetGuiComponentVisibleAction action, IEntityManager entityManager)
        {
            if (action.IsVisble)
            {
                ShowWidget(action.Widget, action.Priority);
            }
            else
            {
                RemoveWidget(action.Widget);
            }
        }

        private void ShowWidget(IGuiComponent widget, int? priority = null)
        {
            if (_widgets.Any(x => x.Component == widget))
                return;

            if (!priority.HasValue)
                priority = _widgets.Count == 0 ? 0 : _widgets.Max(x => x.Priority) + 1;

            _widgets.Add(new WidgetItem(priority.Value, widget));
            _widgets.Sort();
        }

        private void RemoveWidget(IGuiComponent widget)
        {
            var w = _widgets.FirstOrDefault(x => x.Component == widget);
            if (w != null)
                _widgets.Remove(w);
        }

        public void SetGuiVisiblity(SetGuiVisibleAction action, IEntityManager entityManager)
        {
            entityManager.GetFirstComponentOfType<GuiComponent>().GuiVisible = action.IsVisible;
        }

        public void HandleInput(GuiKeyInputAction action, IEntityManager entityManager)
        {
            FocusedWidget?.HandleKeyInput(action.Key);
        }

        private void Draw(ISpriteBatch batch, ISkin skin)
        {
            var completeArea = batch.GraphicsDevice.ScissorRectangle;
            _widgets.ForEach(x => DrawRecursive(x.Component, batch, completeArea, skin));
            batch.GraphicsDevice.ScissorRectangle = completeArea;
        }

        private void DrawRecursive(IGuiComponent component, ISpriteBatch batch,
            Rectangle parentRectangle, ISkin skin)
        {
            var componentRectangle = Rectangle.Intersect(parentRectangle, component.Area);
            batch.GraphicsDevice.ScissorRectangle = componentRectangle;

            component.Update();
            skin.GetRendererForComponent(component.GetType())?
                .Render(batch, component);

            foreach (var componentChild in component.Children)
                DrawRecursive(componentChild, batch, componentRectangle, skin);
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