using GameEngine.Registry;

namespace GameEngineTest.Registry
{
    [GameService(typeof(IGameService))]
    public class GameService : IGameService { 
        public static int instances = 0; 
        public GameService() { instances++; } 
    }
}