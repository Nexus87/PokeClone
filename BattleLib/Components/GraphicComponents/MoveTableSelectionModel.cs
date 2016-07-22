using Base;
using GameEngine.Graphics;
using GameEngine.Registry;
using GameEngine.Utils;

namespace BattleLib.GraphicComponents
{
    public class MoveTableSelectionModel : TableSingleSelectionModel
    {
        public MoveTableSelectionModel(MoveModel model) :
            this((ITableModel<Move>) model)
        {
        
        }
        internal MoveTableSelectionModel(ITableModel<Move> model)
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
