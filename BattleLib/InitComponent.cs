using Base;
using BattleLib.Components.AI;
using BattleLib.Components.BattleState;
using BattleLib.GraphicComponents;
using BattleLib.GraphicComponents.GUI;
using GameEngine;
using GameEngine.EventComponent;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib
{
    public class InitComponent : GameComponent
    {
        Client player;
        Client ai;

        private BattleStateComponent battleState;

        public InitComponent(Configuration config, PokeEngine game) : base(game)
        {
            var playerID = new ClientIdentifier();
            var aiID = new ClientIdentifier();
            playerID.Name = "Player";
            playerID.IsPlayer = false;

            aiID.Name = "AI";
            aiID.IsPlayer = false;

            player = new Client(playerID);
            ai = new Client(aiID);

            var graphic = new BattleGraphics(game, playerID, aiID);
            battleState = new BattleStateComponent(playerID, aiID, game);
            var gui = new BattleGUI(config, game, battleState, playerID);
            var aiComponent = new AIComponent(battleState, ai, game);

            game.Components.Add(aiComponent);
            game.Components.Add(battleState);
            game.Graphic = graphic;
            
        }

        public override void Initialize()
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var baseData = new PKData();
            Stats stats = new Stats() { HP = 30 };

            battleState.SetCharacter(player.ID, player.Pokemons.First());

            Game.Components.Remove(this);
        }
    }
}
