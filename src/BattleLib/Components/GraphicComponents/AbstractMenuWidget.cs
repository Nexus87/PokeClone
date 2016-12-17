using GameEngine;
using GameEngine.Graphics;
using GameEngine.Graphics.GUI;
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
        protected TableWidget<T> tableWidget;
        protected IWidget borderWidget;

        public event EventHandler ExitRequested
        {
            add { tableWidget.ExitRequested += value; }
            remove { tableWidget.ExitRequested -= value; }
        }

        public event EventHandler<SelectionEventArgs<T>> ItemSelected
        {
            add { tableWidget.ItemSelected += value; }
            remove { tableWidget.ItemSelected -= value; }
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
            tableWidget = widget;
            borderWidget = border;
        }

        public void ResetSelection()
        {
            tableWidget.SelectCell(0, 0);
        }

        public bool HandleInput(CommandKeys key)
        {
            return tableWidget.HandleInput(key);
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            borderWidget.Draw(time, batch);
        }

        public override void Setup()
        {
            borderWidget.Setup();
        }

        protected override void Update()
        {
            borderWidget.SetCoordinates(this);
        }
    }
}
