using Base;
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
        ClientIdentifier player = new ClientIdentifier();
        ClientIdentifier ai = new ClientIdentifier();
        private BattleStateComponent battleState;

        public InitComponent(Configuration config, PokeEngine game) : base(game)
        {
            player.Name = "Player";
            player.IsPlayer = false;

            ai.Name = "AI";
            ai.IsPlayer = false;

            var graphic = new BattleGraphics(game, player, ai);
            battleState = new BattleStateComponent(player, ai, game);
            var gui = new BattleGUI(config, game, battleState, player);

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

            Pokemon playerPkmn = new Pokemon(baseData, 10, "Pikachu", stats, stats);
            Pokemon aiPkmn = new Pokemon(baseData, 5, "Jigglypuff", stats, stats);

            playerPkmn.HP = 10;
            aiPkmn.HP = 20;

            battleState.SetCharacter(player, playerPkmn);
            battleState.SetCharacter(ai, aiPkmn);

            Game.Components.Remove(this);
        }
    }
}
