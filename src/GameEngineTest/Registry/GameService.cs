using GameEngine.TypeRegistry;

namespace GameEngineTest.Registry
{
    [GameService(typeof(IGameService))]
    public class GameService : IGameService { 
        public static int Instances;
        public GameService() { Instances++; }
    }
}