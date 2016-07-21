using Base;
using BattleLib.GraphicComponents;
using GameEngine;
using GameEngine.Graphics;
using GameEngine.Graphics.GUI;
using GameEngine.Utils;
using Microsoft.Xna.Framework;
using System;

namespace BattleLib.Components.GraphicComponents
{
    public class MoveWidget : AbstractGraphicComponent, IWidget, IMoveWidget
    {
        private TableWidget<Move> tableWidget;
        private Dialog dialog;

        public event EventHandler<SelectionEventArgs<Move>> ItemSelected
        {
            add { tableWidget.ItemSelected += value; }
            remove { tableWidget.ItemSelected -= value; }
        }

        public event EventHandler ExitRequested
        {
            add { tableWidget.OnExitRequested += value; }
            remove { tableWidget.OnExitRequested -= value; }
        }

        public MoveWidget(AttackModel model, AttackTableRenderer renderer, AttackTableSelectionModel selectionModel, Dialog dialog) :
            this(new TableWidget<Move>(null, null, model, renderer, selectionModel), dialog)
        { }

        private MoveWidget(TableWidget<Move> tableWidget, Dialog dialog)
        {
            tableWidget.CheckNull("tableWidget");
            dialog.CheckNull("dialog");

            this.tableWidget = tableWidget;
            this.dialog = dialog;

            dialog.VisibilityChanged += (obj, args) => VisibilityChanged(this, args);
            dialog.AddWidget(tableWidget);
        }

        public bool HandleInput(CommandKeys key)
        {
            return tableWidget.HandleInput(key);
        }

        protected override void DrawComponent(GameTime time, ISpriteBatch batch)
        {
            dialog.Draw(time, batch);
        }

        public override void Setup()
        {
            dialog.Setup();
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
                return dialog.IsVisible;
            }
            set
            {
                dialog.IsVisible = value;
            }
        }

        protected override void Update()
        {
            base.Update();
            dialog.SetCoordinates(this);
        }
    }
}