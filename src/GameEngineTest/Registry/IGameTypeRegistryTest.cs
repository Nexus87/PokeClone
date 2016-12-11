﻿using GameEngine.Registry;
using NUnit.Framework;
using System;
using System.Linq;
using System.Reflection;

namespace GameEngineTest.Registry
{
    public abstract class IGameTypeRegistryTest
    {
        protected abstract IGameTypeRegistry CreateRegistry();

        [SetUp]
        public void Setup()
        {
            GameComponentClass.instances = 0;
            GameService.instances = 0;
        }

        [GameType]
        public class GameType { }
        [Test]
        public void ScanAssembly_GameTypeAttribute_TypeCanBeResolved()
        {
            var registry = CreateRegistry();
            registry.ScanAssembly(Assembly.GetExecutingAssembly());

            GameType type = registry.ResolveType<GameType>();

            Assert.NotNull(type);
        }

        [GameType]
        public class GenericType<T> { }

        [Test]
        public void ScanAssembly_GameTypeAttributeGenericType_TypeCanBeResolved()
        {
            var registry = CreateRegistry();
            registry.ScanAssembly(Assembly.GetExecutingAssembly());

            GenericType<Object> type = registry.ResolveType<GenericType<Object>>();

            Assert.NotNull(type);
        }

        [Test]
        public void ScanAssembly_GameServiceAttribute_ServiceCanBeResolved()
        {
            var registry = CreateRegistry();
            registry.ScanAssembly(Assembly.GetExecutingAssembly());

            IGameService service = registry.ResolveType<IGameService>();

            Assert.NotNull(service);
        }

        [Test]
        public void ResolveType_GameServiceAttribute_OnlyOneInstanceCreated()
        {
            var registry = CreateRegistry();
            registry.ScanAssembly(Assembly.GetExecutingAssembly());

            registry.ResolveType<IGameService>();
            registry.ResolveType<IGameService>();

            Assert.AreEqual(1, GameService.instances);
        }

        public class ClassWithArgument
        {
            public String argument;
            public ClassWithArgument(String argument)
            {
                this.argument = argument;
            }
        }
    }
}
