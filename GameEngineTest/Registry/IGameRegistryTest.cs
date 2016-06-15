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
    [GameComponentAttribute]
    public class TestType { }

    public interface TestInterface{}

    [GameComponentAttribute(RegisterType= typeof(TestInterface))]
    public class ImplementingClass : TestInterface{}

    [GameComponentAttribute(singleInstance: true)]
    public class SingleTestType
    {
        public static int counter = 0;
        public SingleTestType() { counter++; }
    }

    [GameComponentAttribute]
    public class GenericClass<T> { }


    public abstract class IGameRegistryTest
    {
        protected abstract IGameRegistry CreateRegistry();

        [Test]
        public void ScanAssembly_SimpleComponent_CanResolveComponent()
        {
            var registry = CreateRegistryAndScan();

            Assert.DoesNotThrow(() => registry.ResolveType<TestType>());
        }

        [Test]
        public void ScanAssembly_SingleUseAssembly_WillOnlyCreateOne()
        {
            var registry = CreateRegistryAndScan();

            registry.ResolveType<SingleTestType>();
            registry.ResolveType<SingleTestType>();

            Assert.AreEqual(1, SingleTestType.counter);
        }

        [Test]
        public void ScanAssembly_RegisterTypeAsInterface_CanResolveType()
        {
            var registry = CreateRegistryAndScan();

            Assert.DoesNotThrow(() => registry.ResolveType<TestInterface>());
        }

        [Test]
        public void ScanAssembly_RegisterTypeAsInterface_ResolvedTypeIsInstanceOfImplementingClass()
        {
            var registry = CreateRegistryAndScan();

            var resolvedType = registry.ResolveType<TestInterface>();

            Assert.IsInstanceOf<ImplementingClass>(resolvedType);
        }

        [Test]
        public void ScanAssembly_GenericType_CanBeResolved()
        {
            var registry = CreateRegistryAndScan();

            Assert.DoesNotThrow(() => registry.ResolveType<GenericClass<object>>());
        }

        private IGameRegistry CreateRegistryAndScan()
        {
            var registry = CreateRegistry();
            registry.ScanAssembly(Assembly.GetExecutingAssembly());
            return registry;
        }
    }
}
