using GameEngine;
using GameEngine.Registry;
using System;
using System.Reflection;

namespace PokemonGame
{
    public class PokemonGameModule : IModule
    {
        public string ModuleName
        {
            get { return "PokemonGameModule"; }
        }

        public void RegisterTypes(IGameTypeRegistry registry)
        {
            registry.ScanAssembly(Assembly.GetExecutingAssembly());
        }

        public void Start(IGameComponentManager componentManager, IGameTypeRegistry registry)
        {
            throw new NotImplementedException();
        }

        public void Stop(IGameComponentManager componentManager)
        {
            throw new NotImplementedException();
        }
    }
}
