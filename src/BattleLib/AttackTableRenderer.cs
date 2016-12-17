using Base;
using GameEngine.GUI.Graphics;
using GameEngine.GUI.Graphics.TableView;
using GameEngine.Registry;

namespace BattleLib
{
    [GameType]
    public class AttackTableRenderer : ITableRenderer<Move>
    {
        private readonly DefaultTableRenderer<Move> _renderer;

        public AttackTableRenderer(DefaultTableRenderer<Move> renderer)
        {
            renderer.DefaultString = "------";
            _renderer = renderer;
        }

        public IGraphicComponent GetComponent(int row, int column, Move data, bool isSelected)
        {
            return _renderer.GetComponent(row, column, data, isSelected);
        }
    }
}
