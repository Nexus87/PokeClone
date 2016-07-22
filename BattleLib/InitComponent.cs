using Base;
using Base.Rules;
using BattleLib.Components;
using BattleLib.Components.AI;
using BattleLib.Components.BattleState;
using BattleLib.Components.GraphicComponents;
using BattleLib.GraphicComponents;
using GameEngine;
using GameEngine.Graphics;
using Microsoft.Xna.Framework;
using System.Linq;

namespace BattleLib
{
    public class InitComponent : GameEngine.IGameComponent
    {
        Client player;
        Client ai;
        IPokeEngine engine;
        private BattleStateComponent battleState;
        private BattleGraphicController graphic;
        private IGUIService gui;

        public InitComponent(Configuration config, IPokeEngine game, IEventQueue queue, GraphicComponentFactory factory, IMoveEffectCalculator rules, ICommandScheduler scheduler)
        {
            BattleLibTypes.RegisterTypes(factory.registry);
            engine = game;
            BattleData data = factory.registry.ResolveType<BattleData>();
            var playerID = data.PlayerId;
            var aiID = data.Clients.Where(id => !id.IsPlayer).First();
            player = new Client(playerID);
            ai = new Client(aiID);
            
            battleState = (BattleStateComponent) factory.registry.ResolveType<IBattleStateService>();
            var eventCreator = (EventCreator) factory.registry.ResolveType<IEventCreator>();

            graphic = new BattleGraphicController(factory.registry.ResolveType<Screen>(), game, factory, playerID, aiID);
            gui = factory.registry.ResolveType<IGUIService>();

            var eventProcess = new BattleEventProcessor(gui, graphic, queue, eventCreator);
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
            battleState.SetCharacter(player.Id, player.Pokemons.First());
            engine.RemoveGameComponent(this);
        }
    }
}
