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
    public class MoveMenuWidget : AbstractMenuWidget<Move>
    {

        public MoveMenuWidget(MoveModel model, AttackTableRenderer renderer, MoveTableSelectionModel selectionModel, Dialog dialog) :
            base(new TableWidget<Move>(null, null, model, renderer, selectionModel), dialog)
        { }
    }
}