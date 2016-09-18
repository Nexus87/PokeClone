using GameEngine.Registry;

namespace GameEngineTest.Registry
{
    public class AutofacGameTypeRegistryTest : IGameTypeRegistryTest
    {
        protected override IGameTypeRegistry CreateRegistry()
        {
            return new AutofacGameTypeRegistry();
        }
    }
}
