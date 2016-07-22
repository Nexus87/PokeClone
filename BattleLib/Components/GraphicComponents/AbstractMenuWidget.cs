using GameEngine;
using GameEngine.Graphics;
using GameEngine.Graphics.GUI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components.GraphicComponents
{
    public class AbstractMenuWidget<T> : AbstractGraphicComponent, IMenuWidget<T>
    {
        private TableWidget<T> tableWidget;
        private IWidget borderWidget;

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

        private void SetComponents(TableWidget<T> tableWidget, IWidget border)
        {
            this.tableWidget = tableWidget;
            borderWidget = border;

            borderWidget.VisibilityChanged += (obj, args) => VisibilityChanged(this, args);
        }

        public void ResetSelection()
        {
            tableWidget.SelectCell(0, 0);
        }

        public event EventHandler<VisibilityChangedEventArgs> VisibilityChanged = delegate { };

        public bool IsVisible
        {
            get
            {
                return borderWidget.IsVisible;
            }
            set
            {
                borderWidget.IsVisible = value;
            }
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
