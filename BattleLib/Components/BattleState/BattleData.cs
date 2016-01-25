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
        public BattleData()
        {
            PlayerPkmn = new PokemonWrapper();
            AIPkmn = new PokemonWrapper();
        }

        public PokemonWrapper PlayerPkmn { get; private set; }
        public PokemonWrapper AIPkmn { get; private set; }

        public ClientIdentifier player;
        public ClientIdentifier ai;

        public ICommand playerCommand;
        public ICommand aiCommand;
    }
}
