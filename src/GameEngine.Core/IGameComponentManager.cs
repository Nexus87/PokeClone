﻿using GameEngine.Globals;
using GameEngine.GUI;

namespace GameEngine.Core
{
    public interface IGameComponentManager
    {
        void AddGameComponent(IGameComponent component);
        void RemoveGameComponent(IGameComponent component);
        IGraphicComponent Graphic { get; set; }
    }
}