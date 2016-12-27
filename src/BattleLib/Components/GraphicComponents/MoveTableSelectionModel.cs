using Base;
using GameEngine.GUI.Graphics.TableView;
using GameEngine.GUI.Utils;
using GameEngine.TypeRegistry;

namespace BattleMode.Core.Components.GraphicComponents
{
    [GameType]
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
