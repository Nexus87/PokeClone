using GameEngine.Graphics;

namespace BattleLib.GraphicComponents
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
