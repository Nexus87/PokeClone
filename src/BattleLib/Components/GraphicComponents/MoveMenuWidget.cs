using Base;
using GameEngine.GUI.Graphics.GUI;
using GameEngine.TypeRegistry;

namespace BattleMode.Core.Components.GraphicComponents
{
    [GameType]
    public class MoveMenuWidget : AbstractMenuWidget<Move>
    {

        public MoveMenuWidget(MoveModel model, MoveTableSelectionModel selectionModel,
            Dialog dialog, IGameTypeRegistry registry) :
            base(new TableWidget<Move>(null, null, model, selectionModel, registry), dialog, registry)
        {
        }
    }
}