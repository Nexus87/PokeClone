using System;
using System.Reflection;
using GameEngine.Components;
using GameEngine.Core;
using GameEngine.Core.ModuleManager;
using GameEngine.Graphics.Textures;
using GameEngine.TypeRegistry;

namespace PokemonGame
{
    public class PokemonGameModule : IModule
    {
        public string ModuleName => "PokemonGameModule";

        public void RegisterTypes(IGameTypeRegistry registry)
        {
            registry.ScanAssembly(Assembly.GetExecutingAssembly());
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
