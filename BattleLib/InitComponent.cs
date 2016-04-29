using Base.Rules;
using BattleLib.Components.AI;
using BattleLib.Components.BattleState;
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

        public InitComponent(Configuration config, IPokeEngine game, GraphicComponentFactory factory, RulesSet rules, ICommandScheduler scheduler)
        {
            var playerID = new ClientIdentifier();
            var aiID = new ClientIdentifier();
            playerID.Name = "Player";
            playerID.IsPlayer = false;
            engine = game;

            aiID.Name = "AI";
            aiID.IsPlayer = false;

            player = new Client(playerID);
            ai = new Client(aiID);

            var graphic = new BattleGraphics(game, factory, playerID, aiID);
            battleState = new BattleStateComponent(playerID, aiID, game, rules, scheduler);
            var gui = new BattleGUI(config, game, factory, battleState, playerID, aiID);
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
