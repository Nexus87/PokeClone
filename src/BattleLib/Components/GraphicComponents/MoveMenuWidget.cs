using Base;
using GameEngine.GUI.Graphics.GUI;
using GameEngine.Registry;

namespace BattleLib.Components.GraphicComponents
{
    [GameType]
    public class MoveMenuWidget : AbstractMenuWidget<Move>
    {

        public MoveMenuWidget(MoveModel model, AttackTableRenderer renderer, MoveTableSelectionModel selectionModel, Dialog dialog) :
            base(new TableWidget<Move>(null, null, model, renderer, selectionModel), dialog)
        { }
    }
}