﻿using GameEngine.Registry;

namespace GameEngine
{
    public interface IModule
    {
        string ModuleName { get; }
        void RegisterTypes(IGameTypeRegistry registry);
        void Start(IGameComponentManager manager, IGameTypeRegistry registry);
        void Stop(IGameComponentManager engine);
    }
}