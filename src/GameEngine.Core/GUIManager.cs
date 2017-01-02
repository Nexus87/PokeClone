using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine.Core.GameEngineComponents;
using GameEngine.Globals;
using GameEngine.Graphics.General;
using GameEngine.GUI;
using GameEngine.TypeRegistry;
using Microsoft.Xna.Framework;

namespace GameEngine.Core
{
    [GameService(typeof(GuiManager))]
    public class GuiManager : IInputHandler
    {
        private class WidgetItem : IComparable<WidgetItem>
        {
            public readonly int Priority;
            public readonly IGraphicComponent Component;

            public WidgetItem(int priority, IGraphicComponent component)
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

        private readonly InputComponent _inputComponent;
        private readonly List<WidgetItem> _widgets2 = new List<WidgetItem>();

        public GuiManager(InputComponent inputComponent)
        {
            _inputComponent = inputComponent;
        }

        public void ShowWidget(IGraphicComponent widget, int? priority = null)
        {
            if(_widgets2.Any(x => x.Component == widget))
                return;

            if (!priority.HasValue)
                priority = _widgets2.Count == 0 ? 0 : _widgets2.Max(x => x.Priority) + 1;

            _widgets2.Add(new WidgetItem(priority.Value, widget));
            _widgets2.Sort();

        }

        public void CloseWidget(IGraphicComponent widget)
        {
            var w = _widgets2.FirstOrDefault(x => x.Component == widget);
            if (w != null)
                _widgets2.Remove(w);
        }

        public bool IsActive { get; set; }


        public void HandleKeyInput(CommandKeys key)
        {

            FocusedWidget?.HandleKeyInput(key);
        }

        public IGraphicComponent FocusedWidget => _widgets2.LastOrDefault()?.Component;

        public void Close()
        {
            IsActive = false;
            _inputComponent.RemoveHandler(this);
        }

        public void Draw(GameTime time, ISpriteBatch batch)
        {
            if (!IsActive)
                return;
            _widgets2.ForEach(x => x.Component.Draw(time, batch));
        }

        public void Show()
        {
            IsActive = true;
            _inputComponent.AddHandler(this, true);
        }
    }
}