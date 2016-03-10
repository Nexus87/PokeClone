using BattleLib.Components.BattleState.Commands;
using System;
using System.Collections.Generic;

namespace BattleLib.Components.BattleState
{
    public class BattleData
    {
        public ClientIdentifier Ai { get; set; }

        public ICommand AiCommand { get; set; }

        public ClientIdentifier Player { get; set; }

        public ICommand PlayerCommand { get; set; }

        private List<ClientIdentifier> clients = new List<ClientIdentifier>();

        public BattleData(ClientIdentifier player, ClientIdentifier ai)
        {
            this.Player = player;
            this.Ai = ai;

            clients.Add(ai);
            clients.Add(player);

            PlayerPokemon = new PokemonWrapper(player);
            AiPokemon = new PokemonWrapper(ai);
        }

        public PokemonWrapper AiPokemon { get; private set; }

        public IReadOnlyList<ClientIdentifier> Clients { get { return clients.AsReadOnly(); } }

        public PokemonWrapper PlayerPokemon { get; private set; }

        public PokemonWrapper GetPokemon(ClientIdentifier id)
        {
            return id == Player ? PlayerPokemon : AiPokemon;
        }

        public void SetCommand(ClientIdentifier id, ICommand command)
        {
            if (id == Player)
                PlayerCommand = command;
            else
                AiCommand = command;
        }
    }
}