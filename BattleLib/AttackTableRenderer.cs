using Base;
using GameEngine.Graphics;
using GameEngine.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleLib.Components.GraphicComponents
{
    [GameType]
    public class AttackTableRenderer : ITableRenderer<Move>
    {
        readonly DefaultTableRenderer<Move> renderer;

        public AttackTableRenderer(DefaultTableRenderer<Move> renderer)
        {
            renderer.DefaultString = "------";
            this.renderer = renderer;
        }

        public ISelectableGraphicComponent GetComponent(int row, int column, Move data, bool isSelected)
        {
            return renderer.GetComponent(row, column, data, isSelected);
        }
    }
}
