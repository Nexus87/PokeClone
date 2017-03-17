using GameEngine.Core;
using GameEngine.Entities;
using GameEngine.Tools;
using GameEngine.TypeRegistry;
using PokemonShared.Data;
using Module = GameEngine.Core.ModuleManager.Module;

namespace PokemonShared.Core
{
    public class PokemonSharedModule : Module
    {
        public PokemonSharedModule() : base(new object(), "Shared", "Shared")
        {
        }

        public override void RegisterTypes(IGameTypeRegistry registry)
        {
            registry.RegisterAsService<JSonStorage<MoveData>, IStorage<MoveData>>(x => new JSonStorage<MoveData>("PokemonShared/" + "MoveData.json"));
            registry.RegisterAsService<JSonStorage<PokemonData>, IStorage<PokemonData>>(x => new JSonStorage<PokemonData>("PokemonShared/" + "PokemonData.json"));
            registry.RegisterAsService<JSonStorage<PokemonData>, IStorage<MoveSetItem>>(x => new JSonStorage<PokemonData>("PokemonShared/" + "MoveSets.json"));
        }

        public override void Start(IGameComponentManager manager, IInputHandlerManager inputHandlerManager, IGameTypeRegistry registry)
        {
            throw new System.NotImplementedException();
        }

        public override void Stop(IGameComponentManager engine, IInputHandlerManager inputHandlerManager)
        {
            throw new System.NotImplementedException();
        }
    }
}