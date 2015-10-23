using Base;
using BattleLib.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components.BattleStateComponent
{
    public class ClientIdentifier
    {
        public String Name { get; set; }
    }
    public class BattleData
    {
        ClientIdentifier player;
        ClientIdentifier ai;

        public Pokemon playerPkmn;
        public Pokemon aiPkmn;

        public IClientCommand playerCommand;
        public IClientCommand aiCommand;
    }

    public class BattleStateComponent : GameComponent
    {
        BattleData data = new BattleData();

        IBattleState currentState;

        public BattleStateComponent(ClientIdentifier player, ClientIdentifier ai, Game game) : base(game){}
        public override void Update(GameTime gameTime)
        {
            currentState.Update(data);
        }
    }
}
