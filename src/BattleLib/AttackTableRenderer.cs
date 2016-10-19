using Base;
using GameEngine.Graphics;
using GameEngine.Registry;

namespace BattleLib
{
    [GameType]
    public class AttackTableRenderer : ITableRenderer<Move>
    {
        private readonly DefaultTableRenderer<Move> renderer;

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
