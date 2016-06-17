using Base;
using GameEngine.Graphics;
using GameEngine.Registry;
using GameEngine.Utils;

namespace BattleLib.GraphicComponents
{
    [GameTypeAttribute]
    class AttackTableSelectionModel : TableSingleSelectionModel
    {
        public AttackTableSelectionModel(ITableModel<Move> model)
            : base(
            (row, column) => { 
                if(model.IndexInRange(row, column))
                    return model.DataAt(row, column) != null;
                return false;
            }
            )
        {
            model.CheckNull("model");
        }

    }
}
