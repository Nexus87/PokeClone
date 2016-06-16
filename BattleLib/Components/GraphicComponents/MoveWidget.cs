using Base;
using GameEngine;
using GameEngine.Graphics;
using GameEngine.Graphics.GUI;
using GameEngine.Utils;
using System;

namespace BattleLib.Components.GraphicComponents
{
    public class MoveWidget : AbstractWidget, IWidget, IMoveWidget
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

        public MoveWidget(TableWidget<Move> tableWiget, Dialog dialog)
        {
            tableWidget.CheckNull("tableWidget");
            dialog.CheckNull("dialog");

            this.tableWidget = tableWiget;
            this.dialog = dialog;

            dialog.AddWidget(tableWidget);
        }

        public override bool HandleInput(CommandKeys key)
        {
            return tableWidget.HandleInput(key);
        }

        protected override void DrawComponent(Microsoft.Xna.Framework.GameTime time, ISpriteBatch batch)
        {
            dialog.Draw(time, batch);
        }

        public override void Setup()
        {
            dialog.Setup();
        }
    }
}