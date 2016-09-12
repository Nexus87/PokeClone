﻿using System.Reflection;
using GameEngine;
using GameEngine.Registry;

namespace MainModule
{
    public class MainModule : IModule
    {
        public string ModuleName { get { return "MainModule"; } }

        public void RegisterTypes(IGameTypeRegistry registry)
        {
            registry.ScanAssembly(Assembly.GetExecutingAssembly());
        }

        public void Start(IGameComponentManager manager, IGameTypeRegistry registry)
        {
            throw new System.NotImplementedException();
        }

        public void Stop(IGameComponentManager engine)
        {
            throw new System.NotImplementedException();
        }
    }
}