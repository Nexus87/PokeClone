using Base;
using GameEngine.Graphics.Views;
using System;
using System.Collections.Generic;

namespace BattleLib.GraphicComponents.GUI
{
    internal class AttackModel : DefaultListModel<Move>
    {
        public override int Rows { get { return 4; } }

        public override Move DataAt(int row, int column)
        {
            if (row >= items.Count)
                return null;
            return items[row];
        }

        public override string DataStringAt(int row, int column)
        {
            return row < items.Count ? items[row].ToString() : "----";
        }
    }
}