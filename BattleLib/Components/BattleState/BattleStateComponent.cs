using Base;
using BattleLib.Interfaces;
using Microsoft.Xna.Framework;
using System;

namespace BattleLib.Components.BattleState
{
    internal class PkmnChangedArgs : EventArgs
    {
        public ClientIdentifier id;
    }

    public class ClientIdentifier
    {
        public String Name { get; set; }
    }

    public class BattleStateComponent : GameComponent
    {
        BattleData data = new BattleData();

        IBattleState currentState;

        public ClientIdentifier PlayerIdentifier {
            get { return data.player; }
            set { data.player = value; }
        }

        public ClientIdentifier AIIdentifier
        {
            get { return data.ai; }
            set { data.ai = value; }
        }

        public BattleStateComponent(Game game) : base(game) { }
        public BattleStateComponent(ClientIdentifier player, ClientIdentifier ai, Game game) : base(game)
        {
            data.player = player;
            data.ai = ai;
        }

        public override void Initialize()
        {
            if (AIIdentifier == null || PlayerIdentifier == null)
                throw new InvalidOperationException("One of the identifier is missing");
        }

        public override void Update(GameTime gameTime)
        {
            currentState.Update(data);
        }

        public void SetCharacter(ClientIdentifier id, Pokemon pkmn)
        {
            currentState.SetCharacter(id, pkmn);
        }

        public void SetMove(ClientIdentifier id, Move move)
        {
            currentState.SetMove(id, move);
        }

        public void SetItem(ClientIdentifier id, Item item)
        {
            currentState.SetItem(id, item);
        }
    }
}
