using GameEngine;
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
    public class TestModule : IModule
    {
        public static bool WasCreated;
        public static bool WasCalled ;
        public string ModuleName { get { return "TestModule"; } }
        public TestModule()
        {
            WasCreated = true;
        }

        public void RegisterTypes(IGameTypeRegistry registry)
        {
            WasCalled = true;
        }


        public void Start(PokeEngine engine, IGameTypeRegistry registry)
        {
            throw new NotImplementedException();
        }

        public void Stop(PokeEngine engine)
        {
            throw new NotImplementedException();
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
