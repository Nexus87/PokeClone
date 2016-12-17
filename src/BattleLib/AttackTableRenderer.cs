using Base;
using GameEngine.Graphics;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.TableView;
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

        public IGraphicComponent GetComponent(int row, int column, Move data, bool isSelected)
        {
            return renderer.GetComponent(row, column, data, isSelected);
        }
    }
}
