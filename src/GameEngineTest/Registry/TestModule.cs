﻿using System;
using GameEngine.Core;
using GameEngine.Core.GameEngineComponents;
using GameEngine.Core.ModuleManager;
using GameEngine.Graphics;
using GameEngine.TypeRegistry;

namespace GameEngineTest.Registry
{
    public class TestModule : IModule
    {
        public static bool WasCreated;
        public static bool WasCalled ;
        public string ModuleName => "TestModule";

        public TestModule()
        {
            WasCreated = true;
        }

        public void RegisterTypes(IGameTypeRegistry registry)
        {
            WasCalled = true;
        }

        public void AddTextureConfigurations(TextureConfigurationBuilder builder)
        {
        }


        public void Start(IGameComponentManager componentManager, IInputHandlerManager inputHandlerManager, IGameTypeRegistry registry)
        {
            throw new NotImplementedException();
        }

        public void Stop(IGameComponentManager componentManager, IInputHandlerManager inputHandlerManager)
        {
            throw new NotImplementedException();
        }
    }
}