﻿using GameEngine.Graphics.Textures;
using GameEngine.TypeRegistry;

namespace GameEngine.Core.ModuleManager
{
    internal interface IModuleManager
    {
        void RegisterModule(IModule module);
        void RegisterContentModule(IContentModule module);
        IGameTypeRegistry TypeRegistry { get; }
        void RegisterTypes();
        void AddTextureConfigurations(TextureConfigurationBuilder builder);
    }
}
