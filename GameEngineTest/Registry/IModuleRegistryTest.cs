using GameEngine.Registry;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Registry
{
    [ModuleRegistration("TestModule")]
    public class TestModule
    {
        public static bool WasCreated = false;
        public static bool WasCalled = false;
        public TestModule()
        {
            WasCreated = true;
        }
        [ModuleInit]
        public void Init(IGameTypeRegistry registry)
        {
            WasCalled = true;
        }
    }

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

            Assert.True(TestModule.WasCreated);
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
