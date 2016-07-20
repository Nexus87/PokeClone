using GameEngine;
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
            GameComponentClass.instances = 0;
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
    

    }
}
