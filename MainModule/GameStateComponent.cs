using Microsoft.Xna.Framework;
using IGameComponent = GameEngine.IGameComponent;

namespace MainModule
{
    public class GameStateComponent : IGameComponent, IGameStateComponent
    {
        public void Move(Direction direction){}

        public void AddWorldEvent(IWorldEvent worldEvent){}
        public void RemoveWorldEvent(IWorldEvent worldEvent){}


        public void Initialize()
        {
            throw new System.NotImplementedException();
        }

        public void Update(GameTime time)
        {
            throw new System.NotImplementedException();
        }
    }
}