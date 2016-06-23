﻿using GameEngine;
using GameEngine.Registry;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GameEngineTest.Registry
{
    [GameTypeAttribute]
    public class SimpleType { }

    public interface TestInterface{}

    [GameTypeAttribute(RegisterType= typeof(TestInterface))]
    public class ImplementingClass : TestInterface{}

    [GameTypeAttribute(singleInstance: true)]
    public class SingleTestType
    {
        public static int counter = 0;
        public SingleTestType() { counter++; }
    }

    [GameTypeAttribute]
    public class GenericClass<T> { }

    [GameTypeAttribute]
    [DefaultParameter("parameter", "key")]
    public class ClassWithDefaultParameters
    {
        public ClassWithDefaultParameters(String parameter)
        {
            this.parameter = parameter;
        }

        public string parameter { get; set; }
    }

    [GameTypeAttribute]
    [DefaultParameter("parameter1", "Key")]
    [DefaultParameter("parameter2", 10)]
    public class ClassWithMulitpleDefaultParameters
    {
        public string parameter1;
        public int parameter2;
        public ClassWithMulitpleDefaultParameters(String parameter1, int parameter2)
        {
            this.parameter1 = parameter1;
            this.parameter2 = parameter2;
        }
    }

    [GameTypeAttribute]
    public class ParentClassWithAttribute { }
    public class InheritedClassWithoutAttribute : ParentClassWithAttribute { }


    [GameTypeAttribute]
    public class OtherImplementingType : TestInterface { }

    [GameTypeAttribute]
    [DefaultParameterType("instance", typeof(OtherImplementingType))]
    public class ClassWithDefaultParameterType
    {
        public TestInterface instance;
        public ClassWithDefaultParameterType(TestInterface instance)
        {
            this.instance = instance;
        }
    }

    public class GameComponentClass : GameEngine.IGameComponent
    {
        public static int instances;
        public GameComponentClass() { instances++; }
        public void Initialize() { throw new NotImplementedException(); }
        public void Update(GameTime time) { throw new NotImplementedException(); }
    }

    public abstract class IGameTypeRegistryTest
    {
        protected abstract IGameTypeRegistry CreateRegistry();

        [SetUp]
        public void Setup()
        {
            SingleTestType.counter = 0;
            GameComponentClass.instances = 0;
        }

        [Test]
        public void ScanAssembly_SimpleComponent_CanResolveComponent()
        {
            var registry = CreateRegistryAndScan();

            Assert.DoesNotThrow(() => registry.ResolveType<SimpleType>());
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

        [Test]
        public void ScanAssembly_ClassWithDefaultParameter_RegisteredParameterIsUsed()
        {
            var registry = CreateRegistryAndScan();
            registry.RegisterParameter("key", "String");

            var resolvedType = registry.ResolveType<ClassWithDefaultParameters>();

            Assert.AreEqual("String", resolvedType.parameter);
        }

        [Test]
        public void ScanAssembly_ClassWithDefaultParameterWithoutResourceRegistering_ResolvedTypeResolvedWithNullArgument()
        {
            var registry = CreateRegistryAndScan();

            var resolvedType = registry.ResolveType<ClassWithDefaultParameters>();

            Assert.AreEqual(null, resolvedType.parameter);
        }

        [Test]
        public void ScanAssembly_ClassWithMultipeDefaultParameter_RegisteredParameterIsUsed()
        {
            var registry = CreateRegistryAndScan();
            registry.RegisterParameter("Key", "String");
            registry.RegisterParameter(10, 20);

            var resolvedType = registry.ResolveType<ClassWithMulitpleDefaultParameters>();

            Assert.AreEqual("String", resolvedType.parameter1);
            Assert.AreEqual(20, resolvedType.parameter2);
        }
        [Test]
        public void ScanAssembly_ClassInheritAttribute_TypeIsNotRegistered()
        {
            var registry = CreateRegistryAndScan();

            Assert.Throws<TypeNotRegisteredException>(() => registry.ResolveType<InheritedClassWithoutAttribute>());
        }

        [Test]
        public void ScanAssembly_ClassWithDefaultParameterType_ResolvedWithGivenType()
        {
            var registry = CreateRegistryAndScan();

            var resolvedInstance = registry.ResolveType<ClassWithDefaultParameterType>();
            Assert.IsInstanceOf<OtherImplementingType>(resolvedInstance.instance);
        }

        [Test]
        public void RegisterAsService_SingeType_ResolveCreatesOnlyOneInstance()
        {
            var registry = CreateRegistry();

            registry.RegisterAsService<SingleTestType, SingleTestType>();

            registry.ResolveType<SingleTestType>();
            registry.ResolveType<SingleTestType>();

            Assert.AreEqual(1, SingleTestType.counter);
        }

        [Test]
        public void RegisterGameComponentForModule_GameComponentClass_ResolveCreateOnlyOneInstance()
        {
            var registry = CreateRegistry();

            registry.RegisterGameComponentForModule<GameComponentClass>("module");

            registry.ResolveType<GameComponentClass>();
            registry.ResolveType<GameComponentClass>();

            Assert.AreEqual(1, GameComponentClass.instances);
        }

        [Test]
        public void CreateGameComponents_ForValidModuleName_ReturnsGameComponentClass()
        {
            var registry = CreateRegistry();

            registry.RegisterGameComponentForModule<GameComponentClass>("module");

            var components = registry.CreateGameComponents("module");
            var expectedComponent = registry.ResolveType<GameComponentClass>();
            
            Assert.True(components.Contains(expectedComponent));
        }

        [Test]
        public void CreateGameComponents_CreateComponentsForUnknownModule_ReturnsEmptyList()
        {
            var registry = CreateRegistry();

            registry.RegisterGameComponentForModule<GameComponentClass>("module");

            var components = registry.CreateGameComponents("m");

            Assert.IsEmpty(components);
        }
        private IGameTypeRegistry CreateRegistryAndScan()
        {
            var registry = CreateRegistry();
            registry.ScanAssembly(Assembly.GetExecutingAssembly());
            return registry;
        }

    }
}
