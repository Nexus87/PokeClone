using GameEngine.Graphics;

namespace MainMode.Core.Graphics
{
    public interface ISceneController
    {
        Scene Scene { get; }
        void CenterField(int fieldX, int fieldY);
        void MoveMap(Direction moveDirection);
        void LoadMap(Map map);
    }
}