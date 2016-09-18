using GameEngine.Registry;
using NUnit.Framework;

namespace GameEngineTest.Registry
{
    [TestFixture]
    public class AutofacModuleRegistryTest : IModuleRegistryTest
    {
        protected override IModuleRegistry CreateModuleRegistration()
        {
            return new AutofacModuleRegistry();
        }
    }
}
