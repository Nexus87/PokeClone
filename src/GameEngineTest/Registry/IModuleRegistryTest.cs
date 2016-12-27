using NUnit.Framework;
using System.Linq;
using System.Reflection;
using GameEngine.Core.Registry;

namespace GameEngineTest.Registry
{
    public abstract class IModuleRegistryTest
    {
        [SetUp]
        public void SetUp()
        {
            TestModule.WasCalled = TestModule.WasCreated = false;
        }

        [Test]
        public void RegisterModule_TestModule_TestModuleIsCreated()
        {
            var registry = CreateModuleRegistration();
            
            registry.RegisterModule(Assembly.GetExecutingAssembly());

            Assert.True(registry.RegisteredModuleNames.Contains("TestModule"));
        }

        [Test]
        public void RegisterModule_TestModule_InitMethodIsCalled()
        {
            var registry = CreateModuleRegistration();

            registry.RegisterModule(Assembly.GetExecutingAssembly());

            Assert.True(TestModule.WasCalled);
        }
        protected abstract IModuleRegistry CreateModuleRegistration();
    }
}
