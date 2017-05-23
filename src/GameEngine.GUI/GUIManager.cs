using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine.Globals;
using GameEngine.Graphics.General;
using GameEngine.GUI;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace GameEngine.Core
{
    [GameService(typeof(GuiManager))]
    internal class GuiManager : IInputHandler
    {
        private class WidgetItem : IComparable<WidgetItem>
        {
            public readonly int Priority;
            public readonly IGuiComponent Component;

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

        private readonly List<WidgetItem> _widgets = new List<WidgetItem>();


        public void ShowWidget(IGuiComponent widget, int? priority = null)
        {
            if(_widgets.Any(x => x.Component == widget))
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

        public bool IsActive { get; set; }


        public void HandleKeyInput(CommandKeys key)
        {

            FocusedWidget?.HandleKeyInput(key);
        }

        public IGuiComponent FocusedWidget => _widgets.LastOrDefault()?.Component;

        public void Close()
        {
            IsActive = false;
        }

        public void Draw(GameTime time, ISpriteBatch batch)
        {
            if (!IsActive)
                return;
            _widgets.ForEach(x => x.Component.Draw(time, batch));
        }

        public void Show()
        {
            IsActive = true;
        }
    }
}