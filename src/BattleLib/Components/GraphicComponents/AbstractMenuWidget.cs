using Microsoft.Xna.Framework;
using System;
using GameEngine.Globals;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.General;
using GameEngine.GUI.Graphics.GUI;

namespace BattleLib.Components.GraphicComponents
{
    public class AbstractMenuWidget<T> : AbstractGraphicComponent, IMenuWidget<T>
    {
        protected TableWidget<T> TableWidget;
        protected IWidget BorderWidget;

        public event EventHandler ExitRequested
        {
            add { TableWidget.ExitRequested += value; }
            remove { TableWidget.ExitRequested -= value; }
        }

        public event EventHandler<SelectionEventArgs<T>> ItemSelected
        {
            add { TableWidget.ItemSelected += value; }
            remove { TableWidget.ItemSelected -= value; }
        }

        public AbstractMenuWidget(TableWidget<T> widget)
        {
            SetComponents(widget, widget);
        }
        public AbstractMenuWidget(TableWidget<T> widget, Dialog border)
        {
            border.AddWidget(widget);
            SetComponents(widget, border);
        }

        private void SetComponents(TableWidget<T> widget, IWidget border)
        {
            TableWidget = widget;
            BorderWidget = border;
        }

        public void ResetSelection()
        {
            TableWidget.SelectCell(0, 0);
        }

        public override void HandleKeyInput(CommandKeys key)
        {
            TableWidget.HandleKeyInput(key);
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            BorderWidget.Draw(time, batch);
        }

        public override void Setup()
        {
            BorderWidget.Setup();
        }

        protected override void Update()
        {
            BorderWidget.SetCoordinates(this);
        }
    }
}
