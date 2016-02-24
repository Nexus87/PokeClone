using Base;
using BattleLib.Components.BattleState.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleLib.Components.BattleState
{
    internal class PkmnChangedArgs : EventArgs
    {
        public ClientIdentifier id;
    }

    public class BattleData
    {
        public BattleData(ClientIdentifier player, ClientIdentifier ai)
        {
            this.player = player;
            this.ai = ai;
            PlayerPkmn = new PokemonWrapper(player);
            AIPkmn = new PokemonWrapper(ai);
        }

        public PokemonWrapper GetPkmn(ClientIdentifier id)
        {
            return id == player ? PlayerPkmn : AIPkmn;
        }

        public PokemonWrapper PlayerPkmn { get; private set; }
        public PokemonWrapper AIPkmn { get; private set; }

        public ClientIdentifier player;
        public ClientIdentifier ai;

        public ICommand playerCommand;
        public ICommand aiCommand;
    }
}
