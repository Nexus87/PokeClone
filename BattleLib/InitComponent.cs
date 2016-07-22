using Base;
using Base.Rules;
using BattleLib.Components;
using BattleLib.Components.AI;
using BattleLib.Components.BattleState;
using BattleLib.Components.GraphicComponents;
using BattleLib.GraphicComponents;
using GameEngine;
using GameEngine.Graphics;
using GameEngine.Registry;
using Microsoft.Xna.Framework;
using System.Linq;

namespace BattleLib
{
    public class InitComponent : GameEngine.IGameComponent
    {
        Client player;
        Client ai;
        PokeEngine engine;
        private IBattleStateService battleState;
        private IBattleGraphicController graphic;
        private IGUIService gui;

        public InitComponent(Configuration config, PokeEngine game, IGameTypeRegistry registry)
        {
            BattleLibTypes.RegisterTypes(registry);
            engine = game;
            BattleData data = registry.ResolveType<BattleData>();
            var playerID = data.PlayerId;
            var aiID = data.Clients.Where(id => !id.IsPlayer).First();
            player = new Client(playerID);
            ai = new Client(aiID);
            
            battleState = registry.ResolveType<IBattleStateService>();
            var eventCreator = (EventCreator) registry.ResolveType<IEventCreator>();

            graphic = registry.ResolveType<IBattleGraphicController>();
            gui = registry.ResolveType<IGUIService>();

            var eventProcess = registry.ResolveType<BattleEventProcessor>();
            var aiComponent = new AIComponent(battleState, ai, playerID);
            

            game.AddGameComponent(aiComponent);
            game.AddGameComponent(battleState);
            game.Graphic = graphic;
            
        }

        public void Initialize()
        {
        }

        public void Update(GameTime gameTime)
        {
            // The AI sets the Character itself
            engine.ShowGUI();
            battleState.SetCharacter(player.Id, player.Pokemons.First());
            engine.RemoveGameComponent(this);
        }
    }
}
