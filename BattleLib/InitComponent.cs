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
        //EventCreator eventCreator;
        private BattleStateComponent battleState;
        private BattleGraphicController graphic;
        private BattleGUI gui;

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

            graphic = new BattleGraphicController(game, factory, playerID, aiID);
            gui = new BattleGUI(game, factory.registry.ResolveType<IMenuWidget<Move>>(), factory, battleState, playerID, aiID);

            var eventProcess = new BattleEventProcessor(gui, graphic, queue, eventCreator);
            var aiComponent = new AIComponent(battleState, ai, playerID);
            

            game.AddGameComponent(aiComponent);
            game.AddGameComponent(battleState);
            game.Graphic = graphic;
            
        }

        public void Initialize()
        {
        }

        private void SetupBattleState(ClientIdentifier playerID, ClientIdentifier aiID, IMoveEffectCalculator rules, ICommandScheduler scheduler)
        {
            //var actionState = new WaitForActionState();
            //var characterState = new WaitForCharState(eventCreator);
            //var commandExecutor = new CommandExecuter(rules, eventCreator);
            //var executionState = new ExecuteState(scheduler, commandExecutor);
            //battleState = new BattleStateComponent(playerID, aiID, actionState, characterState, executionState, eventCreator);
        }
        public void Update(GameTime gameTime)
        {
            // The AI sets the Character itself
            battleState.SetCharacter(player.Id, player.Pokemons.First());
            engine.RemoveGameComponent(this);
        }
    }
}
