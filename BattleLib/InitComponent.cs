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
        readonly Client player;
        readonly Client ai;
        readonly PokeEngine engine;
        readonly IBattleStateService battleState;
        readonly IBattleGraphicController graphic;
        readonly IGUIService gui;

        public InitComponent(PokeEngine game, IGameTypeRegistry registry)
        {
            engine = game;
            var data = registry.ResolveType<BattleData>();
            var playerID = data.PlayerId;
            var aiID = data.Clients.First(id => !id.IsPlayer);
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
