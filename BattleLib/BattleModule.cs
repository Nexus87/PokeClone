using Base;
using BattleLib.Components;
using BattleLib.Components.AI;
using BattleLib.Components.BattleState;
using BattleLib.Components.GraphicComponents;
using BattleLib.GraphicComponents;
using GameEngine;
using GameEngine.Graphics;
using GameEngine.Graphics.GUI;
using GameEngine.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib
{
    public class BattleModule : IModule
    {
        private IBattleGraphicController graphic;
        private IBattleStateService battleState;
        private AIComponent aiComponent;
        
        public string ModuleName
        {
            get { return "BattleModule"; }
        }

        public void RegisterTypes(IGameTypeRegistry registry)
        {
            registry.ScanAssembly(Assembly.GetExecutingAssembly());
        }

        public void Start(PokeEngine engine, IGameTypeRegistry registry)
        {
            var data = registry.ResolveType<BattleData>();
            var playerID = data.PlayerId;
            var aiID = data.Clients.First(id => !id.IsPlayer);
            var player = new Client(playerID);
            var ai = new Client(aiID);

            battleState = registry.ResolveType<IBattleStateService>();
            graphic = registry.ResolveType<IBattleGraphicController>();
            aiComponent = new AIComponent(battleState, ai, playerID);
            // Needs to be created since no other class depend on it.
            registry.ResolveType<BattleEventProcessor>();


            engine.AddGameComponent(aiComponent);
            engine.AddGameComponent(battleState);
            engine.Graphic = graphic;

            engine.ShowGUI();
            battleState.SetCharacter(player.Id, player.Pokemons.First());
        }

        public void Stop(PokeEngine engine)
        {
            engine.RemoveGameComponent(aiComponent);
            engine.RemoveGameComponent(battleState);
            engine.Graphic = null;

            battleState = null;
            graphic = null;
            aiComponent = null;
        }
    }
}
