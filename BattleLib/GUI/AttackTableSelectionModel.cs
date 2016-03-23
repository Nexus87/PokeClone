using BattleLib.GraphicComponents.GUI;
using GameEngine.Graphics.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.GUI
{
    internal class AttackTableSelectionModel : DefaultTableSelectionModel
    {
        private AttackModel model;

        public AttackTableSelectionModel(AttackModel model)
        {
            this.model = model;
        }

        public override bool SelectIndex(int row, int column)
        {
            if (model[row, column] == null)
                return false;
            return base.SelectIndex(row, column);
        }
    }
}
